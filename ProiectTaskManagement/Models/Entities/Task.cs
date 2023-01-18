
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectTaskManagement.Models.Relationships;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectTaskManagement.Models.Entities
{
    public class Task
    {
        public string ProjectId { get; set; }

        public int Id;

        [Required(ErrorMessage = "Task title required")]
        [MinLength(4, ErrorMessage = "Task title needs to be at least 4 characters long")]
        [MaxLength(30, ErrorMessage = "Task title needs to be at most 30 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Task description is required")]
        [MinLength(20, ErrorMessage = "Description needs to be at least 20 characters long")]
        public string Description { get; set; }
        
        public int? StatusId { get; set; }

        [Required(ErrorMessage = "Task start date required")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual Status? Status { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<TaskAssign>? TaskAssigns { get; set; }

        [NotMapped]
        public int? CountComments;

        [NotMapped]
        public IEnumerable<SelectListItem>? Statuses { get; set; }
    }
}
