using AutoMapper;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}