using HelpDesk.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpDesk.Mvc.Services
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<int> CreateTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(int id, Ticket ticket);
        Task DeleteTicketAsync(int id);
        Task<List<Ticket>> GetTicketsByStatusAsync(string status);
    }
}
