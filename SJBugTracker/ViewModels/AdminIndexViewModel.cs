using Microsoft.AspNet.Identity.EntityFramework;
using SJBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJBugTracker.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}