using System.Collections.Generic;
using System.Linq;
using CloudSpritzers1.src.model.faq;
using CloudSpritzers1.src.repository;

namespace CloudSpritzers1.src.service
{
	public class FAQService
	{
		private readonly FAQRepository _faqRepository;

		public FAQService(FAQRepository faqRepository)
		{
			_faqRepository = faqRepository;
		}

		public List<FAQEntry> GetAll()
		{
			return _faqRepository.GetAll().ToList();
		}

        public List<FAQEntry> GetByCategory(FAQCategoryEnum category)
        {
			return _faqRepository.GetByCategory(category);
        }

		public void AddFAQEntry(FAQEntry newElem)
		{
			_faqRepository.Add(newElem);
		}

		public void EditFAQEntry(FAQEntry tempEntry, int faqEntryId)
		{
			_faqRepository.UpdateById(faqEntryId, tempEntry);
		}

		public void DeleteFAQEntry(int entryId)
		{
			_faqRepository.DeleteById(entryId);
		}

		public void IncrementViewCount(FAQEntry entry)
		{
			_faqRepository.IncrementViewCount(entry.Id);
		}

		public void IncrementWasHelpfulVotes(FAQEntry entry)
		{
			_faqRepository.IncrementWasHelpfulVotes(entry.Id);
		}

        public void IncrementWasNotHelpfulVotes(FAQEntry entry)
        {
			_faqRepository.IncrementWasNotHelpfulVotes(entry.Id);
            
        }




    }

}