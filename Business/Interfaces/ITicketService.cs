using Database.Models;

namespace Business.Interfaces
{
    public interface ITicketService
    {
        Task<int> AddAsync(string title, string description, string createdById);
        Task<int> ApproveAsync(string changedById, int ticketId);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<IEnumerable<Ticket>> GetByUserIdAsync(string userId);
        Task<int> RejectAsync(string changedById, int ticketId);
    }
}