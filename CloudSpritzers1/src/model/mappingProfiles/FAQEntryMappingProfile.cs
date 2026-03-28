using AutoMapper;
using CloudSpritzers1.src.model.message;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model.faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.DTO;

namespace CloudSpritzers1.src.model.mappingProfiles
{
    public class FAQEntryMappingProfile : Profile
    {
        public FAQEntryMappingProfile()
        {
            CreateMap<FAQEntry, FAQEntryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GetId()))
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.GetQuestion()))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.GetAnswer()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.GetCategory()))
                .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => src.GetViewCount()))
                .ForMember(dest => dest.WasHelpfulVotes, opt => opt.MapFrom(src => src.GetWasHelpfulVotes()))
                .ForMember(dest => dest.WasNotHelpfulVotes, opt => opt.MapFrom(src => src.GetWasNotHelpfulVotes()));
        }
    }
}
