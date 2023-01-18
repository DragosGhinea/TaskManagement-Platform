using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectTaskManagement.Models.Entities
{
    public class Comment
    {
        [Key]
        public string? CommentId { get; set; }

        public string ProjectId { get; set; }
        public int TaskId { get; set; }
        public string? AppUserId { get; set; }

        [Required(ErrorMessage = "Comment content required.")]
        [MinLength(20, ErrorMessage = "Content needs to be at least 20 characters.")]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey("Parent")]
        public string? ParentId { get; set; }

        public virtual TeamMember? TeamMember { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual Task? Task { get; set; }

        public virtual Comment? Parent { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
