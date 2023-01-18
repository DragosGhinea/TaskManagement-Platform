using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProiectTaskManagement.Data;
using ProiectTaskManagement.Models;
using ProiectTaskManagement.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration.GetValue<string>("GoogleClientID");
    googleOptions.ClientSecret = builder.Configuration.GetValue<string>("GoogleClientSecret");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


/// TeamMembers Controller

app.MapControllerRoute(
    name: "TeamMembersShow",
    pattern: "/projects/{projectId}/team",
    defaults: new { controller = "TeamMembers", action = "Index" });

app.MapControllerRoute(
    name: "TeamMembersCreate",
    pattern: "/projects/{projectId}/team/add",
    defaults: new { controller = "TeamMembers", action = "Create" });

app.MapControllerRoute(
    name: "TeamMembersDelete",
    pattern: "/projects/{projectId}/team/remove/{memberId}",
    defaults: new { controller = "TeamMembers", action = "Delete" });


app.MapControllerRoute(
    name: "TeamMembersLeave",
    pattern: "/projects/{projectId}/team/leave",
    defaults: new { controller = "TeamMembers", action = "Leave" });


app.MapControllerRoute(
    name: "TeamMembersInviteLeader",
    pattern: "/projects/{projectId}/team/inviteLeader",
    defaults: new { controller = "TeamMembers", action = "InviteLeader" });


app.MapControllerRoute(
    name: "TeamMembersDropLeadership",
    pattern: "/projects/{projectId}/team/dropLeadership",
    defaults: new { controller = "TeamMembers", action = "DropLeadership" });

/// Statuses routes

app.MapControllerRoute(
    name: "StatusesShow",
    pattern: "/projects/{projectId}/statuses/",
    defaults: new { controller = "Statuses", action = "Index" });

app.MapControllerRoute(
    name: "StatusesCreate",
    pattern: "/projects/{projectId}/statuses/create",
    defaults: new { controller = "Statuses", action = "Create" });

app.MapControllerRoute(
    name: "StatusesDelete",
    pattern: "/projects/{projectId}/statuses/remove",
    defaults: new { controller = "Statuses", action = "Delete" });

app.MapControllerRoute(
    name: "StatusesEdit",
    pattern: "/projects/{projectId}/statuses/edit",
    defaults: new { controller = "Statuses", action = "Edit" });



/// Tasks routes
app.MapControllerRoute(
    name: "TaskUpdateStatus",
    pattern: "/projects/{projectId}/tasks/updateStatus/{taskId}/{newStatusId}",
    defaults: new { controller = "Tasks", action = "UpdateStatus" });

app.MapControllerRoute(
    name: "TaskDelete",
    pattern: "/projects/{projectId}/tasks/delete/{taskId}",
    defaults: new { controller = "Tasks", action = "Delete" });

app.MapControllerRoute(
    name: "TaskEdit",
    pattern: "/projects/{projectId}/tasks/edit/{taskId}",
    defaults: new { controller = "Tasks", action = "Edit" });

app.MapControllerRoute(
    name: "TaskCreate",
    pattern: "/projects/{projectId}/tasks/create",
    defaults: new { controller = "Tasks", action = "Create" });

app.MapControllerRoute(
    name: "TaskAssign",
    pattern: "/projects/{projectId}/tasks/assign/{taskId}",
    defaults: new { controller = "Tasks", action = "Assign" });

app.MapControllerRoute(
    name: "TaskShow",
    pattern: "/projects/{projectId}/tasks/{taskId?}",
    defaults: new { controller = "Tasks", action = "Show" });




/// Comments Routes

app.MapControllerRoute(
    name: "CommentsShow",
    pattern: "/projects/{projectId}/task/{taskId}/comments/{comment_id?}",
    defaults: new { controller = "Comments", action = "Show" });

app.MapControllerRoute(
    name: "CommentsAdd",
    pattern: "/projects/{projectId}/task/{taskId}/comments/add",
    defaults: new { controller = "Comments", action = "Add" });

app.MapControllerRoute(
    name: "CommentsDelete",
    pattern: "/projects/{projectId}/task/{taskId}/comments/delete/{comment_id?}",
    defaults: new { controller = "Comments", action = "Delete" });

app.MapControllerRoute(
    name: "CommentsEdit",
    pattern: "/projects/{projectId}/task/{taskId}/comments/edit/{comment_id?}",
    defaults: new { controller = "Comments", action = "Edit" });

/// Projects routes


app.MapControllerRoute(
    name: "ProjectsCreate",
    pattern: "/projects/create",
    defaults: new { controller = "Projects", action = "Create" });

app.MapControllerRoute(
    name: "ProjectsDelete",
    pattern: "/projects/delete/{projectId?}",
    defaults: new { controller = "Projects", action = "Delete" });

app.MapControllerRoute(
    name: "ProjectsEdit",
    pattern: "/projects/edit/{projectId?}",
    defaults: new { controller = "Projects", action = "Edit" });


app.MapControllerRoute(
    name: "ProjectsShow",
    pattern: "/projects/{projectId}",
    defaults: new { controller = "Projects", action = "Show" });

app.MapControllerRoute(
    name: "ProjectsShow",
    pattern: "/projects/",
    defaults: new { controller = "Projects", action = "Index" });


/// Panel routes

app.MapControllerRoute(
    name: "PanelProjects",
    pattern: "/panel/projects",
    defaults: new { controller = "Panel", action = "Projects" });

app.MapControllerRoute(
    name: "PanelUsers",
    pattern: "/panel/users",
    defaults: new { controller = "Panel", action = "AppUsers" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");


app.MapRazorPages();

app.Run();
