﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TicketDto> TicketDtos { get; set; }
    }
}