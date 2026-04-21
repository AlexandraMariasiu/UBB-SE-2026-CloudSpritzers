using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.Src.Model.Chats;
using CloudSpritzers1.Src.Dto;
using AutoMapper;

namespace CloudSpritzers1.Src.Dto.MappingProfiles
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
