using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}