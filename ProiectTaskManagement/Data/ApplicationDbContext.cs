using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProiectTaskManagement.Models.Entities;
using ProiectTaskManagement.Models.Relationships;
using Task = ProiectTaskManagement.Models.Entities.Task;

namespace ProiectTaskManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>()
                .HasKey(ob => new { ob.ProjectId, ob.Id });

            modelBuilder.Entity<Task>()
                .HasKey(ob => new { ob.ProjectId, ob.Id });

            modelBuilder.Entity<TeamMember>()
                .HasKey(ob => new { ob.ProjectId, ob.AppUserId });

            modelBuilder.Entity<TaskAssign>()
                .HasKey(ob => new { ob.ProjectId, ob.TaskId, ob.TeamMemberId });


            modelBuilder.Entity<TaskAssign>()
                .HasOne(ob => ob.TeamMember)
                .WithMany(ob => ob.TaskAssigns)
                .HasForeignKey(ob => new { ob.ProjectId, ob.TeamMemberId })
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskAssign>()
                .HasOne(ob => ob.AssignedBy)
                .WithMany(ob => ob.TasksGivenByMe)
                .HasForeignKey(ob => new { ob.ProjectId, ob.AssignedById })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskAssign>()
                .HasOne(ob => ob.Task)
                .WithMany(ob => ob.TaskAssigns)
                .HasForeignKey(ob => new { ob.ProjectId, ob.TaskId })
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(ob => ob.TeamMembers)
                .WithOne(ob => ob.AppUser)
                .HasForeignKey(ob => ob.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamMember>()
                .HasOne(ob => ob.AddedByUser)
                .WithMany(ob => ob.TeamMembersAdded)
                .HasForeignKey(ob => ob.AddedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeamMember>()
                .HasOne(ob => ob.AppUser)
                .WithMany(ob => ob.TeamMembers)
                .HasForeignKey(ob => ob.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamMember>()
                .HasOne(ob => ob.Project)
                .WithMany(ob => ob.TeamMembers)
                .HasForeignKey(ob => ob.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            /*modelBuilder.Entity<Comment>()
                .HasOne(ob => ob.Parent)
                .WithMany(ob => ob.Comments)
                .HasForeignKey(ob => ob.ParentId)
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<Comment>()
                .HasOne(ob => ob.Task)
                .WithMany(ob => ob.Comments)
                .HasForeignKey(ob => new { ob.ProjectId, ob.TaskId })
                .OnDelete(DeleteBehavior.Cascade);

            /*modelBuilder.Entity<Comment>()
                .HasOne(ob => ob.TeamMember)
                .WithMany(ob => ob.Comments)
                .HasForeignKey(ob => new { ob.ProjectId, ob.AppUserId })
                .OnDelete(DeleteBehavior.NoAction);
            */

            modelBuilder.Entity<Comment>()
                .HasOne(ob => ob.AppUser)
                .WithMany(ob => ob.Comments)
                .HasForeignKey(ob => ob.AppUserId)
                .OnDelete(DeleteBehavior.SetNull);

            /* modelBuilder.Entity<Comment>()
                 .HasOne(ob => ob.TeamMember)
                 .WithMany(ob => ob.Comments)
                 .HasForeignKey(ob => new {ob.ProjectId, ob.AppUser})
                 .OnDelete(DeleteBehavior.NoAction);
            */
            


            modelBuilder.Entity<Task>()
                .HasOne(ob => ob.Status)
                .WithMany(ob => ob.Tasks)
                .HasForeignKey(ob => new { ob.ProjectId, ob.StatusId})
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Task>()
                .HasOne(ob => ob.Project)
                .WithMany(ob => ob.Tasks)
                .HasForeignKey(ob => ob.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Status>()
                .HasOne(ob => ob.Project)
                .WithMany(ob => ob.Statuses)
                .HasForeignKey(ob => ob.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);


        }


        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TaskAssign> TaskAssigns { get; set; }
    }
}