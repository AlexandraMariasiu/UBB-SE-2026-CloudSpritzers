using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.ticket
{
    public class TicketSubcategory
    {
        public int SubcategoryId { get; }
        public string SubcategoryName { get; }
        public int ExternalId { get; }
        public TicketCategory Category { get; }

        public TicketSubcategory(int subcategoryId, string subcategoryName, int externalId, TicketCategory category)
        {
            SubcategoryId = subcategoryId;
            SubcategoryName = subcategoryName;
            ExternalId = externalId;
            Category = category;
        }
    }
}