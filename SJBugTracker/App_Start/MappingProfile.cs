using AutoMapper;
using SJBugTracker.Dtos;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Project, ProjectDto>();
            Mapper.CreateMap<ProjectDto, Project>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Ticket, TicketDto>();
            Mapper.CreateMap<TicketDto, Ticket>().ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}