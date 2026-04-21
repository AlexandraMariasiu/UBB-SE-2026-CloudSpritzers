using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.Src.Model.Ticket;
using CloudSpritzers1.Src.Repository;

namespace CloudSpritzers1.Src.Dto
{
    public record TicketDTO(
        int TicketId,
        int CreatorAccountId,
        string CreatorEmailAddress,
        TicketUrgencyLevelEnum UrgencyLevel,
        TicketStatusEnum CurrentStatus,
        int CategoryId,
        string CategoryName,
        int SubcategoryId,
        string SubcategoryName,
        string Subject,
        string Description,
        DateTime CreationTimestamp)
    { }
}
