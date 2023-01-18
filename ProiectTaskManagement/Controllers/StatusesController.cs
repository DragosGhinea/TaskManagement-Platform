using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;

namespace ProiectTaskManagement.Controllers
{
    public class StatusesController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public StatusesController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }
        public IActionResult Index(string projectId)
        {
            var project = db.Projects.Include("Statuses").FirstOrDefault(p => p.Id == projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            Status newStatus = new Status();
            newStatus.ProjectId = projectId;
            ViewBag.NewStatus = newStatus;

            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(string projectId, int statusId, Status status)
        {
            var project = db.Projects.Include("Statuses").FirstOrDefault(p => p.Id == projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            var statusE = db.Statuses.FirstOrDefault(s => s.ProjectId == projectId && s.Id == statusId);
            if(statusE == null)
            {
                TempData["Eroare"] = "That status does not exist.";
                return RedirectToAction("Index", "Statuses", new { @projectId = projectId });
            }

            if (ModelState.IsValid)
            {
                statusE.Name = status.Name;
                statusE.Color = status.Color;
                db.SaveChanges();
                TempData["Success"] = "Status modified successfully!";
                return RedirectToAction("Index", "Statuses", new { @projectId = projectId });
            }
            else
            {
                string messages = string.Join("<br>", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                TempData["Eroare"] = "Errors trying to edit <b>" + new HtmlSanitizer().Sanitize(statusE.Name) + "</b>:<br>" + messages;
                return RedirectToAction("Index", "Statuses", new { @projectId = projectId });
            }
        }

        [HttpPost]
        public IActionResult Delete(string projectId, int statusId, Status status)
        {
            var project = db.Projects.Include("Statuses").FirstOrDefault(p => p.Id == projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            var statusE = db.Statuses.FirstOrDefault(s => s.ProjectId == projectId && s.Id == statusId);
            if (statusE == null)
            {
                TempData["Eroare"] = "That status does not exist.";
                return RedirectToAction("Index", "Statuses", new { @projectId = projectId });
            }

            var tasks = db.Tasks.Where(t => t.StatusId == statusId).Select(t => t);
            foreach(var task in tasks)
            {
                task.StatusId = null;
            }
            db.Statuses.Remove(statusE);
            db.SaveChanges();
            TempData["Success"] = "Status " + statusE.Name + " was deleted!";
            return RedirectToAction("Index", "Statuses", new { @projectId = projectId });
        }

        [HttpPost]
        public IActionResult Create(string projectId, Status status)
        {
            var project = db.Projects.Include("Statuses").FirstOrDefault(p => p.Id == projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                int id;
                try
                {
                    id = db.Statuses.Where(s => s.ProjectId == projectId).Max(s => s.Id);
                }
                catch (Exception)
                {
                    id = 0;
                }
                status.Id = id + 1;
                db.Statuses.Add(status);
                db.SaveChanges();
                TempData["Success"] = "New status " + status.Name + " created and added to project!";
                return RedirectToAction("Index", "Statuses", new { @projectId = projectId });
            }
            else
            {
                string messages = string.Join("<br>", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                TempData["Eroare"] = "Errors trying to create a new status:<br>" + messages;
                return RedirectToAction("Index", "Statuses", new { @projectId = projectId });
            }
        }
    }
}
