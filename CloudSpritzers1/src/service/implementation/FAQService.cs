using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers1.src.model.faq;
using CloudSpritzers1.src.repository.implementations;
using CloudSpritzers1.src.repository.interfaces;
using CloudSpritzers1.src.service.interfaces;
using Sprache;

namespace CloudSpritzers1.src.service.implementation
{
	public class FAQService : IFAQService
	{
		private readonly IFAQRepository faqRepository;

		public FAQService(IFAQRepository faqRepository)
		{
			this.faqRepository = faqRepository;
		}

		public List<FAQEntry> GetAll()
		{
			return faqRepository.GetAll().ToList();
		}

        public List<FAQEntry> GetByCategory(FAQCategoryEnum category)
        {
			return faqRepository.GetByCategory(category);
        }

		public void AddFAQEntry(FAQEntry newElem)
		{
			faqRepository.CreateNewEntity(newElem);
		}

		public void EditFAQEntry(FAQEntry tempEntry, int faqEntryId)
		{
			faqRepository.UpdateById(faqEntryId, tempEntry);
		}

		public void DeleteFAQEntry(int entryId)
		{
			faqRepository.DeleteById(entryId);
		}

		public void IncrementViewCount(FAQEntry entry)
		{
			faqRepository.IncrementViewCount(entry.Id);
		}

		public void IncrementWasHelpfulVotes(FAQEntry entry)
		{
			faqRepository.IncrementWasHelpfulVotes(entry.Id);
		}

        public void IncrementWasNotHelpfulVotes(FAQEntry entry)
        {
			faqRepository.IncrementWasNotHelpfulVotes(entry.Id);
        }

		public List<FAQEntry> FilterFAQEntry(FAQCategoryEnum category, string searchQuery)
		{
			var faqs = this.faqRepository.GetAll().AsEnumerable();
			if (category != FAQCategoryEnum.All)
			{
				faqs = this.GetByCategory(category);
			}

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                faqs = faqs.Where(f =>
                    (f.Question?.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (f.Answer?.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ?? false));
            }
			return faqs.ToList();
        }
    }
}