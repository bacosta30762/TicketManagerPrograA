using Database.Models;

namespace Database.Interfaces
{
    public interface IHistoryRepository
    {
        Task AddAsync(History history);
        Task<IEnumerable<History>> ListAsync(int ticketid);
    }
}