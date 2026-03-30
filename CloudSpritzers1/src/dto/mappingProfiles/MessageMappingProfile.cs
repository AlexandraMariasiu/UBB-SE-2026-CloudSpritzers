using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.DTO;
using CloudSpritzers1.src.model.message;
using CloudSpritzers1.src.service.bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.dto.mappingProfiles
{

    // FIXME: add for membors for each of the mappings
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile() {
            System.Diagnostics.Debug.WriteLine("MessageMappingProfile Loaded!");
            CreateMap<BotMessage, MessageDTO>()
            .ForMember(dest => dest.MessageId, 
                opt => opt.MapFrom(src => src.GetId()))
            
            .ForMember(dest => dest.MessageText, 
                opt => opt.MapFrom(src => src.GetMessage()))
            
            .ForMember(dest => dest.SenderId, 
                opt => opt.MapFrom(src => BotEngine.BOT_CANNONIZED_ID))
            
            .ForMember(dest => dest.Timestamp, 
                opt => opt.MapFrom(src => ((IMessage)src).GetTimeStamp()))
            
            .ForMember(dest => dest.FaqOptions, 
                opt => opt.MapFrom(src => ((IMessage)src).GetNextOptions()))
            
            .ForMember(dest => dest.ChatId, 
                opt => opt.MapFrom(src => src.GetChat().ChatId));

            CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.MessageId, 
                    opt => opt.MapFrom(src => src.GetId()))
                
                .ForMember(dest => dest.MessageText, 
                    opt => opt.MapFrom(src => src.GetMessage()))
                
                .ForMember(dest => dest.SenderId, 
                    opt => opt.MapFrom(src => src.GetSender().GetId()))
                
                .ForMember(dest => dest.Timestamp, 
                    opt => opt.MapFrom(src => ((IMessage)src).GetTimeStamp()))
                
                .ForMember(dest => dest.FaqOptions, 
                    opt => opt.MapFrom(src => ((IMessage)src).GetNextOptions()))
                
                .ForMember(dest => dest.ChatId, 
                    opt => opt.MapFrom(src => src.GetChat().ChatId));
        }
    }
}
