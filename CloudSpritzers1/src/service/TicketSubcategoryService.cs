using CloudSpritzers1.src.model.ticket;
using CloudSpritzers1.src.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TicketSubcategoryService
{
    private readonly TicketSubcategoryRepository _subcategoryRepository;

    public TicketSubcategoryService(TicketSubcategoryRepository subcategoryRepository)
    {
        _subcategoryRepository = subcategoryRepository;
    }


    public IEnumerable<TicketSubcategory> GetSubcategoriesByCategoryId(int categoryId)
    {
        return _subcategoryRepository.GetByCategoryId(categoryId);
    }
    public TicketSubcategory GetSubcategoryById(int subcategoryId)
    {
        return _subcategoryRepository.GetById(subcategoryId);
    }
    //public IEnumerable<TicketCategory> GetAllSubcategories()
    //{
    //    return _subcategoryRepository.GetAll();
    //}
}