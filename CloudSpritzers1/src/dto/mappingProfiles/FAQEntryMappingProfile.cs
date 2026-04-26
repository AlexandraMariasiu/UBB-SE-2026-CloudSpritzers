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
                .ConstructUsing(sourceEntity => new FAQEntryDTO(
                    sourceEntity.Id,
                    sourceEntity.Question,
                    sourceEntity.Answer,
                    sourceEntity.Category,
                    sourceEntity.ViewCount,
                    sourceEntity.HelpfulVotesCount,
                    sourceEntity.NotHelpfulVotesCount));

            CreateMap<FAQEntryDTO, FAQEntry>()
                .ConstructUsing(sourceEntity => new FAQEntry(
                    sourceEntity.Id,
                    sourceEntity.Question,
                    sourceEntity.Answer,
                    sourceEntity.Category,
                    sourceEntity.ViewCount,
                    sourceEntity.HelpfulVotesCount,
                    sourceEntity.NotHelpfulVotesCount));
    }
    }
}
