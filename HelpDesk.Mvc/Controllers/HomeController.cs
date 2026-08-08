using HelpDesk.Mvc.Models;
using HelpDesk.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITicketService _ticketService;

        public HomeController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            ViewBag.TotalTickets = tickets.Count;
            ViewBag.OpenTickets = tickets.Count(t => t.Status == "Open");
            ViewBag.ClosedTickets = tickets.Count(t => t.Status == "Closed");
            return View();
        }
    }
}
