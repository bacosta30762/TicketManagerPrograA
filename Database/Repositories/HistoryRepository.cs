using Database.Interfaces;
using Database.Models;

namespace Database.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public HistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(History history)
        {
            _context.TicketsHistory.Add(history);
            await _context.SaveChangesAsync();
        }

    }
}
