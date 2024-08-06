using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Interfaces;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                Title = ticket.Title,
                Description = ticket.Description,
                CreatedBy = ticket.CreatedBy.Name,
                CreatedDate = ticket.CreatedAt,
                Status = ticket.Status, 
            });

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
    }
}
