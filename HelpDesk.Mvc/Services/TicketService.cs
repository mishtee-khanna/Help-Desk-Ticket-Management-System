using HelpDesk.Mvc.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HelpDesk.Mvc.Services
{
    public class TicketService : ITicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CreateTicketAsync(Ticket ticket)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ticket", ticket);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Ticket/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>("api/Ticket/All");
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ticket>($"api/Ticket/{id}");
        }

        public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>($"api/Ticket/Status/{status}");
        }

        public async Task UpdateTicketAsync(int id, Ticket ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Ticket/{id}", ticket);
            response.EnsureSuccessStatusCode();
        }
    }
}
