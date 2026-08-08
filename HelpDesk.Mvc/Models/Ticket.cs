using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Mvc.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Priority { get; set; } // Low, Medium, High
        
        public string Status { get; set; } // Open, In Progress, Closed
        
        public string RaisedBy { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}
