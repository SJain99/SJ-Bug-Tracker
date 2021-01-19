using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SJBugTracker.Models;
using SJBugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SJBugTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            var users = _context.Users.Include(u => u.Roles).ToList();
            var roles = _context.Roles.ToList();

            var viewModel = new AdminIndexViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(viewModel);
        }

        public ActionResult RoleManagement(string id)
        {

            var viewModel = new RoleManagementViewModel
            {
                User = _context.Users.Include(u => u.Roles).SingleOrDefault(u => u.Id == id),
                Roles = _context.Roles.ToList()
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> UpdateRole(RoleManagementViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index");

            var isInRole = await UserManager.IsInRoleAsync(viewModel.UserId, viewModel.Role);
            var userRoles = await UserManager.GetRolesAsync(viewModel.UserId);

            if (isInRole || userRoles.Contains("Admin"))
                return RedirectToAction("Index");

            foreach (var role in userRoles)
            {
                await UserManager.RemoveFromRoleAsync(viewModel.UserId, role);
            }

            await UserManager.AddToRoleAsync(viewModel.UserId, viewModel.Role);
            return RedirectToAction("Index");
        }

        public async Task DeleteUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var isAdmin = await UserManager.IsInRoleAsync(id, "Admin");

            if (user != null && !isAdmin)
                await UserManager.DeleteAsync(user);
            throw new Exception();
        }
    }
}