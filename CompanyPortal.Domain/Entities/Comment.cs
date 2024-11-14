using CompanyPortal.Domain.Entities.Common;

namespace CompanyPortal.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public  string? Text { get; set; }
        public DateTime EditedDate { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        //Connections
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public Guid PostId { get; set; }
        public Post? Post { get; set; }
        public ICollection<Like>? Likes { get; set; }
    }
}
