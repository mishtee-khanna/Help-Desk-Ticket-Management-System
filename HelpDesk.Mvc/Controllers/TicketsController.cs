using HelpDesk.Mvc.Models;
using HelpDesk.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HelpDesk.Mvc.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index(string status)
        {
            var tickets = string.IsNullOrEmpty(status) ? 
                await _ticketService.GetAllTicketsAsync() : 
                await _ticketService.GetTicketsByStatusAsync(status);
            
            ViewBag.SelectedStatus = status;
            return View(tickets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Status = "Open"; // Hardcoded
                await _ticketService.CreateTicketAsync(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _ticketService.UpdateTicketAsync(id, ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
