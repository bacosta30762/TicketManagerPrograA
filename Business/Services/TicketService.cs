using Business.Interfaces;
using Database.Enums;
using Database.Interfaces;
using Database.Models;

namespace Business.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IHistoryRepository _ticketHistory;

        public TicketService(ITicketRepository ticketRepository, IHistoryRepository ticketHistory)
        {
            _ticketRepository = ticketRepository;
            _ticketHistory = ticketHistory;
        }

        public async Task<int> AddAsync(string title, string description, string createdById)
        {
            var ticket = new Ticket
            {
                Title = title,
                Description = description,
                CreatedById = createdById,
                CreatedAt = DateTime.Now,
                Status = TicketStatus.InProgress,
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

        public async Task<int> ApproveAsync(string changedById, int ticketId)
        {
            await _ticketRepository.UpdateStatusAsync(ticketId, TicketStatus.Approved);


            var history = new History
            {
                ChangedById = changedById,
                ChangeDescription = "Ticket Aprobado",
                TicketId = ticketId,
                CreatedAt = DateTime.Now
            };

            await _ticketHistory.AddAsync(history);

            return ticketId;

        }

        public async Task<int> RejectAsync(string changedById, int ticketId)
        {
            await _ticketRepository.UpdateStatusAsync(ticketId, TicketStatus.Refused);


            var history = new History
            {
                ChangedById = changedById,
                ChangeDescription = "Ticket Rechazado",
                TicketId = ticketId,
                CreatedAt = DateTime.Now
            };

            await _ticketHistory.AddAsync(history);

            return ticketId;

        }

        public async Task<IEnumerable<History>> ListHistoryAsync(int ticketId)
        {
            return await _ticketHistory.ListAsync(ticketId);

        }


    }
}
