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
    [Authorize(Roles = "Admin, Project Manager")]
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

        [Authorize(Roles = "Admin")]
        public ActionResult RoleAssignment(string id)
        {
            var viewModel = new RoleAssignmentViewModel
            {
                User = _context.Users.Include(u => u.Roles).SingleOrDefault(u => u.Id == id),
                Roles = _context.Roles.ToList()
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AssignRole(IdentityUserRole userRole)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var role = _context.Roles.SingleOrDefault(r => r.Id == userRole.RoleId).Name;

            var isInRole = await UserManager.IsInRoleAsync(userRole.UserId, role);

            if(isInRole)
                return RedirectToAction("Index");

            await UserManager.AddToRoleAsync(userRole.UserId, role);

            return RedirectToAction("Index");
        }
    }
}