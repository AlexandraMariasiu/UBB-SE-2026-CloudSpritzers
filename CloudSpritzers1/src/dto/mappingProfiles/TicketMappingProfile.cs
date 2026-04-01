using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.dto.mappingProfiles
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("TicketMappingProfile Loaded!");
            CreateMap<Ticket, TicketDTO>();
        }
    }
}
