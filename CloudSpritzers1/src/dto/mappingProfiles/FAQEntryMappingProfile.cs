using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Formats.Tar;
using AutoMapper;
using CloudSpritzers1.Src.Model.Message;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Model.Faq;

namespace CloudSpritzers1.Src.Dto.MappingProfiles
{
    public class FAQEntryMappingProfile : Profile
    {
        public FAQEntryMappingProfile()
        {
            CreateMap<FAQEntry, FAQEntryDTO>()
                .ConstructUsing(source => new FAQEntryDTO(
                    source.Id,
                    source.Question,
                    source.Answer,
                    source.Category,
                    source.ViewCount,
                    source.HelpfulVotesCount,
                    source.NotHelpfulVotesCount));

            CreateMap<FAQEntryDTO, FAQEntry>()
                .ConstructUsing(source => new FAQEntry(
                    source.Id,
                    source.Question,
                    source.Answer,
                    source.Category,
                    source.ViewCount,
                    source.HelpfulVotesCount,
                    source.NotHelpfulVotesCount));
    }
    }
}
