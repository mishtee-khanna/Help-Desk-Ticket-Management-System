using HelpDesk.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelpDesk.Api.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<int> CreateTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int id);
        Task<List<Ticket>> GetTicketsByStatusAsync(string status);
    }
}
