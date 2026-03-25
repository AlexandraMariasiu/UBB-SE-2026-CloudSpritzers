namespace CloudSpritzers.src.model.review
{
    public class Review
    {
        private int _id;
        private int _userId;
        private string _message;
        private int _dutyFreeRating;
        private int _flightExperienceRating;
        private int _staffFriendlinessRating;
        private int _cleanlinessRating;

        public Review(int id, int userId, string message, int dutyFreeRating, int flightExperienceRating, int staffFriendlinesRating, int cleanlinessRating)
        {
            this._id = id;
            this._userId = userId;
            this._message = message;
            this._dutyFreeRating = dutyFreeRating;
            this._flightExperienceRating = flightExperienceRating;
            this._staffFriendlinessRating = staffFriendlinesRating;
            this._cleanlinessRating = cleanlinessRating;
        }

        //GETTERS

        public int GetId() 
        { 
            return this._id; 
        }

        public int GetUserId() 
        { 
            return this._userId; 
        }
        
        public string GetMessage() 
        { 
            return this._message; 
        }
        
        public int GetDutyFreeRating()
        {
            return this._dutyFreeRating;
        }
        
        public int GetFlightExperienceRating()
        {
            return this._flightExperienceRating;
        }
        
        public int GetStaffFriendlinessRating()
        {
            return this._staffFriendlinessRating;
        }
        
        public int GetCleanlinessRating()
        {
            return this._cleanlinessRating;
        }

    }
}
