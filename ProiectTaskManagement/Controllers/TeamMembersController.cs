using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;

namespace ProiectTaskManagement.Controllers
{
    public class TeamMembersController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamMembersController(
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
            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");
            var members = db.TeamMembers.Include("AppUser").Include("AddedByUser").Where(ob => ob.ProjectId == projectId);

            var statsCountTasks = (from assign in db.TaskAssigns
                         where assign.ProjectId == projectId
                         group assign by assign.TeamMemberId
                         into taskuriGrupate
                         select new { TeamMemberId = taskuriGrupate.Key, Contor = taskuriGrupate.Count() }).ToDictionary(t => t.TeamMemberId, t => t.Contor);

            var statsCountComments = (from comm in db.Comments
                                      where comm.ProjectId == projectId && comm.AppUserId!=null
                                      group comm by comm.AppUserId
                                      into commsGrupate
                                      select new { TeamMemberId = commsGrupate.Key, Contor = commsGrupate.Count() }).ToDictionary(t => t.TeamMemberId, t => t.Contor);


            ViewBag.Members = new List<TeamMember>();
            ViewBag.TeamLeaders = new List<TeamMember>();

            var userId = _userManager.GetUserId(User);
            ViewBag.UserId = userId;
            foreach(var member in members)
            {
                member.CommentsCountCache = statsCountComments.GetValueOrDefault(member.AppUserId, 0);
                member.TasksCountCache = statsCountTasks.GetValueOrDefault(member.AppUserId, 0);
                if (member.IsTeamLeader())
                {
                    ViewBag.TeamLeaders.Add(member);
                    if (member.AppUserId == userId)
                    {
                        ViewBag.IsLeader = true;
                    }
                }
                else
                {
                    ViewBag.Members.Add(member);
                    if (member.AppUserId == userId)
                    {
                        ViewBag.IsLeader = false;
                    }
                }
            }

            //if ViewBag.IsLeader is null
            //means it's Admin

            return View(project);
        }

