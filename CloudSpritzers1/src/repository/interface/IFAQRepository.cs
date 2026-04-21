using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.Src.Model.Faq;

<<<<<<< HEAD
namespace CloudSpritzers1.Src.Repository.Interfaces
=======
namespace CloudSpritzers1.src.repository.interfaces;

public interface IFAQRepository : IRepository<int, FAQEntry>
>>>>>>> f13abe4dcf77fd075160dbe0705442508e85ce1c
{
    List<FAQEntry> GetByCategory(FAQCategoryEnum category);
    void IncrementViewCount(int id);
    void IncrementWasHelpfulVotes(int id);
    void IncrementWasNotHelpfulVotes(int id);
}
