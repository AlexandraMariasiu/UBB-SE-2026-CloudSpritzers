using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers.src.model.ticket
{
    public class TicketSubcategory
    {
        public int SubcategoryId { get; }
        public string Name { get; }
        public int ExternalId { get; }
        public int CategoryId { get; } // should we use it as id like in the uml or change it to an object?

        public TicketSubcategory(int subcategoryId, string name, int externalId, int categoryId)
        {
            SubcategoryId = subcategoryId;
            Name = name;
            ExternalId = externalId;
            CategoryId = categoryId;
        }
    }
}
