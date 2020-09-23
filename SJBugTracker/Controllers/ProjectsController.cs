using SJBugTracker.Models;
using SJBugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}