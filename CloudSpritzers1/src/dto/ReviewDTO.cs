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
            int Id,
            int UserId,
            string Message,
            int DutyFreeRating,
            int FlightExperienceRating,
            int StaffFriendlinessRating,
            int CleanlinessRating
        );
    }
}
