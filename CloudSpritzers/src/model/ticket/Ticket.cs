 namespace CloudSpritzers.src.model.ticket
 {
    public class Ticket
    {
        public int TicketId { get; }
        public User User { get; }
        public UrgencyLevelEnum UrgencyLevel { get; }
        public StatusEnum Status { get; private set; }
        public Category Category { get; }
        public Subcategory Subcategory { get; }
        public string Subject { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }
        public Ticket(int ticketId, User user, UrgencyLevelEnum urgencyLevel, StatusEnum status, Category category, Subcategory subcategory, string subject, string description, DateTime createdAt)
        {
            TicketId = ticketId;
            User = user;
            UrgencyLevel = urgencyLevel;
            Status = status;
            Category = category;
            Subcategory = subcategory;
            Subject = subject;
            Description = description;
            CreatedAt = createdAt;
            }
        public void UpdateStatus(StatusEnum newStatus)
        {
            this.Status = newStatus;
        }
    }
 }

