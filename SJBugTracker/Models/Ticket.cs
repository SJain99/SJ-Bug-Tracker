using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SJBugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [Required]
        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
        [Required]
        public int TicketPriorityId { get; set; }
        public TicketPriority TicketPriority { get; set; }
        [Required]
        public int TicketStatusId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}