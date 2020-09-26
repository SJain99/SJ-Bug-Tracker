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
    }
}