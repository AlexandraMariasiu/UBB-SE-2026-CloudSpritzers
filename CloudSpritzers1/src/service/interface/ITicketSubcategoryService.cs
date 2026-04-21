using System.Collections.Generic;
using CloudSpritzers1.src.model.ticket;

namespace CloudSpritzers1.src.service.interfaces
{
    public interface ITicketSubcategoryService
    {
        TicketSubcategory GetSubcategoryById(int subcategoryId);

        IEnumerable<TicketSubcategory> GetSubcategoriesByCategoryId(int categoryId);
    }
}