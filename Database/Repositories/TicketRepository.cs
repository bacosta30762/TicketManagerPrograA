using Database.Enums;
using Database.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetByIdAsync(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);

            if (ticket != null)
            {
                return ticket;
            }

            throw new KeyNotFoundException($"No se encontró el Ticket ID:{ticketId}.");
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            var tickets = await _context.Tickets.Include(t => t.CreatedBy).ToListAsync();

            return tickets;
        }

        public async Task<int> AddAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket.Id;
        }

        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(string userId)
        {
            return await _context.Tickets.Where(t => t.CreatedById == userId).ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            var ticketInDb = await _context.Tickets.FindAsync(ticket.Id);

            if (ticketInDb != null)
            {
                ticketInDb.Title = ticket.Title;
                ticketInDb.Description = ticket.Description;
                ticketInDb.Status = ticket.Status;
                ticketInDb.UpdatedAt = DateTime.Now;

                _context.Tickets.Update(ticketInDb);
                await _context.SaveChangesAsync();
            }

            throw new KeyNotFoundException($"No se encontró el Ticket ID:{ticket.Id}.");
        }

        public async Task UpdateStatusAsync(int id, TicketStatus status)
        {
            var ticketInDb = await _context.Tickets.FindAsync(id);

            if (ticketInDb != null)
            {
                ticketInDb.Status = status;
                ticketInDb.UpdatedAt = DateTime.Now;

                _context.Tickets.Update(ticketInDb);
                await _context.SaveChangesAsync();
            }

            throw new KeyNotFoundException($"No se encontró el Ticket ID:{id}.");
        }

        public async Task DeleteAsync(int ticketId)
        {
            var ticketInDb = await _context.Tickets.FindAsync(ticketId);

            if (ticketInDb != null)
            {
                _context.Tickets.Remove(ticketInDb);
                await _context.SaveChangesAsync();
            }

            throw new KeyNotFoundException($"No se encontró el Ticket ID:{ticketId}.");
        }
    }

}
