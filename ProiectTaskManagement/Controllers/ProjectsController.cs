using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;
using System.Text.RegularExpressions;
using Project = ProiectTaskManagement.Models.Entities.Project;

namespace ProiectTaskManagement.Controllers
{

    public class ProjectsController : Controller
    {

        private readonly ApplicationDbContext db;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public ProjectsController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Index()
        {
            var MyProjectIds = from project in db.Projects
                             join member in db.TeamMembers
                                 on project.Id equals member.ProjectId
                             where member.AppUserId.Equals(_userManager.GetUserId(User)) && member.AddedByUserId == null
                             select project.Id;

            var ProjectsWithAccessIds = from project in db.Projects
                                        join member in db.TeamMembers
                                            on project.Id equals member.ProjectId
                                        where member.AppUserId.Equals(_userManager.GetUserId(User)) && member.AddedByUserId != null
                                        select project.Id;

            var CountDict = (from project in db.Projects
                            join member in db.TeamMembers
                                on project.Id equals member.ProjectId
                            group project by project.Id into proiecteGrupate
                            select new {ProjectId = proiecteGrupate.Key, Contor = proiecteGrupate.Count()} ).ToDictionary(t => t.ProjectId, t => t.Contor);

            ViewBag.MyProjects = new List<Project>();
            ViewBag.ProjectsWithAccess = new List<Project>();
            
            foreach (Project project in db.Projects.Where(p => MyProjectIds.Contains(p.Id)))
            {
                project.MemberCount = CountDict[project.Id];
                project.TeamLeadersCache = project.GetTeamLeaders(db);
                ViewBag.MyProjects.Add(project);
            }

            foreach (Project project in db.Projects.Where(p => ProjectsWithAccessIds.Contains(p.Id)))
            {
                project.MemberCount = CountDict[project.Id];
                project.TeamLeadersCache = project.GetTeamLeaders(db);
                ViewBag.ProjectsWithAccess.Add(project);
            }

            return View();
        }

        public IActionResult Show(string projectId)
        {
            try
            {
                Project? project = db.Projects
                    .Include("TeamMembers")
                    .Include("TeamMembers.AppUser")
                    .Include("Statuses")
                    .Include("Tasks")
                    .Include("Tasks.Status")
                    .Include("Tasks.TaskAssigns")
                    .Include("Tasks.TaskAssigns.TeamMember")
                    .Include("Tasks.TaskAssigns.TeamMember.AppUser")
                    .SingleOrDefault(ob => ob.Id == projectId);

                if (project == null || !User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

                var userId = _userManager.GetUserId(User);
                if (!User.IsInRole("Admin"))
                {
                    foreach (var member in project.TeamMembers)
                    {
                        if (member.AppUserId == userId)
                        {
                            ViewBag.IsLeader = member.AddedByUserId == null;
                            break;
                        }
                    }
                }

                if (!User.IsInRole("Admin") && ViewBag.IsLeader == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var tasksCommentsCount = (from comment in db.Comments where comment.ProjectId == projectId group comment by comment.TaskId into comentariiGrupate
                                         select new {TaskId = comentariiGrupate.Key, Contor = comentariiGrupate.Count()}).ToDictionary(t => t.TaskId, t => t.Contor);
                foreach (var task in project.Tasks)
                {
                    task.CountComments = tasksCommentsCount.GetValueOrDefault(task.Id, 0);
                }

                ViewBag.UserId = userId;

                return View(project);
            }
            catch(Exception x)
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            ViewBag.MemberList = new List<string>();

            return View();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ICollection<string>? MemberList, Project proj)
        {
            if (MemberList == null)
                MemberList = new List<string>();
            //hashset for duplicate elimination
            HashSet<AppUser> listaUseri = new HashSet<AppUser>();
            string userId = _userManager.GetUserId(User);
            foreach (var nume in MemberList)
            {
                if (nume.Contains('@'))
                {
                    AppUser user = await _userManager.FindByEmailAsync(email: nume);
                    if (user != null && user.Id != userId)
                        listaUseri.Add(user);
                }
                else
                {
                    AppUser user = await _userManager.FindByNameAsync(nume);
                    if (user != null && user.Id != userId)
                        listaUseri.Add(user);
                }

            }
            if (listaUseri.Count == MemberList.Count)
            {
                proj.CreationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Projects.Add(proj);


                    TeamMember newT = new TeamMember();
                    newT.AppUserId = userId;
                    newT.ProjectId = proj.Id;
                    newT.AddedByUserId = null;
                    newT.JoinDate = DateTime.Now;

                    db.TeamMembers.Add(newT);

                    foreach (AppUser user in listaUseri)
                    {
                        newT = new TeamMember();
                        newT.AppUserId = user.Id;
                        newT.ProjectId = proj.Id;
                        newT.AddedByUserId = userId;
                        newT.JoinDate = DateTime.Now;
                        db.TeamMembers.Add(newT);
                    }

                    db.SaveChanges();
                    GenerateStatuses(proj.Id);
                    TempData["Success"] = "The project "+proj.Title+" was created!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.MemberList = MemberList;
                    return View(proj);

                }
            }
            else
            {
                ViewBag.MesajEroareEchipa = "List of users provided was incorrect. Removed users that don't exist or that appeared more than once.";
                ViewBag.MemberList = new List<string>();
                foreach (var user in listaUseri)
                    ViewBag.MemberList.Add(user.UserName);


                return View(proj);
            }
        }

        [HttpPost]
        public IActionResult Delete(string? projectId)
        {
            if(projectId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {

                Project? proj = db.Projects.Include("TeamMembers").FirstOrDefault(ob => ob.Id == projectId);

                if (proj == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                    return RedirectToAction("Index", "Home");

                db.TaskAssigns.RemoveRange(db.TaskAssigns.Where(t => t.ProjectId == projectId).Select(t => t).ToList());
                db.SaveChanges();

                db.TeamMembers.RemoveRange(proj.TeamMembers);
                db.SaveChanges();

                db.Projects.Remove(proj);
                TempData["Success"] = "The project " + proj.Title + " was deleted!";
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch(Exception x)
            {
                return RedirectToAction("Index");
            }
        }


        public IActionResult Edit(string? projectId)
        {
            if (projectId == null)
                return RedirectToAction("Index");

            Project? proj = db.Projects.Find(projectId);
            if (proj == null)
                return RedirectToAction("Index");

            if (!User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            return View(proj);
        }

        [HttpPost]
        public IActionResult Edit(string projectId, Project project)
        {
            try
            {
                Project? proj = db.Projects.Find(projectId);
                if(proj == null)
                {
                    return RedirectToAction("Index");
                }

                if (!User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                    return RedirectToAction("Index", "Home");

                if (ModelState.IsValid)
                {
                    proj.CreationDate = project.CreationDate;
                    proj.Title = project.Title;

                    db.SaveChanges();
                    TempData["Success"] = "The project modifications for " + proj.Title + " were saved!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(project);
                }
            }catch(Exception x)
            {
                return View(project);
            }
        }

        [NonAction]
        private void GenerateStatuses(string projectId)
        {
            db.Statuses.AddRange(
                new Status
                {
                    ProjectId = projectId,
                    Id = 1,
                    Color = "#8fce00",
                    Name = "Done"
                },
                new Status
                {
                    ProjectId = projectId,
                    Id = 2,
                    Color = "#999999",
                    Name = "In Progress"
                },

                new Status
                {
                    ProjectId = projectId,
                    Id = 3,
                    Color = "#cc0000",
                    Name = "After Deadline"
                }
            );

            db.SaveChanges();
        }

    }
}
