using Database.Enums;

namespace Web.ViewModels.Tickets
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public TicketStatus Status { get; set; }

    }
}
