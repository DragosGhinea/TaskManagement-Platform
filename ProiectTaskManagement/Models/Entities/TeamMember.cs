
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Relationships;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectTaskManagement.Models.Entities
{
    public class TeamMember
    {
        public string ProjectId { get; set; }

        [Required(ErrorMessage = "User required")]
        public string AppUserId { get; set; }
        public string? AddedByUserId { get; set; }

        public DateTime JoinDate { get; set; }

        public bool IsTeamLeader()
        {
            return AddedByUserId == null;
        }

        public virtual ICollection<TaskAssign> TaskAssigns { get; set; }

        public virtual ICollection<TaskAssign> TasksGivenByMe { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }
        public ICollection<Comment> GetComments(ApplicationDbContext db)
        {
            return db.Comments.Where(comm => comm.ProjectId == ProjectId && comm.AppUserId == AppUserId).ToList();
        }

        public virtual Project? Project { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual AppUser? AddedByUser { get; set; }

        [NotMapped]
        public int? CommentsCountCache { get; set; }
        [NotMapped]
        public int? TasksCountCache { get; set; }
    }
}
