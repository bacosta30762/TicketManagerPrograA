using Database.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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

        public async Task <IEnumerable<History>> ListAsync(int ticketid)
        {
            var TicketHistory = await _context.TicketsHistory.Where(h => h.TicketId == ticketid)
                .ToListAsync();

            return TicketHistory;
        }

    }
}
