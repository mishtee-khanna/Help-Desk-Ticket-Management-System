using System;

namespace HelpDesk.Api.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // Low, Medium, High
        public string Status { get; set; } // Open, In Progress, Closed
        public string RaisedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
