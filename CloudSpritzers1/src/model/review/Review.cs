using CloudSpritzers1.Src.Model;

namespace CloudSpritzers1.Src.Model.Review
{
    public class Review
    {
        private int _id;
        private User _user;
        private string _message;
        private int _dutyFreeRating;
        private int _flightExperienceRating;
        private int _staffFriendlinessRating;
        private int _cleanlinessRating;

        public Review(int id, User user, string message, int dutyFreeRating, int flightExperienceRating, int staffFriendlinesRating, int cleanlinessRating)
        {
            this._id = id;
            this._user = user;
            this._message = message;
            this._dutyFreeRating = dutyFreeRating;
            this._flightExperienceRating = flightExperienceRating;
            this._staffFriendlinessRating = staffFriendlinesRating;
            this._cleanlinessRating = cleanlinessRating;
        }

        // GETTERS
        public int GetId()
        {
            return this._id;
        }

        public User GetUser()
        {
            return this._user;
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
