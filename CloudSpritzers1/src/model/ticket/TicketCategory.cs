using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.ticket
{
    public class TicketCategory
    {
        public int CategoryId { get; }
        public string Name { get; }

        public UrgencyLevelEnum UrgencyLevel { get; }

        public TicketCategory(int categoryId, string name, UrgencyLevelEnum urgencyLevel)
        {
            CategoryId = categoryId;
            Name = name;
            UrgencyLevel = urgencyLevel;
        }
    }
}
