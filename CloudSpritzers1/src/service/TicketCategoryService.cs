using CloudSpritzers1.src.model.ticket;
using CloudSpritzers1.src.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TicketCategoryService
{
    private readonly TicketCategoryRepository _categoryRepository;

    public TicketCategoryService(TicketCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public TicketCategory GetCategoryById(int categoryId)
    {
        return _categoryRepository.GetById(categoryId); 
    }
    public IEnumerable<TicketCategory> GetAllCategories()
    {
        return _categoryRepository.GetAll();
    }
}