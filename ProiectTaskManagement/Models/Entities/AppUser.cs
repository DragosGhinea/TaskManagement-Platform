using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectTaskManagement.Models.Entities
{
    [Index(nameof(NormalizedEmail), IsUnique = true)]
    public class AppUser : IdentityUser
    {
        [RegularExpression("^[a-zA-Z0-9_.]*$", ErrorMessage = "The username must be alphanumeric with _ and . at most")]
        //[Required(ErrorMessage = "Username-ul este obligatoriu")]
        [MinLength(3, ErrorMessage = "Username needs to be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Username must be 20 characters long at most")]
        override
        public string? UserName { get; set; }

        //[Required(ErrorMessage = "Prenumele este obligatoriu")]
        [RegularExpression("^[A-Z][a-z]+([-][A-Z][a-z]+)*$", ErrorMessage = "The first name must be 'Firstname' or 'Firstname1-Firstname2', each first name must be at least 2 characters long")]
        public string? FirstName { get; set; }

        //[Required(ErrorMessage = "Numele este obligatoriu")]
        [RegularExpression("^[A-Z][a-z]+([-][A-Z][a-z]+)*$", ErrorMessage = "The last name must be 'Lastname' or 'Lastname1-Lastname2', each last name must be at least 2 characters long")]
        public string? LastName { get; set; }

        public string? ProfileImg { get; set; }
        public DateTime? RegisterDate { get; set; }

        public virtual ICollection<TeamMember>? TeamMembers { get; set; }
        public virtual ICollection<TeamMember>? TeamMembersAdded { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        public string GetProfilePicture()
        {
            if (ProfileImg == null)
            {
                int calcul = 0;
                foreach(char v in Id)
                {
                    calcul += v;
                }
                calcul %= 16;
                calcul += 1;


                ProfileImg=Path.Combine("images", "defaultPfp", "pfp" + calcul + ".svg");
            }

            return ProfileImg;
        }
    }
}
