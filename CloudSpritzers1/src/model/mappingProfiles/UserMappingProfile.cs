using AutoMapper;
using CloudSpritzers1.src.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.mappingProfiles
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
