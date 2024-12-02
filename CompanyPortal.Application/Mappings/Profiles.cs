using AutoMapper;
using CompanyPortal.Application.Abstractions.Comment.Dtos;
using CompanyPortal.Application.Abstractions.Department.Dtos;
using CompanyPortal.Application.Abstractions.Like.Dtos;
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
            CreateMap<Domain.Entities.AppUser, ProfileDto>().ReverseMap();
            CreateMap<Domain.Entities.Like, LikeDto>().ReverseMap();
            CreateMap<Domain.Entities.Comment, CommentDto>().ReverseMap();
        }
    }
}
