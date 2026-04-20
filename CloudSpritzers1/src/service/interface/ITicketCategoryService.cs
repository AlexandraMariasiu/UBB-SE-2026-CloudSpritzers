using System.Collections.Generic;
using CloudSpritzers1.src.model.ticket;

namespace CloudSpritzers1.src.service.interfaces
{
    public interface ITicketCategoryService
    {
        TicketCategory GetCategoryById(int categoryId);

        IEnumerable<TicketCategory> GetAllCategories();
    }
}