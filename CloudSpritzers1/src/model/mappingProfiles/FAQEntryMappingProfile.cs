using AutoMapper;
using CloudSpritzers1.src.model.message;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model.faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.mappingProfiles
{
    public class FAQEntryMappingProfile : Profile
    {
        public FAQEntryMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("TextDialogMappingProfile Loaded!");
            CreateMap<FAQEntry, FAQEntryDTO>();
        }
    }
}
