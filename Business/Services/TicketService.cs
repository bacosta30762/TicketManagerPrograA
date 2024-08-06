using Business.Interfaces;
using Database.Enums;
using Database.Interfaces;
using Database.Models;

namespace Business.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<int> AddAsync(string title, string description, string createdById)
        {
            var ticket = new Ticket
            {
                Title = title,
                Description = description,
                CreatedById = createdById,
                CreatedAt = DateTime.Now,
                Status = TicketStatus.Pending,
            };

            var ticketId = await _ticketRepository.AddAsync(ticket);

            return ticketId;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(string userId)
        {
            return await _ticketRepository.GetByUserIdAsync(userId);
        }
    }
}
