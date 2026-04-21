using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.Src.Model.Ticket;
using CloudSpritzers1.Src.Repository;
using CloudSpritzers1.Src.Repository.Interfaces;
using CloudSpritzers1.Src.Service.Interfaces;

public class TicketSubcategoryService : ITicketSubcategoryService
{
    private readonly ITicketSubcategoryRepository _subcategoryRepository;

    public TicketSubcategoryService(ITicketSubcategoryRepository subcategoryRepository)
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
    // public IEnumerable<TicketCategory> GetAllSubcategories()
    // {
    //    return _subcategoryRepository.GetAll();
    // }
}