using AutoMapper;
using CloudSpritzers1.src.model.review;
using CloudSpritzers1.src.DTO.CloudSpritzers1.src.DTO;

namespace CloudSpritzers1.src.dto.mappingProfiles
{
    public class RevieMappingProfile : Profile
    {
        public RevieMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("ReviewMappingProfile Loaded!");
            CreateMap<Review, ReviewDTO>()
            .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.GetId()))

            .ForMember(dest => dest.UserId,
                        opt => opt.MapFrom(src => src.GetUser().UserId))

            .ForMember(dest => dest.Message,
                        opt => opt.MapFrom(src => src.GetMessage()))

            .ForMember(dest => dest.DutyFreeRating,
                        opt => opt.MapFrom(src => src.GetDutyFreeRating()))

            .ForMember(dest => dest.FlightExperienceRating,
                        opt => opt.MapFrom(src => src.GetFlightExperienceRating()))

            .ForMember(dest => dest.StaffFriendlinessRating,
                        opt => opt.MapFrom(src => src.GetStaffFriendlinessRating()))

            .ForMember(dest => dest.CleanlinessRating,
                        opt => opt.MapFrom(src => src.GetCleanlinessRating()))

            .ConstructUsing(src => new ReviewDTO(
                src.GetId(),
                src.GetUser().UserId,
                src.GetMessage(),
                src.GetDutyFreeRating(),
                src.GetFlightExperienceRating(),
                src.GetStaffFriendlinessRating(),
                src.GetCleanlinessRating()
            ));
        }
    }
}
