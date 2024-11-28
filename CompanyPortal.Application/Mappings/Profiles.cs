using AutoMapper;
using CompanyPortal.Application.Abstractions.Department.Dtos;
using CompanyPortal.Application.Abstractions.Post.Dtos;

namespace CompanyPortal.Application.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Domain.Entities.Department, DepartmentDto>().ReverseMap();
            CreateMap<Domain.Entities.Post, PostDto>().ReverseMap();
        }
    }
}
