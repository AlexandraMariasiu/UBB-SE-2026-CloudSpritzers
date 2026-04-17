using System;
using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model.message;
using CloudSpritzers1.src.service;
using CloudSpritzers1.src.service.bot;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<IMessage, MessageDTO>()
            .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.GetMessage()))
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src =>
                new DateTimeOffset(((IMessage)src).GetTimeStamp().Ticks, TimeSpan.Zero)))
            .ForMember(dest => dest.FaqOptions, opt => opt.MapFrom(src => src.GetNextOptions()))
            .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.GetChat().ChatId))
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.GetSender().GetFullName()))
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.GetSender().GetId()));

        CreateMap<BotMessage, MessageDTO>()
            .IncludeBase<IMessage, MessageDTO>()
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => BotEngine.BOT_CANNONIZED_ID));

        CreateMap<Message, MessageDTO>()
            .IncludeBase<IMessage, MessageDTO>();
    }
}