        public IActionResult Create(string projectId)
        {
            var project = db.Projects.Find(projectId);
            if(project == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isAllowed;
            if (User.IsInRole("Admin"))
                isAllowed = true;
            //member that is team leader was found
            else if(db.TeamMembers.Any(ob => ob.ProjectId == projectId && ob.AddedByUserId == null && ob.AppUserId == _userManager.GetUserId(User))){
                isAllowed = true;
            }
            else
            {
                isAllowed = false;
            }

            if(isAllowed)
            {
                ViewBag.MemberList = new List<AppUser>();
                return View(project);
            }
            else
            {
                TempData["Eroare"] = "Not enough permission to do this.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string projectId, [FromForm] ICollection<string>? MemberList)
        {
            var project = db.Projects.Find(projectId);
            if (project == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isAllowed;
            if (User.IsInRole("Admin"))
                isAllowed = true;

            else if (db.TeamMembers.Any(ob => ob.ProjectId == projectId && ob.AddedByUserId == null && ob.AppUserId == _userManager.GetUserId(User)))
            {
                isAllowed = true;
            }
            else
            {
                isAllowed = false;
            }

            if (isAllowed)
            {
                if (MemberList == null)
                    MemberList = new List<string>();

                HashSet<AppUser> userList = new HashSet<AppUser>();
                string userId = _userManager.GetUserId(User);
                var users = from member in db.TeamMembers where member.ProjectId == projectId select member.AppUserId;
                foreach (var name in MemberList)
                {
                    if (name.Contains('@'))
                    {
                        AppUser user = await _userManager.FindByEmailAsync(email: name);
                        if (user != null && !users.Contains(user.Id))
                            userList.Add(user);
                    }
                    else
                    {
                        AppUser user = await _userManager.FindByNameAsync(name);
                        if (user != null && !users.Contains(user.Id))
                            userList.Add(user);
                    }

                }
                if (MemberList.Count != 0 && userList.Count == MemberList.Count)
                {
                    TeamMember newT;

                    foreach (AppUser user in userList)
                    {
                        newT = new TeamMember();
                        newT.AppUserId = user.Id;
                        newT.ProjectId = projectId;
                        newT.AddedByUserId = userId;
                        newT.JoinDate = DateTime.Now;
                        db.TeamMembers.Add(newT);
                    }

                    db.SaveChanges();
                    TempData["Success"] = "The team members list was modified!";
                    return RedirectToAction("Index", new {@projectId=projectId});
                }
                else
                {
                    ViewBag.Eroare = "List of users provided was incorrect. Removed users that don't exist or are already in the team.";
                    List<string> usr = new List<string>();
                    foreach (var user in userList)
                        usr.Add(user.UserName);
                    ViewBag.MemberList = usr;
                    return View(project);
                }
            }
            else
            {
                TempData["Eroare"] = "Not enough permission to do this.";
                return RedirectToAction("Index", new { @projectId = projectId });
            }
        }

        [HttpPost]
        public IActionResult Delete(string projectId, string memberId)
        {
            var project = db.Projects.Find(projectId);
            if (project == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isAllowed;
            if (User.IsInRole("Admin"))
                isAllowed = true;
            //am gasit membru care indeplineste toate cerintele sa fie leader de echipa
            else if (db.TeamMembers.Any(ob => ob.ProjectId == projectId && ob.AddedByUserId == null && ob.AppUserId == _userManager.GetUserId(User)))
            {
                isAllowed = true;
            }
            else
            {
                isAllowed = false;
            }

            if (isAllowed)
            {
                TeamMember? member = db.TeamMembers.FirstOrDefault(m => m.ProjectId == projectId && m.AppUserId == memberId);
                if(member == null)
                {
                    TempData["Eroare"] = "The user is not part of the team";
                }
                else if(member.AddedByUserId == null)
                {
                    TempData["Eroare"] = "The user is a leader and can not be removed.";
                }
                else
                {
                    foreach (var task in db.TaskAssigns.Where(t => t.ProjectId == projectId && t.TeamMemberId == memberId))
                        task.AssignedById = null;
                    db.SaveChanges();
                    db.TeamMembers.Remove(member);
                    db.SaveChanges();
                    TempData["Success"] = "User was successfully removed from team!";
                }
                return RedirectToAction("Index", new { @projectId = projectId });
            }
            else
            {
                TempData["Eroare"] = "Not enough permission to do this.";
                return RedirectToAction("Index", new { @projectId = projectId });
            }
        }

        [HttpPost]
        public IActionResult InviteLeader(string projectId, string memberId)
        {
            var project = db.Projects.Find(projectId);
            if (project == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if(User.IsInRole("Admin") || db.TeamMembers.Any(ob => ob.ProjectId == projectId && ob.AddedByUserId == null && ob.AppUserId == _userManager.GetUserId(User)))
            {
                TeamMember? member = db.TeamMembers.FirstOrDefault(m => m.ProjectId == projectId && m.AppUserId == memberId);
                if(member == null)
                {
                    TempData["Eroare"] = "The user is not a member of the team.";
                    return RedirectToAction("Index", new { @projectId = projectId });
                }

                if(member.AddedByUserId == null)
                {
                    TempData["Eroare"] = "The user is already a team leader.";
                    return RedirectToAction("Index", new { @projectId = projectId });
                }

                member.AddedByUserId = null;
                db.SaveChanges();
                TempData["Success"] = "User was promoted to team leader!";
            }
            else
            {
                TempData["Eroare"] = "You don't have permission to add a leader.";
            }
            return RedirectToAction("Index", new { @projectId = projectId });
        }

        [HttpPost]
        public IActionResult DropLeadership(string projectId, string projectLeaderId)
        {
            var project = db.Projects.Find(projectId);
            if (project == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var leader = db.TeamMembers.FirstOrDefault(m => m.ProjectId == projectId && m.AppUserId == projectLeaderId);
            if(leader == null)
            {
                TempData["Eroare"] = "That user is not a member of the team.";
                return RedirectToAction("Index", new { @projectId = projectId });
            }

            var userId = _userManager.GetUserId(User);
            if (User.IsInRole("Admin") || leader.AppUserId == userId)
            {
                if (leader.AddedByUserId != null)
                {
                    TempData["Eroare"] = "The user is not a team leader.";
                    return RedirectToAction("Index", new { @projectId = projectId });
                }
                var leaderIds = from leader1 in db.TeamMembers where leader1.ProjectId == projectId && leader1.AddedByUserId == null && leader1.AppUserId != projectLeaderId select leader1.AppUserId;
                if(leaderIds.Count() > 0)
                {
                    leader.AddedByUserId = leaderIds.First();
                    db.SaveChanges();
                    TempData["Success"] = "User is no longer a team leader!";
                }
                else
                {
                    TempData["Eroare"] = "There needs to be at least one leader, before dropping leadership add another leader.";
                }
                return RedirectToAction("Index", new { @projectId = projectId });
            }
            else
            {
                TempData["Eroare"] = "You do not have permission to drop leadership for that user.";
                return RedirectToAction("Index", new { @projectId = projectId });
            }
        }

        [HttpPost]
        public IActionResult Leave(string projectId)
        {
            var project = db.Projects.Find(projectId);
            if (project == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var member = db.TeamMembers.FirstOrDefault(m => m.ProjectId == projectId && m.AppUserId == _userManager.GetUserId(User));
            if (member == null)
            {
                TempData["Eroare"] = "You are not part of this team.";
                return RedirectToAction("Index", new { @projectId = projectId });
            }


            if (member.AddedByUserId == null)
            {
                TempData["Eroare"] = "You are a team leader, you need to drop leadership first.";
                return RedirectToAction("Index", new { @projectId = projectId });
            }

            db.TeamMembers.Remove(member);
            db.SaveChanges();
            TempData["Success"] = "You successfully left the team of project " + project.Title;
            
            return RedirectToAction("Index", "Projects");
        }
    }
}
