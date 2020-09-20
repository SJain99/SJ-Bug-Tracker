using AutoMapper;
using SJBugTracker.Dtos;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SJBugTracker.Controllers.Api
{
    public class ProjectsController : ApiController
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<ProjectDto> GetProjects()
        {
            var projects = (from p in _context.Projects
                            select new ProjectDto()
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Description = p.Description
                            }).ToList();

            foreach (ProjectDto projectDto in projects)
            {
                var tickets = from ticket in _context.Tickets
                              where ticket.ProjectId == projectDto.Id
                              select new TicketDto()
                              {
                                  Id = ticket.Id,
                                  Name = ticket.Name,
                                  Description = ticket.Description
                              };

                projectDto.TicketDtos = tickets.ToList();
            }

            return projects;
        }

        public ProjectDto GetProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var projectDto = Mapper.Map<ProjectDto>(project);

            var tickets = from ticket in _context.Tickets
                          where ticket.ProjectId == projectDto.Id
                          select new TicketDto()
                          {
                              Id = ticket.Id,
                              Name = ticket.Name,
                              Description = ticket.Description
                          };

            projectDto.TicketDtos = tickets.ToList();

            return projectDto;
        }

        [HttpPost]
        public IHttpActionResult CreateProject(ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var project = Mapper.Map<Project>(projectDto);

            _context.Projects.Add(project);
            _context.SaveChanges();

            projectDto.Id = project.Id;

            return Created(new Uri(Request.RequestUri + "/" + project.Id), projectDto);
        }

        [HttpPut]
        public void UpdateProject(int id, ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var projectInDB = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projectInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(projectDto, projectInDB);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteProject(int id)
        {
            var projectInDB = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projectInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Projects.Remove(projectInDB);
            _context.SaveChanges();
        }
    }
}
