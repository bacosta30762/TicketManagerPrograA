namespace Database.Models
{
    public class History
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string ChangeDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ChangedById { get; set; }
        public User ChangedBy { get; set; }

    }
}
