using AutoMapper;
using CloudSpritzers.src.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers.src.model.mappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            System.Diagnostics.Debug.WriteLine("EmployeeMappingProfile Loaded!");
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
