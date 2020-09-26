using SJBugTracker.Models;
using SJBugTracker.ViewModels;
using System;
using System.Collections.Generic;
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
    }
}