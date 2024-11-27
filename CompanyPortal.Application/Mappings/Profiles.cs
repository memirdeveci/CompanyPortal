using AutoMapper;
using CompanyPortal.Application.Abstractions.Department.Dtos;

namespace CompanyPortal.Application.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Domain.Entities.Department, DepartmentDto>().ReverseMap();
        }
    }
}
