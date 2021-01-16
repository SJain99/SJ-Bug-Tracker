using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJBugTracker.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public ProjectDto Project { get; set; }
        [Required]
        public int TicketTypeId { get; set; }
        public TicketTypeDto TicketType { get; set; }
        [Required]
        public int TicketPriorityId { get; set; }
        public TicketPriorityDto TicketPriority { get; set; }
        [Required]
        public int TicketStatusId { get; set; }
        public TicketStatusDto TicketStatus { get; set; }
    }
}