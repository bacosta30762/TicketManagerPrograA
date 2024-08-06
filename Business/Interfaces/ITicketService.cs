using Database.Models;

namespace Business.Interfaces
{
    public interface ITicketService
    {
        Task<int> AddAsync(string title, string description, string createdById);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<IEnumerable<Ticket>> GetByUserIdAsync(string userId);
    }
}