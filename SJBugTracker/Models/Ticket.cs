namespace SJBugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Project Project { get; set; }
        public byte ProjectId { get; set; }
    }
}