using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Tickets
{
    public class CreateTicketViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
