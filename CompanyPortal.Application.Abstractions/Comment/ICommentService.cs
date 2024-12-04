using CompanyPortal.Application.Abstractions.Comment.Dtos;
using System.Security.Claims;

namespace CompanyPortal.Application.Abstractions.Comment
{
    public interface ICommentService
    {
        Task<bool> AddComment(CommentDto comment, ClaimsPrincipal principal);
        Task<bool> DeleteComment(Guid id);
        Task<CommentDto> GetCommentById(Guid id);
        Task<List<CommentDto>> GetAllComments(Guid PostId);
    }
}
