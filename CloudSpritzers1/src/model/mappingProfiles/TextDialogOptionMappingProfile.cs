using AutoMapper;
using CloudSpritzers.src.dto;
using CloudSpritzers.src.model.message;
using CloudSpritzers1.src.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.mappingProfiles
{
    public class TextDialogOptionMappingProfile : Profile
    {
        public TextDialogOptionMappingProfile() {
            System.Diagnostics.Debug.WriteLine("TextDialogMappingProfile Loaded!");
            CreateMap<TextDialogOption, TextDialogOptionDTO>();
        }
    }
}
