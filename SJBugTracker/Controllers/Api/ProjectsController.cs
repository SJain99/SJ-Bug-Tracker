using AutoMapper;
using SJBugTracker.Dtos;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

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
            var ticketDtos = new List<KeyValuePair<int, TicketDto>>();
            List<ProjectDto> projectDtos = new List<ProjectDto>();

            foreach (var ticket in _context.Tickets)
                ticketDtos.Add(new KeyValuePair<int, TicketDto>(ticket.ProjectId, Mapper.Map<Ticket, TicketDto>(ticket)));
            

            foreach (var project in _context.Projects)
            {
                var matches = from val in ticketDtos where val.Key == project.Id select val.Value;
                var ticketDtosList = matches.ToList();
                var projectDto = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    TicketDtos = ticketDtosList
                };

                projectDtos.Add(projectDto);
            }

            return projectDtos;
        }

        public ProjectDto GetProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var ticketDtos = new List<KeyValuePair<int, TicketDto>>();

            foreach (var ticket in _context.Tickets)
                ticketDtos.Add(new KeyValuePair<int, TicketDto>(ticket.ProjectId, Mapper.Map<Ticket, TicketDto>(ticket)));

            var matches = from val in ticketDtos where val.Key == project.Id select val.Value;
            var ticketDtosList = matches.ToList();

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                TicketDtos = ticketDtosList
            };

            return projectDto;
        }

        [HttpPost]
        public IHttpActionResult CreateProject(ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tickets = new List<KeyValuePair<int, Ticket>>();

            foreach (var ticketDto in projectDto.TicketDtos)
            {
                tickets.Add(new KeyValuePair<int, Ticket>(ticketDto.ProjectDto.Id, Mapper.Map<TicketDto, Ticket>(ticketDto)));
            }

            var matches = from val in tickets where val.Key == projectDto.Id select val.Value;
            var ticketsList = matches.ToList();

            var project = new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Description = projectDto.Description,
                Tickets = ticketsList
            };

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

            var tickets = new List<KeyValuePair<int, Ticket>>();

            foreach (var ticketDto in projectDto.TicketDtos)
            {
                tickets.Add(new KeyValuePair<int, Ticket>(ticketDto.ProjectDto.Id, Mapper.Map<TicketDto, Ticket>(ticketDto)));
            }

            var matches = from val in tickets where val.Key == projectDto.Id select val.Value;
            var ticketsList = matches.ToList();

            projectInDB.Id = projectDto.Id;
            projectInDB.Name = projectDto.Name;
            projectInDB.Description = projectDto.Description;
            projectInDB.Tickets = ticketsList;

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
