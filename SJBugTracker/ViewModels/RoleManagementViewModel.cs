using Microsoft.AspNet.Identity.EntityFramework;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.ViewModels
{
    public class RoleManagementViewModel
    {
        public ApplicationUser User { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}