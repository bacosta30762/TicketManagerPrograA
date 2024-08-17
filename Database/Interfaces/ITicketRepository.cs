using Database.Enums;
using Database.Models;

namespace Database.Interfaces
{
    public interface ITicketRepository
    {
        Task<int> AddAsync(Ticket ticket);
        Task DeleteAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket> GetByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetByUserIdAsync(string userId);
        Task UpdateAsync(Ticket ticket);
        Task UpdateStatusAsync(int id, TicketStatus status);
    }
}