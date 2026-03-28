namespace CloudSpritzers1.src.model.faq
{
    public class FAQEntry
    {
        private int _id;
        private string _question;
        private string _answer;
        private FAQCategoryEnum _category;
        private int _viewCount;
        private int _wasHelpfulVotes;
        private int _wasNotHelpfulVotes;

        public FAQEntry(int id, string question, string answer, FAQCategoryEnum category)
        {
            _id = id;
            _question = question;
            _answer = answer;
            _category = category;
            _viewCount = 0;
            _wasHelpfulVotes = 0;
            _wasNotHelpfulVotes = 0;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetQuestion()
        {
            return _question;
        }

        public string GetAnswer()
        {
            return _answer;
        }

        public FAQCategoryEnum GetCategory()
        {
            return _category;
        }

        public int GetViewCount()
        {
            return _viewCount;
        }

        public int GetWasHelpfulVotes()
        {
            return _wasHelpfulVotes;
        }

        public int GetWasNotHelpfulVotes()
        {
            return _wasNotHelpfulVotes;
        }

        public void IncrementViewCount()
        {
            _viewCount++;
        }

        public void IncrementWasHelpfulVotes()
        {
            _wasHelpfulVotes++;
        }

        public void IncrementWasNotHelpfulVotes()
        {
          _wasNotHelpfulVotes++;
        }

     
    }
}