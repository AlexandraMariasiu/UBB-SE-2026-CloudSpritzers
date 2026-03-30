using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.DTO
{
    namespace CloudSpritzers1.src.DTO
    {
        public record ReviewDTO(
            int id,
            int userId,
            string message,
            int dutyFreeRating,
            int flightExperienceRating,
            int staffFriendlinessRating,
            int cleanlinessRating
        );
    }
}
