using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectTaskManagement.Models.Entities
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Give the project a title")]
        [MinLength(3, ErrorMessage = "The title needs to be at least 3 characters")]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        public int TasksContor { get; set; }

        public virtual ICollection<TeamMember>? TeamMembers { get; set; }
    
        public virtual ICollection<Task>? Tasks { get; set; }
        public virtual ICollection<Status>? Statuses { get; set; }

        public ICollection<TeamMember> GetTeamLeaders(ApplicationDbContext db)
        {
            return db.TeamMembers.Include("AppUser").Where(member => member.AddedByUserId == null && member.ProjectId == Id).ToList();
        }

        [NotMapped]
        public int? MemberCount;

        [NotMapped]
        public ICollection<TeamMember>? TeamLeadersCache { get; set; }
    }
}
