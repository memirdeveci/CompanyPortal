using CompanyPortal.Application.Abstractions.Post.Dtos;

namespace CompanyPortal.Application.Abstractions.Post
{
    public interface IPostService
    {
        Task<bool> AddPost(PostDto post);
        Task<bool> DeletePost(Guid id);
        Task<bool> EditPost(PostDto post);
        Task<PostDto> GetPostById(Guid id);
        Task<List<PostDto>> GetAllPosts();
        Task<bool> SoftDeletePost(Guid id);
    }
}
