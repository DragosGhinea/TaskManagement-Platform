
using ProiectTaskManagement.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task = ProiectTaskManagement.Models.Entities.Task;

namespace ProiectTaskManagement.Models.Relationships
{
    public class TaskAssign
    {
        public string ProjectId { get; set; }
        public int TaskId { get; set; }

        
        public string TeamMemberId { get; set; }
        
        public string? AssignedById { get; set; }


        public virtual Task? Task { get; set; }
        public virtual TeamMember? TeamMember { get; set; }
        public virtual TeamMember? AssignedBy { get; set; }
    }
}
