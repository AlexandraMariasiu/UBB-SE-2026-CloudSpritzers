using CloudSpritzers1.src.model.faq;

namespace CloudSpritzers1.src.DTO
{
    public class FAQEntryDTO
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public FAQCategoryEnum Category { get; set; }
        public int ViewCount { get; set; }
        public int WasHelpfulVotes { get; set; }
        public int WasNotHelpfulVotes { get; set; }

        public FAQEntryDTO() { }

        public FAQEntryDTO(FAQEntry entry)
        {
            Id = entry.GetId();
            Question = entry.GetQuestion();
            Answer = entry.GetAnswer();
            Category = entry.GetCategory();
            ViewCount = entry.GetViewCount();
            WasHelpfulVotes = entry.GetWasHelpfulVotes();
            WasNotHelpfulVotes = entry.GetWasNotHelpfulVotes();
        }
    }
}