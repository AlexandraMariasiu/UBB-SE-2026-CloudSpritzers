using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.model.chat;
using CloudSpritzers1.src.dto;
using AutoMapper;

namespace CloudSpritzers1.src.dto.mappingProfiles
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("ChatMappingProfile Loaded!");
            CreateMap<Chat, ChatDTO>();
        }
    }
}
