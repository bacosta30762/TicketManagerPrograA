namespace Web.ViewModels.Tickets
{
    public class HistoryViewModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string ChangeDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ChangedBy { get; set; }
    }
}
