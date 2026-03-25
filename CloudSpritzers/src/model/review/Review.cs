namespace CloudSpritzers.src.model.review
{
    public class Review
    {
        private int id;
        private int userId;
        private string message;
        private int dutyFreeRating;
        private int flightExperienceRating;
        private int staffFriendlinessRating;
        private int cleanlinessRating;

        public Review(int id, int userId, string message, int dutyFreeRating, int flightExperienceRating, int staffFriendlinesRating, int cleanlinessRating)
        {
            this.id = id;
            this.userId = userId;
            this.message = message;
            this.dutyFreeRating = dutyFreeRating;
            this.flightExperienceRating = flightExperienceRating;
            this.staffFriendlinessRating = staffFriendlinesRating;
            this.cleanlinessRating = cleanlinessRating;
        }

        //GETTERS

        public int GetId() 
        { 
            return this.id; 
        }

        public int GetUserId() 
        { 
            return this.userId; 
        }
        
        public string GetMessage() 
        { 
            return this.message; 
        }
        
        public int GetDutyFreeRating()
        {
            return this.dutyFreeRating;
        }
        
        public int GetFlightExperienceRating()
        {
            return this.flightExperienceRating;
        }
        
        public int GetStaffFriendlinessRating()
        {
            return this.staffFriendlinessRating;
        }
        
        public int GetCleanlinessRating()
        {
            return this.cleanlinessRating;
        }

    }
}
