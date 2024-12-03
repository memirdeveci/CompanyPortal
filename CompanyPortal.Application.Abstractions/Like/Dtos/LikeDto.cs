using CompanyPortal.Domain.Entities;

namespace CompanyPortal.Application.Abstractions.Like.Dtos
{
    public class LikeDto
    {
        public char? LikeType { get; set; }

        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public Guid PostId { get; set; }
        public Domain.Entities.Post? Post { get; set; }
        public Guid? CommentId { get; set; }
        public Domain.Entities.Comment? Comment { get; set; }
    }
}
