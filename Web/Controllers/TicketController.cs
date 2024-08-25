using Business.Interfaces;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.ViewModels.Tickets;

namespace Web.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<User> _userManager;

        public TicketController(ITicketService ticketService, UserManager<User> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            IEnumerable<Ticket> tickets;

            if (isAdmin)
            {
                tickets = await _ticketService.GetAllAsync();
            }
            else
            {
                tickets = await _ticketService.GetByUserIdAsync(user.Id);
            }

            var ticketViewModels = tickets.Select(ticket => new TicketViewModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                CreatedBy = ticket.CreatedBy.Name,
                CreatedDate = ticket.CreatedAt,
                Status = ticket.Status,
            });

            ViewBag.IsAdmin = isAdmin;

            return View(ticketViewModels);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ticketId = await _ticketService.AddAsync(model.Title, model.Description, userId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _ticketService.ApproveAsync(userId, ticketId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _ticketService.RejectAsync(userId, ticketId);
            return RedirectToAction("Index");
        }

        /*[HttpGet]
        public async Task<IActionResult> History(int ticketId)
        {
            var history = await _ticketService.ListHistoryAsync(ticketId);
            return View("History", history);
        }*/

        [HttpGet]
        public async Task<IActionResult> History(int ticketId)
        {
            var history = await _ticketService.ListHistoryAsync(ticketId);

            var historyViewModels = history.Select(h => new HistoryViewModel
            {
                Id = h.Id,
                TicketId = h.TicketId,
                ChangeDescription = h.ChangeDescription,
                CreatedAt = h.CreatedAt,
                ChangedBy = h.ChangedBy?.UserName
            });

            return View(historyViewModels);
        }

    }
    
}
