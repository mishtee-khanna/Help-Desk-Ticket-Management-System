using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _repository;

        public TicketController(ITicketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _repository.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _repository.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            ticket.CreatedDate = System.DateTime.Now;
            var id = await _repository.CreateTicketAsync(ticket);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket ticket)
        {
            if (ticket == null || id != ticket.Id)
            {
                return BadRequest();
            }

            var existingTicket = await _repository.GetTicketByIdAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            // Keep the original created date
            ticket.CreatedDate = existingTicket.CreatedDate;
            
            await _repository.UpdateTicketAsync(ticket);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var existingTicket = await _repository.GetTicketByIdAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            await _repository.DeleteTicketAsync(id);
            return Ok();
        }

        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetTicketsByStatus(string status)
        {
            var tickets = await _repository.GetTicketsByStatusAsync(status);
            return Ok(tickets);
        }
    }
}
