using AutoMapper;
using CloudSpritzers.src.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers.src.model.mappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>()
            
            .ForMember(dest => dest.Name,
                       opt => opt.MapFrom(src => src.GetName()))
            
            .ForMember(dest => dest.Email,
                       opt => opt.MapFrom(src => src.GetEmail()))

            .ConstructUsing(src => new UserDTO(
                src.GetName(),
                src.GetEmail()
            ));
        }
    }
}
