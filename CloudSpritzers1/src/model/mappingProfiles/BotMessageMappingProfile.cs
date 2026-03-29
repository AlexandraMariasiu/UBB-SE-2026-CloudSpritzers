using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.mappingProfiles
{
    public class BotMessageMappingProfile : Profile
    {
        public BotMessageMappingProfile() {
            System.Diagnostics.Debug.WriteLine("BotMessageMappingProfile Loaded!");
            CreateMap<BotMessage, BotMessageDTO>();
        }
    }
}
