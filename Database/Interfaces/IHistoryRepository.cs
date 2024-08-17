using Database.Models;

namespace Database.Interfaces
{
    public interface IHistoryRepository
    {
        Task AddAsync(History history);
    }
}