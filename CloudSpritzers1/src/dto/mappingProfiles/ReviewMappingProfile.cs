using AutoMapper;
using CloudSpritzers1.src.model.review;
using CloudSpritzers1.src.dto;

namespace CloudSpritzers1.src.dto.mappingProfiles
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("ReviewMappingProfile Loaded!");
            CreateMap<Review, ReviewDTO>()

            .ConstructUsing(src => new ReviewDTO(
                src.GetId(),
                src.GetUser().UserId,
                src.GetUser().GetName(),
                src.GetMessage(),
                src.GetDutyFreeRating(),
                src.GetFlightExperienceRating(),
                src.GetStaffFriendlinessRating(),
                src.GetCleanlinessRating(),
                0
            ));
        }
    }
}
