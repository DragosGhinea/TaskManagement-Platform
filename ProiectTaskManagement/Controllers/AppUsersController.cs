using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace ProiectTaskManagement.Controllers
{
    public class AppUsersController : Controller
    {

        private readonly ApplicationDbContext db;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AppUsersController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment webHostEnvironment
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;

            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string appUserId)
        {
            AppUser user = db.AppUsers.Find(appUserId);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if(user.Id == _userManager.GetUserId(User))
            {
                return RedirectToAction("AppUsers", "Panel");
            }

            var task_assignsGiven = db.TaskAssigns.Where(task => task.AssignedById == appUserId);

            foreach (var task in task_assignsGiven)
            {
                task.AssignedById = null;
            }

            db.SaveChanges();

            var projects = db.Projects.Include("TeamMembers").Where(p => p.TeamMembers.Any(t => t.AppUserId == appUserId && t.AddedByUserId == null)).Select(p => p).ToList();
            foreach(var project in projects)
            {
                if (project.TeamMembers.Count() == 1)
                {
                    db.TeamMembers.Remove(project.TeamMembers.First());
                    db.Projects.Remove(project);
                }
                else
                {
                    var otherLeaders = new List<TeamMember>();
                    foreach(var member in project.TeamMembers)
                    {
                        if(member.AddedByUserId == null && member.AppUserId != appUserId)
                        {
                            otherLeaders.Add(member);
                        }
                    }
                    if(otherLeaders.Count() == 0)
                    {
                        foreach(var member in project.TeamMembers)
                        {
                            if(member.AppUserId != appUserId)
                            {
                                member.AddedByUserId = null;
                                break;
                            }
                        }
                    }
                }
            }

            await _userManager.DeleteAsync(user);
            db.SaveChanges();

            return RedirectToAction("AppUsers", "Panel");
        }

        public async Task<IActionResult> DeleteProfilePicture()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (user.ProfileImg != null)
            {
                FileInfo pfp = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, user.ProfileImg));
                if (pfp.Exists && !pfp.DirectoryName.Contains("defaultPfp"))
                {
                    pfp.Delete();
                    int calculate = 0;

                    foreach (char v in user.Id)
                    {
                        calculate += v;
                    }
                    calculate %= 16;
                    calculate += 1;


                    user.ProfileImg = Path.Combine("images", "defaultPfp", "pfp" + calculate + ".svg");
                    await _userManager.UpdateAsync(user);
                }
            }
            return LocalRedirect("~/Identity/Account/Manage");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile newPfp)
        {
            if (newPfp!=null && newPfp.Length > 0)
            {
                string ext = Path.GetExtension(newPfp.FileName);
                if (" .jpg .png .svg .jpeg .gif".Contains(" " + ext))
                {
                    AppUser user = await _userManager.GetUserAsync(User);
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "savedPfp", user.Id + ext);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await newPfp.CopyToAsync(fileStream);
                    }
                    user.ProfileImg = Path.Combine("images", "savedPfp", user.Id + ext);
                    await _userManager.UpdateAsync(user);
                }
            }
            return LocalRedirect("~/Identity/Account/Manage");
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetAllRoles()
        {
            var selectList = new List<SelectListItem>();

            var roles = from role in db.Roles
                        select role;

            foreach (var role in roles)
            {
                selectList.Add(new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name.ToString()
                });
            }
            return selectList;
        }


    }
}
