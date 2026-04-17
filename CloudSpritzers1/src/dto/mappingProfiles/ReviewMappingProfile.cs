using AutoMapper;
using CloudSpritzers1.src.model.review;
using CloudSpritzers1.src.dto;
using Microsoft.IdentityModel.Tokens;

namespace CloudSpritzers1.src.dto.mappingProfiles
{
    public class ReviewMappingProfile : Profile
    {
        const int OVERALL_RATING = 0;
        public ReviewMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("ReviewMappingProfile Loaded!");
            CreateMap<Review, ReviewDTO>()

            .ConstructUsing(src => new ReviewDTO(
                src.GetId(),
                src.GetUser().UserId,
                src.GetUser().GetFullName(),
                src.GetMessage(),
                src.GetDutyFreeRating(),
                src.GetFlightExperienceRating(),
                src.GetStaffFriendlinessRating(),
                src.GetCleanlinessRating(),
                OVERALL_RATING
            ));
        }
    }
}
