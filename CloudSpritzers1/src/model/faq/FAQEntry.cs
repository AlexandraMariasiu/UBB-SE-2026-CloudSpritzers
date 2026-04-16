namespace CloudSpritzers1.src.model.faq
{
    public class FAQEntry
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public FAQCategoryEnum Category { get; set; }
        public int ViewCount { get; set; }
        public int HelpfulVotesCount { get; set; }
        public int NotHelpfulVotesCount { get; set; }

        public FAQEntry(int id, string question, string answer, FAQCategoryEnum category, int viewCount, int wasHelpfulVotes, int wasNotHelpfulVotes)
        {
            Id = id;
            Question = question;
            Answer = answer;
            Category = category;
            ViewCount = viewCount;
            HelpfulVotesCount = wasHelpfulVotes;
            NotHelpfulVotesCount = wasNotHelpfulVotes;
        }

        public void IncrementViewCount()
        {
            ViewCount++;
        }

        public void IncrementWasHelpfulVotes()
        {
            HelpfulVotesCount++;
        }

        public void IncrementWasNotHelpfulVotes()
        {
          NotHelpfulVotesCount++;
        }

     
    }
}