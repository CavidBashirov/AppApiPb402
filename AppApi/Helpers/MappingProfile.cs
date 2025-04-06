using AppApi.DTOs.Employee;
using AppApi.Models;
using AutoMapper;
using System.Globalization;

namespace AppApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToString("yyyy-MM-dd")));
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeEditDto, Employee>();
        }
    }
}
