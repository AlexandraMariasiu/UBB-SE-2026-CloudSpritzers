using System.Collections.Generic;
using System.Linq;
using CloudSpritzers.src.model.faq;

namespace CloudSpritzers.src.repository
{
    public class FAQRepository : IRepository<int, FAQEntry>
    {
        private Dictionary<int, FAQEntry> _faqs = new Dictionary<int, FAQEntry>();

        public FAQEntry GetById(int id)
        {
            return _faqs.ContainsKey(id) ? _faqs[id] : null;
        }

        public int Add(FAQEntry elem)
        {
            if (_faqs.ContainsKey(elem.GetId()))
                return -1;

            _faqs[elem.GetId()] = elem;
            return elem.GetId();
        }

        public void UpdateById(int id, FAQEntry elem)
        {
            if (_faqs.ContainsKey(id))
            {
                _faqs[id] = elem;
            }
        }

        public void DeleteById(int id)
        {
            if (_faqs.ContainsKey(id))
            {
                _faqs.Remove(id);
            }
        }

        public IEnumerable<FAQEntry> GetAll()
        {
            return _faqs.Values.ToList();
        }

        public List<FAQEntry> GetByCategory(FAQCategoryEnum category)
        {
            return _faqs.Values
                .Where(f => category == FAQCategoryEnum.All || f.GetCategory() == category)
                .ToList();
        }

        public void IncrementViewCount(int id)
        {
            var faq = GetById(id);
            if (faq != null)
                faq.IncrementViewCount();
        }

        public void IncrementWasHelpfulVotes(int id)
        {
            var faq = GetById(id);
            if (faq != null)
                faq.IncrementWasHelpfulVotes();
        }

        public void IncrementWasNotHelpfulVotes(int id)
        {
            var faq = GetById(id);
            if (faq != null)
                faq.IncrementWasNotHelpfulVotes();
        }
    }
}