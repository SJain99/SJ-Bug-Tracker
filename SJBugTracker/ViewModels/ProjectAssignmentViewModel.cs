using Microsoft.AspNet.Identity.EntityFramework;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJBugTracker.ViewModels
{
    public class ProjectAssignmentViewModel
    {
        public Project Project { get; set; }
        public List<ApplicationUser> Users { get; set; }
        [Display(Name = "Users")]
        public List<string> SelectedUsers { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public int ProjectId { get; set; }
        public List<string> Actions { get; set; }
        public string Action { get; set; }
    }
}