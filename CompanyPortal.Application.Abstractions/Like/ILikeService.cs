using CompanyPortal.Application.Abstractions.Like.Dtos;
using System.Security.Claims;

namespace CompanyPortal.Application.Abstractions.Like
{
    public interface ILikeService
    {
        Task<bool> AddLike(LikeDto like, ClaimsPrincipal principal, string itemType);
        Task<bool> DeleteLike(Guid id);
    }
}
