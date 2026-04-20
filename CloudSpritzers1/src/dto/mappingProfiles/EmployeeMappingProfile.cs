using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model.employee;

namespace CloudSpritzers1.src.dto.mappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
