using CompanyPortal.Application.Abstractions.Comment.Dtos;

namespace CompanyPortal.Application.Abstractions.Comment
{
    public interface ICommentService
    {
        Task<bool> AddComment(CommentDto comment);
        Task<bool> DeleteComment(Guid id);
        Task<bool> EditComment(CommentDto comment);
        Task<CommentDto> GetCommentById(Guid id);
        Task<List<CommentDto>> GetAllComments();
       // Task<bool> SoftDeleteDepartment(Guid id);
    }
}
