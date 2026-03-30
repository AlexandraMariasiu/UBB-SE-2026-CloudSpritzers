using CloudSpritzers1.src.model.faq;

namespace CloudSpritzers1.src.DTO
{
    public record FAQEntryDTO(
        int Id, 
        string Question, 
        string Answer, 
        FAQCategoryEnum Category, 
        int ViewCount,
        int WasHelpfulVotes,
        int WasNotHelpfulVotes
    ) { }
}