using CompanyPortal.Application.Abstractions.Base;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyPortal.Application.Abstractions.Post.Dtos
{
    public class PostDto : BaseDto
    {
        public string? Header { get; set; }
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public string? VideoUrl { get; set; }  //??
        public DateTime EditedDate { get; set; }
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;

        //Connections
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Domain.Entities.Like>? Likes { get; set; }
    }
}
