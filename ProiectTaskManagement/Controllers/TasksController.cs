using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;
using ProiectTaskManagement.Models.Relationships;
using Project = ProiectTaskManagement.Models.Entities.Project;
using Task = ProiectTaskManagement.Models.Entities.Task;

namespace ProiectTaskManagement.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public TasksController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        public IActionResult Show(string projectId, int taskId)
        {
            var project = db.Projects.Find(projectId);

            TeamMember? teamMember = db.TeamMembers.FirstOrDefault(t => t.ProjectId == projectId && t.AppUserId == _userManager.GetUserId(User));
            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && teamMember == null))
                return RedirectToAction("Index", "Home");

            Task? taskk = db.Tasks.FirstOrDefault(ob => ob.Id == taskId && ob.ProjectId == projectId);

            if (taskk == null)
                return RedirectToAction("Show", "Projects", new { @projectId = projectId });

            try
            {
                Models.Entities.Task task = db.Tasks.Include("Project").Include("Comments").Include("Comments.AppUser").Where(ob => ob.ProjectId == projectId && ob.Id == taskId).Single();
                //in mod normal am face o procedura la nivel de baza de date
                //dar ne multumim cu ce stim sa facem acum

                var comentarii = new List<Comment>(task.Comments);
                var cache = new List<Comment>(task.Comments);
                while (cache.Count > 0)
                {
                    var local = new List<Comment>();
                    foreach (Comment comment in cache)
                    {
                        //var localLocal = from comment2 in db.Comments join usr in db.AppUsers on comment2.AppUserId equals usr.Id where comment2.ParentId == comment.CommentId select new { Comment = comment2, Usr = usr };
                        var localLocal = (from comment2 in db.Comments where comment2.ParentId == comment.CommentId select comment2).Include("AppUser");
                        foreach (var comm in localLocal)
                        {
                            comm.Parent = comment;
                        }
                        
                        local.AddRange(localLocal);
                    }
                    
                    cache.Clear();
                    cache.AddRange(local);
                }

                comentarii.Sort(
                        delegate(Comment c1, Comment c2)
                        {
                            return c1.CreationDate.CompareTo(c2.CreationDate);
                        }
                    );

                
                ViewBag.CacheComments = comentarii;
                if(User.IsInRole("Admin") || teamMember.IsTeamLeader())
                {
                    //id-ul este null daca poate sterge/edita orice comentariu
                    ViewBag.UserId = null;
                }
                else
                {
                    //id-ul este user-ul in caz contrar
                    ViewBag.UserId = teamMember.AppUserId;
                }

                return View(task);
            }catch(Exception x)
            {
                return RedirectToAction("Show", "Projects", new {@projectId=projectId});
            }
        }


        [HttpPost]
        public IActionResult Delete(int taskId, string projectId)
        {
            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            Task? taskk = db.Tasks.FirstOrDefault(ob => ob.Id == taskId && ob.ProjectId == projectId);

            if (taskk == null)
                return RedirectToAction("Show", "Projects", new { @projectId = projectId });

            db.Tasks.Remove(taskk);
            db.SaveChanges();
            TempData["Success"] = "The task " + taskk.Title + " was deleted!";

            return RedirectToAction("Show", "Projects", new { @projectId = projectId });
        }



        public async Task<IActionResult> Edit(string projectId, int taskId)
        {
            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            Task? taskk = db.Tasks.FirstOrDefault(ob => ob.Id == taskId && ob.ProjectId == projectId);
            
            if (taskk == null)
                return RedirectToAction("Show", "Projects", new {@projectId=projectId});


            ViewBag.MemberList = new List<string>();
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectTitle = project.Title;

            foreach (var member in db.TaskAssigns.Where(ob => ob.ProjectId == projectId && ob.TaskId == taskId).Select(n => n.TeamMemberId).ToList())
            {
                var User = await _userManager.FindByIdAsync(member);
                if (User != null)
                {
                    ViewBag.MemberList.Add(User.UserName);
                }
            }

            taskk.Statuses = GetStatuses(projectId);
            return View(taskk);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string projectId, int taskId, Task new_task, [FromForm] ICollection<string> MemberList)
        {
            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            Task? taskk = db.Tasks.FirstOrDefault(ob => ob.Id == taskId && ob.ProjectId == projectId);

            if (taskk == null)
                return RedirectToAction("Show", "Projects", new { @projectId = projectId });

            List<AppUser> userList = new List<AppUser>();
            List<String> userListProject = db.TeamMembers.Where(ob => ob.ProjectId == projectId).Select(n => n.AppUserId).ToList();

            foreach (var nume in MemberList)
            {
                if (nume.Contains('@'))
                {
                    AppUser usertemp = await _userManager.FindByEmailAsync(email: nume);
                    if (usertemp != null)
                        if (userListProject.Contains(usertemp.Id))
                            userList.Add(usertemp);
                }
                else
                {
                    AppUser usertemp = await _userManager.FindByNameAsync(nume);
                    if (usertemp != null)
                        if (userListProject.Contains(usertemp.Id))
                            userList.Add(usertemp);
                }

            }

            if (userList.Count == MemberList.Count)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        taskk.Title = new_task.Title;
                        taskk.Description = new HtmlSanitizer().Sanitize(new_task.Description);
                        taskk.StatusId = new_task.StatusId;
                        taskk.EndDate = new_task.EndDate;
                        taskk.StartDate = new_task.StartDate;

                        db.SaveChanges();
                        var assigns = db.TaskAssigns.Where(t => t.ProjectId == projectId && t.TaskId == taskId).Select(t => t);
                        
                        //for each assign that exists
                        foreach(var assign in assigns)
                        {
                            AppUser? user = null;
                            //verify that the user is still assigned
                            foreach(var u in userList)
                            {
                                if(u.Id == assign.TeamMemberId)
                                {
                                    user = u; break;
                                }
                            }

                            //user not found, meaning they no longer belong to the task
                            if (user == null)
                            {
                                db.TaskAssigns.Remove(assign);
                            }
                            //user found, we remove it from our list
                            //that will insert the users that are not assigned yet
                            else
                            {
                                userList.Remove(user);
                            }
                        }

                        //if admin, not a team member so will be added to the task by a null member
                        string? addedBy = User.IsInRole("Admin") ? null : _userManager.GetUserId(User);
                        foreach(var u in userList)
                        {
                            TaskAssign t = new TaskAssign();
                            t.AssignedById = addedBy;
                            t.ProjectId = projectId;
                            t.TaskId = taskId;
                            t.TeamMemberId = u.Id; //users that reach this code are team members guaranteed
                                                   //because they are verified when added in userList
                            db.TaskAssigns.Add(t);
                        }

                        db.SaveChanges();
                        TempData["Success"] = "The modifications for task " + taskk.Title + " were saved!";
                        return RedirectToAction("Show", "Projects", new { @projectId = projectId });
                    }
                    else
                    {
                        ViewBag.MemberList = new List<string>();
                        foreach (var usertemp in userList)
                            ViewBag.MemberList.Add(usertemp.UserName);
                        ViewBag.ProjectId = projectId;
                        ViewBag.ProjectTitle = project.Title;
                        taskk.Statuses = GetStatuses(projectId);
                        return View(taskk);
                    }
                }
                catch (Exception x)
                {
                    ViewBag.MesajEroareEchipa = "The list of users you provided was incorrect, removed users that are not part of the team or that are duplicated.";
                    ViewBag.MemberList = new List<string>();
                    foreach (var usertemp in userList)
                        ViewBag.MemberList.Add(usertemp.UserName);
                    ViewBag.ProjectId = projectId;
                    ViewBag.ProjectTitle = project.Title;
                    new_task.Statuses = GetStatuses(projectId);
                    return View(new_task);
                }
            }
            else
            {
                ViewBag.MesajEroareEchipa = "The list of users you provided was incorrect, removed users that are not part of the team or that are duplicated.";
                ViewBag.MemberList = new List<string>();
                foreach (var usertemp in userList)
                    ViewBag.MemberList.Add(usertemp.UserName);
                ViewBag.ProjectId = projectId;
                ViewBag.ProjectTitle = project.Title;
                new_task.Statuses = GetStatuses(projectId);
                return View(new_task);
            }
        }


        public IActionResult Create(string projectId)
        {
            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            ViewBag.MemberList = new List<string>();
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectTitle = project.Title;

            Task t = new Task();
            t.Statuses = GetStatuses(projectId);
            return View(t);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ICollection<string>? MemberList, Task new_task, string projectId)
        {
            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            List<AppUser> userList = new List<AppUser>();
            List<string> userListProject = db.TeamMembers.Where(ob => ob.ProjectId == projectId).Select(n => n.AppUserId).ToList();
            

            foreach (var name in MemberList)
            {
                if (name.Contains('@'))
                {
                    AppUser usertemp = await _userManager.FindByEmailAsync(email: name);
                    if (usertemp != null)
                        if (userListProject.Contains(usertemp.Id))
                            userList.Add(usertemp);
                }
                else
                {
                    AppUser usertemp = await _userManager.FindByNameAsync(name);
                    if (usertemp != null)
                        if (userListProject.Contains(usertemp.Id))
                            userList.Add(usertemp);
                }

            }

            if (userList.Count == MemberList.Count)
            {
             
                if (ModelState.IsValid)
                {
                    new_task.Description = new HtmlSanitizer().Sanitize(new_task.Description);
                    new_task.Id = ++project.TasksContor;
                    db.Tasks.Add(new_task);
                    db.SaveChanges();
                    var userId = _userManager.GetUserId(User);

                    //daca este admin, nu este team member deci o sa fie adaugat de un team member null
                    string? adaugatDe = User.IsInRole("Admin") ? null : userId;
                    foreach (AppUser usertemp in userList)
                    {
                        TaskAssign intrare = new TaskAssign();
                        intrare.AssignedById = adaugatDe;
                        intrare.TeamMemberId = usertemp.Id;
                        intrare.TaskId = new_task.Id;
                        intrare.ProjectId = projectId;
                        
                        db.TaskAssigns.Add(intrare);
                    }

                    db.SaveChanges();
                    

                    TempData["Success"] = "New task " + new_task.Title + " was created!";
                    return RedirectToAction("Show", "Projects", new { @projectId = projectId });
                }
                else
                {
                    if (MemberList == null)
                    {
                        ViewBag.MemberList = new List<string>();
                    }
                    else
                        ViewBag.MemberList = MemberList;
                    ViewBag.ProjectId = projectId;
                    ViewBag.ProjectTitle = project.Title;
                    new_task.Statuses = GetStatuses(projectId);
                    return View(new_task);

                }
            }
            else
            {
                ViewBag.MesajEroareEchipa = "The list of users you provided was incorrect, removed users that are not part of the team or that are duplicated.";
                ViewBag.MemberList = new List<string>();
                foreach (var usertemp in userList)
                    ViewBag.MemberList.Add(usertemp.UserName);
                ViewBag.ProjectId = projectId;
                ViewBag.ProjectTitle = project.Title;
                new_task.Statuses = GetStatuses(projectId);
                return View(new_task);
            }
        }


        [HttpPost]
        public IActionResult Assign([FromForm] ICollection<string> MembersToRemove, [FromForm] ICollection<string> MembersToAdd, string projectId, int taskId)
        {

            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin") && !db.TeamMembers.Any(t => t.ProjectId == projectId && t.AddedByUserId == null && t.AppUserId == _userManager.GetUserId(User))))
                return RedirectToAction("Index", "Home");

            var assignsToRemove = db.TaskAssigns.Where(t => t.ProjectId == projectId && t.TaskId == taskId && MembersToRemove.Contains(t.TeamMemberId)).Select(t => t).ToList();
            var assignsToAddThatExist = db.TaskAssigns.Where(t => t.ProjectId == projectId && t.TaskId == taskId && MembersToAdd.Contains(t.TeamMemberId)).Select(t => t).ToList();
            db.TaskAssigns.RemoveRange(assignsToRemove);

            var members = db.TeamMembers.Where(t => t.ProjectId == projectId).Select(t => t.AppUserId).ToList();
            
            string? addedBy = User.IsInRole("Admin") ? null : _userManager.GetUserId(User);
            foreach (var member in MembersToAdd)
            {
                //we ensure the member is in team
                if(!members.Contains(member))
                    continue;

                //remove duplicates from TaskAssign
                if (assignsToAddThatExist.Exists(a => a.TeamMemberId == member))
                    continue;

                TaskAssign nou = new TaskAssign();
                nou.ProjectId = projectId;
                nou.TaskId = taskId;
                nou.AssignedById = addedBy;
                nou.TeamMemberId = member;
                db.TaskAssigns.Add(nou);
            }

            db.SaveChanges();
            TempData["Success"] = "List of assigned people was modified!";

            return RedirectToAction("Show", "Projects", new { @projectId = projectId });
        }

        public IActionResult UpdateStatus(string projectId, int taskId, int newStatusId)
        {
            var project = db.Projects.Find(projectId);

            if (project == null || !User.Identity.IsAuthenticated || (!User.IsInRole("Admin")))
            {
                TeamMember? member = db.TeamMembers.Include("TaskAssigns").FirstOrDefault(t => t.ProjectId == projectId && t.AppUserId == _userManager.GetUserId(User));
                if(member == null)
                    return RedirectToAction("Index", "Home");

                if (!member.IsTeamLeader())
                {
                    bool areTask = false;
                    foreach(var assign in member.TaskAssigns)
                    {
                        if(assign.TaskId == taskId)
                        {
                            areTask = true;
                            break;
                        }
                    }
                    if (!areTask)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            Task? task = db.Tasks.FirstOrDefault(t => t.ProjectId == projectId && t.Id== taskId);
            if (task == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Status? status = db.Statuses.FirstOrDefault(s => s.ProjectId == projectId && s.Id == newStatusId);
            if(status == null)
            {
                return RedirectToAction("Index", "Home");
            }

            task.StatusId = newStatusId;
            db.SaveChanges();
            TempData["Success"] = "Changed status to " + status.Name + " for task " + task.Title+"!";

            return RedirectToAction("Show", "Projects", new { @projectId = projectId });
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetStatuses(string projectId)
        {
            var toReturn = new List<SelectListItem>();
            foreach(var s in db.Statuses.Where(ob => ob.ProjectId == projectId).Select(ob => ob))
            {
                toReturn.Add(new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                });
            }
            return toReturn;
        }

    }
}
