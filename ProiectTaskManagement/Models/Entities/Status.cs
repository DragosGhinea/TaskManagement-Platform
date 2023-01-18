using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectTaskManagement.Models.Entities
{
    public class Status
    {
        public string ProjectId { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Status color is required")]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "The color format needs to be HEX")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Status name required")]
        [MaxLength(30, ErrorMessage = "Status name can be maximum 30 characters long")]
        public string Name { get; set; }

        public virtual Project? Project { get; set; }   
        public virtual ICollection<Task>? Tasks { get; set; }
    }
}
