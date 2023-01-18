using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;

namespace ProiectTaskManagement.Controllers
{
    public class PanelController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public PanelController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Projects()
        {
            if(!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            var search = "";

            if (Convert.ToString(HttpContext.Request.Query["search"]) != null)
            {
                search = Convert.ToString(HttpContext.Request.Query["search"]).Trim();
            }

            if (search == "")
            {
                ViewBag.BaseUrl = "/panel/projects?page";
            }
            else
                ViewBag.BaseUrl = "/panel/projects?search=" + search + "&page";

            ViewBag.Search = search;

            int perPageElements = 3;

            int currentPage;
            try
            {
                currentPage = Convert.ToInt32(HttpContext.Request.Query["page"]);
            }
            catch (Exception)
            {
                currentPage = 1;
            }

            if (currentPage == 0)
                currentPage = 1;

            var offset = 0;

            offset = (currentPage - 1) * perPageElements;



            ViewBag.currentPage = currentPage;
            if (search.Length > 1)
            {
                var projectsWithUsers = db.TeamMembers.Include("AppUser").Where(t => 
                    t.AppUser.UserName.Contains(search)
                    || t.AppUser.Email.Contains(search)
                    || t.AppUser.FirstName.Contains(search)
                    || t.AppUser.LastName.Contains(search)
                    ).Include("Project").Select(t => t.Project).ToHashSet();

                var projects1 = db.Projects.Where(p => p.Title.Contains(search)).Select(p => p).ToList();
                var projectsTotal = projectsWithUsers.Union(projects1);
                ViewBag.lastPage = Math.Ceiling((float)projectsTotal.Count() / (float)perPageElements);
                var projects = projectsTotal.Skip(offset).Take(perPageElements);
                foreach (Project project in projects)
                {
                    project.MemberCount = (from p in db.TeamMembers where p.ProjectId == project.Id select project.Id).ToList().Count();
                    project.TeamLeadersCache = project.GetTeamLeaders(db);
                }

                ViewBag.Projects = projects;
            }
            else
            {
                ViewBag.lastPage = Math.Ceiling((float)db.Projects.Count() / (float)perPageElements);

                var projects = db.Projects.Select(p => p).Skip(offset).Take(perPageElements).ToList();

                foreach (Project project in projects)
                {
                    project.MemberCount = (from p in db.TeamMembers where p.ProjectId == project.Id select project.Id).ToList().Count();
                    project.TeamLeadersCache = project.GetTeamLeaders(db);
                }

                ViewBag.Projects = projects;
            }
            return View();
        }

        public IActionResult AppUsers()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            var search = "";

            if (Convert.ToString(HttpContext.Request.Query["search"]) != null)
            {
                search = Convert.ToString(HttpContext.Request.Query["search"]).Trim();
            }

            if (search == "")
            {
                ViewBag.BaseUrl = "/panel/users?page";
            }
            else
                ViewBag.BaseUrl = "/panel/users?search=" + search + "&page";

            ViewBag.Search = search;

            int perPageElements = 3;

            int currentPage;
            try
            {
                currentPage = Convert.ToInt32(HttpContext.Request.Query["page"]);
            }
            catch (Exception)
            {
                currentPage = 1;
            }

            if (currentPage == 0)
                currentPage = 1;

            var offset = 0;

            offset = (currentPage - 1) * perPageElements;

            ViewBag.currentPage = currentPage;

            if (search.Length > 1)
            {
                ViewBag.lastPage = Math.Ceiling((float)db.AppUsers.Where(p => p.Email.Contains(search) || p.UserName.Contains(search) || p.FirstName.Contains(search) || p.LastName.Contains(search)).Count() / (float)perPageElements);
            }
            else
            {
                ViewBag.lastPage = Math.Ceiling((float)db.AppUsers.Count() / (float)perPageElements);
            }

            List<AppUser> appUsers;
            if (search.Length > 1)
            {
                appUsers = db.AppUsers.Where(p => p.Email.Contains(search) || p.UserName.Contains(search) || p.FirstName.Contains(search) || p.LastName.Contains(search)).Select(p => p).Skip(offset).Take(perPageElements).ToList();
            }
            else
            {
                appUsers = db.AppUsers.Select(p => p).Skip(offset).Take(perPageElements).ToList();
            }

            ViewBag.Users = appUsers;
            return View();
        }
    }
}
