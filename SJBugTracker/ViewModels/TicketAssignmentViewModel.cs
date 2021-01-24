using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJBugTracker.ViewModels
{
    public class TicketAssignmentViewModel
    {
        public Ticket Ticket { get; set; }
        public List<ApplicationUser> Users { get; set; }
        [Display(Name = "Users")]
        public List<string> SelectedUsers { get; set; }
        public int TicketId { get; set; }
        public List<string> Actions { get; set; }
        public string Action { get; set; }
    }
}