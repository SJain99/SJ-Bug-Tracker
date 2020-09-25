namespace SJBugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
        public int TicketPriorityId { get; set; }
        public TicketPriority TicketPriority { get; set; }
    }
}