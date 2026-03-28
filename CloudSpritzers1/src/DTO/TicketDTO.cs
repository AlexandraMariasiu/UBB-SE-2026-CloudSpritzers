using CloudSpritzers1.src.model.ticket;
using CloudSpritzers1.src.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.DTO
{
    public class TicketDTO
    {

        public int TicketId { get; set; }
        public int UserId { get; set; }      
        public UrgencyLevelEnum UrgencyLevel { get; set; }
        public StatusEnum Status { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public TicketDTO() { }
        public TicketDTO(int ticketId, int userId, StatusEnum status, UrgencyLevelEnum urgencyLevel, int categoryId, int subcategoryId, string subject, string description, DateTime createdAt)
        {
            TicketId = ticketId;
            UserId = userId;
            UrgencyLevel = urgencyLevel;
            Status = status;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            Subject = subject;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
