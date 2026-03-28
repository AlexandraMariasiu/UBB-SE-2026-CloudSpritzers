using AutoMapper;
using CloudSpritzers1.src.model.review;
using CloudSpritzers1.src.DTO;

namespace CloudSpritzers1.src.model.mappingProfiles
{
    public class RevieMappingProfile : Profile
    {
        public RevieMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("ReviewMappingProfile Loaded!");
            CreateMap<Review, ReviewDTO>();
        }
    }
}
