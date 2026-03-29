using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.DTO
{
    public class ReviewDTO
    {
        public int id { get; set; }
        public int userId { get; set; }   // only the id, for security of user
        public string message { get; set; }
        public int dutyFreeRating { get; set; }
        public int flightExperienceRating { get; set; }
        public int staffFriendlinessRating { get; set; }
        public int cleanlinessRating { get; set; }

        public ReviewDTO() { }

        public ReviewDTO(int id, int userId, string message, int dutyFreeRating, int flightExperienceRating, int staffFriendlinessRating, int cleanlinessRating)
        {
            this.id = id;
            this.userId = userId;
            this.message = message;
            this.dutyFreeRating = dutyFreeRating;
            this.flightExperienceRating = flightExperienceRating;
            this.staffFriendlinessRating = staffFriendlinessRating;
            this.cleanlinessRating = cleanlinessRating;
        }
    }


}
