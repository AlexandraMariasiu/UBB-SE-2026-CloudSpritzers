using AutoMapper;
using CloudSpritzers1.src.model.review;
using CloudSpritzers1.src.dto;

namespace CloudSpritzers1.src.model.mappingProfiles
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("ReviewMappingProfile Loaded!");
            CreateMap<Review, ReviewDTO>()
            .ForMember(dest => dest.id,
                        opt => opt.MapFrom(src => src.GetId()))

            .ForMember(dest => dest.userId,
                        opt => opt.MapFrom(src => src.GetUser().UserId))

            .ForMember(dest => dest.userName,
                        opt => opt.MapFrom(src => src.GetUser().GetName()))

            .ForMember(dest => dest.message,
                        opt => opt.MapFrom(src => src.GetMessage()))

            .ForMember(dest => dest.dutyFreeRating,
                        opt => opt.MapFrom(src => src.GetDutyFreeRating()))

            .ForMember(dest => dest.flightExperienceRating,
                        opt => opt.MapFrom(src => src.GetFlightExperienceRating()))

            .ForMember(dest => dest.staffFriendlinessRating,
                        opt => opt.MapFrom(src => src.GetStaffFriendlinessRating()))

            .ForMember(dest => dest.cleanlinessRating,
                        opt => opt.MapFrom(src => src.GetCleanlinessRating()))

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
