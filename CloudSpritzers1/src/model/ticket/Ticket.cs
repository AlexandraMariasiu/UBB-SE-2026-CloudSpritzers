using CloudSpritzers1.src.model;
using System;

namespace CloudSpritzers1.src.model.ticket
{
    public class Ticket
    {
        public int TicketId { get; }
        public User TicketCreator { get; }
        public TicketUrgencyLevelEnum UrgencyLevel { get; private set; }
        public TicketStatusEnum TicketStatus { get; private set; }
        public TicketCategory Category { get; }
        public TicketSubcategory Subcategory { get; }
        public string TicketSubject { get; }
        public string TicketDescription { get; }
        public DateTime CreatedTimestamp { get; }
        public Ticket(int ticketId, User ticketCreator, TicketStatusEnum initialStatus, TicketCategory category, TicketSubcategory subcategory, string ticketSubject, string ticketDescription, DateTime creationTimestamp, TicketUrgencyLevelEnum? initialUrgencyLevel = null)
        { 
            TicketId = ticketId;
            TicketCreator = ticketCreator;
            UrgencyLevel = initialUrgencyLevel ?? category.CategoryUrgencyLevel;
            TicketStatus = initialStatus;
            Category = category;
            Subcategory = subcategory;
            TicketSubject = ticketSubject;
            TicketDescription = ticketDescription;
            CreatedTimestamp = creationTimestamp;
        }
        public void ChangeTicketStatus(TicketStatusEnum newStatus)
        {
            this.TicketStatus = newStatus;
        }

        public void ChangeUrgencyLevel(TicketUrgencyLevelEnum newUrgencyLevel)
        {
            this.UrgencyLevel = newUrgencyLevel;
        }
    }
 }

