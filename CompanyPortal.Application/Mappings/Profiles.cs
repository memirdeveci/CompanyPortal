using AutoMapper;
using CompanyPortal.Application.Abstractions.Department.Dtos;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using CompanyPortal.Application.Abstractions.User.Dtos;

namespace CompanyPortal.Application.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Domain.Entities.Department, DepartmentDto>().ReverseMap();
            CreateMap<Domain.Entities.Post, PostDto>().ReverseMap();
            CreateMap<Domain.Entities.AppUser, UserDto>().ReverseMap();
            CreateMap<Domain.Entities.AppUser, AdminDto>().ReverseMap();
        }
    }
}
