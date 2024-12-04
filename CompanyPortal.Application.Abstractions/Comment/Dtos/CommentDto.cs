using CompanyPortal.Application.Abstractions.Base;
using CompanyPortal.Domain.Entities;

namespace CompanyPortal.Application.Abstractions.Comment.Dtos
{
    public class CommentDto : BaseDto
    {
        public string? Text { get; set; }
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;

        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public Guid PostId { get; set; }
        public Domain.Entities.Post? Post { get; set; }
        public ICollection<Domain.Entities.Like>? Likes { get; set; }
    }
}
