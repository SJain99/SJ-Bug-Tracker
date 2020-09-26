using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.ViewModels
{
    public class NewTicketViewModel
    {
        public List<Project> Projects { get; set; }
        public Ticket Ticket { get; set; }
        public List<TicketType> TicketTypes { get; set; }
        public List<TicketPriority> TicketPriorities { get; set; }
    }
}