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
            var actions = new List<string>
            {
                "Assign",
                "Remove"
            };

            var viewModel = new RoleManagementViewModel
            {
                User = _context.Users.Include(u => u.Roles).SingleOrDefault(u => u.Id == id),
                Roles = _context.Roles.ToList(),
                Actions = actions
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> ManageRole(RoleManagementViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            if (viewModel.Action == "Assign")
            {
                await AssignRole(viewModel);
                return RedirectToAction("Index");
            }
            else
            {
                await RemoveRole(viewModel);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task AssignRole(RoleManagementViewModel viewModel)
        {
            var isInRole = await UserManager.IsInRoleAsync(viewModel.UserId, viewModel.Role);
            var isUnassigned = await UserManager.IsInRoleAsync(viewModel.UserId, "Unassigned");

            if (isInRole)
                RedirectToAction("Index");

            if(isUnassigned)
            {
                await UserManager.AddToRoleAsync(viewModel.UserId, viewModel.Role);
                await UserManager.RemoveFromRoleAsync(viewModel.UserId, "Unassigned");
                RedirectToAction("Index");
            }

            await UserManager.AddToRoleAsync(viewModel.UserId, viewModel.Role);
        }

        [HttpPost]
        public async Task RemoveRole(RoleManagementViewModel viewModel)
        {
            var isInRole = await UserManager.IsInRoleAsync(viewModel.UserId, viewModel.Role);
            var userRoles = await UserManager.GetRolesAsync(viewModel.UserId);

            if (!isInRole || userRoles.Count <= 1)
            {
                RedirectToAction("Index");
            } else
            {
                await UserManager.RemoveFromRoleAsync(viewModel.UserId, viewModel.Role);
            }
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