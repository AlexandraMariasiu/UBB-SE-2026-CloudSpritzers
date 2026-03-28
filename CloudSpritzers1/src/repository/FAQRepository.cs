using System;
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
            if (!_faqs.ContainsKey(id))
                throw new KeyNotFoundException($"FAQ with id {id} was not found.");

            return _faqs[id];
        }

        public int Add(FAQEntry elem)
        {
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "FAQ entry cannot be null.");

            if (_faqs.ContainsKey(elem.GetId()))
                throw new InvalidOperationException($"FAQ with id {elem.GetId()} already exists.");
   
            _faqs[elem.GetId()] = elem;
            return elem.GetId();
        }

        public void UpdateById(int id, FAQEntry elem)
        {

            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "FAQ entry cannot be null.");

            if (!_faqs.ContainsKey(id))
                throw new KeyNotFoundException($"FAQ with id {id} was not found.");

          
            _faqs[id] = elem;
         
        }

        public void DeleteById(int id)
        {

            if (!_faqs.ContainsKey(id))
                throw new KeyNotFoundException($"FAQ with id {id} was not found.");

            _faqs.Remove(id);
          
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
            faq.IncrementViewCount();
        }

        public void IncrementWasHelpfulVotes(int id)
        {
            var faq = GetById(id);
            faq.IncrementWasHelpfulVotes();
        }

        public void IncrementWasNotHelpfulVotes(int id)
        {
            var faq = GetById(id);
            faq.IncrementWasNotHelpfulVotes();
        }
    }
}