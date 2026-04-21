using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.Src.Model.Ticket;
using CloudSpritzers1.Src.Repository;
using CloudSpritzers1.Src.Repository.Interfaces;
using CloudSpritzers1.Src.Service.Interfaces;

public class TicketCategoryService : ITicketCategoryService
{
    private readonly ITicketCategoryRepository _categoryRepository;

    public TicketCategoryService(ITicketCategoryRepository categoryRepository)
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