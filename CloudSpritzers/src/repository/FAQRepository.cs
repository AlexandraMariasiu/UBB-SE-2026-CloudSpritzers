using System.Collections.Generic;
using System.Linq;
using CloudSpritzers.src.model.faq;

namespace CloudSpritzers.src.repository
{
    public class FAQRepository: IRepository<int, FAQEntry>
    {
        private Dictionary<int, FAQEntry> faqs = new Dictionary<int, FAQEntry>();

        public FAQEntry GetById(int id)
        {
            return faqs.ContainsKey(id) ? faqs[id] : null;
        }

        public int Add(FAQEntry elem)
        {
            if (faqs.ContainsKey(elem.Id))
                return -1; 

            faqs[elem.Id] = elem;
            return elem.Id;
        }

        public void UpdateById(int id, FAQEntry elem)
        {
            if (faqs.ContainsKey(id))
            {
                faqs[id] = elem;
            }
        }

        public void DeleteById(int id)
        {
            if (faqs.ContainsKey(id))
            {
                faqs.Remove(id);
            }

        }


        public IEnumerable<FAQEntry> GetAll()
        {
            return faqs.Values.ToList();
        }

        public List<FAQEntry> GetByCategory(FAQCategoryEnum category)
        { 
            return faqs.Values
                .Where(f => category == FAQCategoryEnum.All || f.Category == category)
                .ToList();
        }

        public void IncrementViewCount(int id)
        {
            var faq = GetById(id);
            if (faq != null)
                faq.ViewCount++;
        }

        public void IncrementWasHelpfulVotes(int id)
        {
            var faq = GetById(id);
            if (faq != null)
                faq.WasHelpfulVotes++;
        }

        public void IncrementWasNotHelpfulVotes(int id)
        {
            var faq = GetById(id);
            if (faq != null)
                faq.WasNotHelpfulVotes++;
        }
    }
}