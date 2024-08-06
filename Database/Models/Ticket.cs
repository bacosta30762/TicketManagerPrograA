using Database.Enums;

namespace Database.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string CreatedById { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TicketStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User CreatedBy { get; set; }
    }
}
