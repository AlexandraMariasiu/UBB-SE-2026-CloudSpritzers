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
            System.Diagnostics.Debug.WriteLine("UserMappingProfile Loaded!");
            CreateMap<User, UserDTO>();
        }
    }
}
