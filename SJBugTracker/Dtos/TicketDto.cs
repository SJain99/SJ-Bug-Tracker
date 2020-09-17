using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ProjectDto ProjectDto { get; set; }
    }
}