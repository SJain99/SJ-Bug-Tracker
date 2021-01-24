using SJBugTracker.Models;
using SJBugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SJBugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext _context;

        public TicketsController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var projects = _context.Projects.ToList();

            var viewModel = new NewTicketViewModel
            {
                Projects = projects,
                TicketTypes = _context.TicketTypes.ToList(),
                TicketPriorities = _context.TicketPriorities.ToList()
            };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var ticket = _context.Tickets.SingleOrDefault(t => t.Id == id);
            var projectId = ticket.ProjectId;
            var project = _context.Projects.SingleOrDefault(p => p.Id == projectId);

            var viewModel = new EditTicketViewModel
            {
                Project = project,
                Ticket = ticket,
                TicketTypes = _context.TicketTypes.ToList(),
                TicketPriorities = _context.TicketPriorities.ToList()
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

            var ticket = _context.Tickets.SingleOrDefault(t => t.Id == id);
            var users = _context.Users.Include(u => u.Roles).Include(u => u.Projects).ToList();

            foreach (var user in users.ToList())
            {
                if (user.Roles.FirstOrDefault().RoleId != "dc581abb-7c4a-4800-8e50-7b65d0e764aa")
                    users.Remove(user);

                if (user.Projects != null)
                {
                    var projectCount = user.Projects.Count();
                    var projectIndex = 0;

                    foreach (var project in user.Projects)
                    {
                        if (project.Id != ticket.ProjectId)
                            projectIndex++;
                    }

                    if (projectIndex == projectCount)
                        users.Remove(user);
                }
                else
                    users.Remove(user);
            }

            var viewModel = new TicketAssignmentViewModel
            {
                Ticket = ticket,
                Users = users,
                Actions = actions
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public RedirectToRouteResult ManageTicket(TicketAssignmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            if (viewModel.Action == "Assign")
            {
                AssignTicketToUsers(viewModel);
                return RedirectToAction("Index");
            }
            else
            {
                RemoveTicketFromUsers(viewModel);
                return RedirectToAction("Index");
            }
        }

        public void AssignTicketToUsers(TicketAssignmentViewModel viewModel)
        {
            var ticket = _context.Tickets.SingleOrDefault(t => t.Id == viewModel.TicketId);
            foreach (var userID in viewModel.SelectedUsers)
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == userID);
                if (!user.Tickets.Contains(ticket))
                    user.Tickets.Add(ticket);
                _context.SaveChanges();
            }
        }

        public void RemoveTicketFromUsers(TicketAssignmentViewModel viewModel)
        {
            var ticket = _context.Tickets.SingleOrDefault(t => t.Id == viewModel.TicketId);
            foreach (var userID in viewModel.SelectedUsers)
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == userID);
                if (user.Tickets.Contains(ticket))
                    user.Tickets.Remove(ticket);
                _context.SaveChanges();
            }
        }
    }
}