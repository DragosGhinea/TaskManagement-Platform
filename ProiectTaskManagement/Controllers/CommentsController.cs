using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;
using System.Threading.Tasks;

namespace ProiectTaskManagement.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public CommentsController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        [HttpPost]
        public IActionResult Create(string projectId, int taskId, string RepliedTo, string Continut)
        {
            var project = db.Projects.Find(projectId);

            TeamMember? teamMember = db.TeamMembers.FirstOrDefault(t => t.ProjectId == projectId && t.AppUserId == _userManager.GetUserId(User));
            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && teamMember == null))
                return RedirectToAction("Index", "Home");

            string? parentComment;
            if(RepliedTo == "null")
                parentComment = null;
            else
                parentComment = RepliedTo;

            Comment comment = new Comment();
            comment.CommentId = Guid.NewGuid().ToString();
            comment.CreationDate = DateTime.Now;
            comment.ProjectId = projectId;
            comment.TaskId = taskId;
            comment.Content = Continut;
            comment.ParentId = parentComment;
            comment.AppUserId = _userManager.GetUserId(User);
            if (TryValidateModel(comment))
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                TempData["Success"] = "Your new comment was added!";
            }
            else
            {
                string messages = string.Join("<br>", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                TempData["Eroare"] = "Errors trying to add a new comment:<br>" + messages;
            }

            return RedirectToAction("Show", "Tasks", new { projectId, taskId });
        }

        [HttpPost]
        public IActionResult Delete(string commentId)
        {
            var comment = db.Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                return RedirectToAction("Index", "Home");
            if(User.IsInRole("Admin") || comment.AppUserId == _userManager.GetUserId(User) || db.TeamMembers.Any(t => t.ProjectId == comment.ProjectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User)))
            {
                var childComms = db.Comments.Where(c => c.ParentId== commentId);
                foreach(var child in childComms)
                {
                    child.ParentId = null;
                }
                db.Comments.Remove(comment);
                db.SaveChanges();
                TempData["Success"] = "Comment deleted!";
            }

            try
            {
                return RedirectToAction("Show", "Tasks", new { comment.ProjectId, comment.TaskId });
            }catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Edit(string commentId)
        {
            var comment = db.Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                return RedirectToAction("Index", "Home");
            if (User.IsInRole("Admin") || comment.AppUserId == _userManager.GetUserId(User) || db.TeamMembers.Any(t => t.ProjectId == comment.ProjectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User)))
            {
                return View(comment);
            }

            try
            {
                return RedirectToAction("Show", "Tasks", new { comment.ProjectId, comment.TaskId });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Edit(string commentId, Comment modifiedComm)
        {
            var comment = db.Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                return RedirectToAction("Index", "Home");

            if (User.IsInRole("Admin") || db.TeamMembers.Any(t => t.ProjectId == comment.ProjectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User)) || (comment.AppUserId == _userManager.GetUserId(User)) && db.TeamMembers.Any(t => t.ProjectId == comment.ProjectId && t.AppUserId == _userManager.GetUserId(User)))
            {
                if (ModelState.IsValid)
                {
                    comment.Content = modifiedComm.Content;
                    db.SaveChanges();
                    TempData["Success"] = "Comment edited!";
                }
                else
                {
                    return View(modifiedComm);
                }
            }

            try
            {
                return RedirectToAction("Show", "Tasks", new { comment.ProjectId, comment.TaskId });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
