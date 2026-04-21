using System.Collections.Generic;
using CloudSpritzers1.Src.Model.Ticket;

namespace CloudSpritzers1.Src.Service.interfaces
{
    public interface ITicketSubcategoryService
    {
        TicketSubcategory GetSubcategoryById(int subcategoryId);

        IEnumerable<TicketSubcategory> GetSubcategoriesByCategoryId(int categoryId);
    }
}