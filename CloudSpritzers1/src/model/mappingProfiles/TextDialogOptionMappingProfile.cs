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
            CreateMap<TextDialogOption, TextDialogOptionDTO>()

            //there might be a need for multiple fields in the future
            .ForMember(dest => dest.Message,
                       opt => opt.MapFrom(src => src.GetMessage()))

            .ConstructUsing(src => new TextDialogOptionDTO(
                src.GetMessage()
            ));
        }
    }
}
