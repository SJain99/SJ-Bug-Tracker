﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectDtoId { get; set; }
    }
}