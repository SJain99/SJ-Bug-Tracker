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
    public class ProjectsController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
                return HttpNotFound();

            var tickets = _context.Tickets.Where(t => t.ProjectId == id).ToList();

            var viewModel = new ProjectDetailsViewModel()
            {
                Project = project,
                Tickets = tickets
            };

            return View(viewModel);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
                return HttpNotFound();

            var tickets = _context.Tickets.Where(t => t.ProjectId == id).ToList();

            var viewModel = new ProjectDetailsViewModel()
            {
                Project = project,
                Tickets = tickets
            };

            return View(viewModel);
        }

        public ActionResult UserManagement(int id)
        {
            var actions = new List<string>
            {
                "Assign",
                "Remove"
            };

            var users = _context.Users.Include(u => u.Roles).Include(u => u.Projects).ToList();

            foreach(var user in users.ToList())
            {
                foreach(var role in user.Roles)
                {
                    if(role.RoleId == "759a9587-8b75-4209-a5eb-3f4e57e3a3fc" || role.RoleId == "6b8ce7f4-726d-4fc7-8fd9-2e4f09b9e066")
                    {
                        users.Remove(user);
                    }
                }
            }

            var viewModel = new ProjectAssignmentViewModel
            {
                Project = _context.Projects.SingleOrDefault(p => p.Id == id),
                Users = users,
                Roles = _context.Roles.ToList(),
                Actions = actions
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public RedirectToRouteResult ManageProject(ProjectAssignmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            if(viewModel.Action == "Assign")
            {
                AssignProjectToUsers(viewModel);
                return RedirectToAction("Index");
            } else
            {
                RemoveProjectFromUsers(viewModel);
                return RedirectToAction("Index");
            }
        }

        public void AssignProjectToUsers(ProjectAssignmentViewModel viewModel)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == viewModel.ProjectId);
            foreach(var userID in viewModel.SelectedUsers)
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == userID);
                if(!user.Projects.Contains(project))
                    user.Projects.Add(project);
                _context.SaveChanges();
            }
        }

        public void RemoveProjectFromUsers(ProjectAssignmentViewModel viewModel)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == viewModel.ProjectId);
            foreach (var userID in viewModel.SelectedUsers)
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == userID);
                if (user.Projects.Contains(project))
                    user.Projects.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}