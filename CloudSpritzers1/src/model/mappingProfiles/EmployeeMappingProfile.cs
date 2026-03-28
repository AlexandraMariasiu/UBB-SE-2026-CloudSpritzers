using AutoMapper;
using CloudSpritzers1.src.DTO;
using CloudSpritzers1.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.model.mappingProfiles
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
