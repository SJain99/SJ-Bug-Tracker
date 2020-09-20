using AutoMapper;
using SJBugTracker.Dtos;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var projects = _context.Projects.ToList();
            return Mapper.Map<List<ProjectDto>>(projects);
        }

        public ProjectDto GetProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<ProjectDto>(project);
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
