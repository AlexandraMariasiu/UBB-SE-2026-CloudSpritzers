using CloudSpritzers1.src.model.faq;

namespace CloudSpritzers1.src.DTO
{
    public class FAQEntryDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public FAQCategoryEnum Category { get; set; }
        public int ViewCount { get; set; }
        public int WasHelpfulVotes { get; set; }
        public int WasNotHelpfulVotes { get; set; }
    }
}