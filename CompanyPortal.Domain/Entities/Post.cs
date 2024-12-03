using CompanyPortal.Domain.Entities.Common;

namespace CompanyPortal.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string? Header { get; set; }
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }
        public string? ContentType { get; set; }
        public string? VideoUrl { get; set; }  //??
        public DateTime EditedDate { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int CommentCount { get; set; }

        //Connections
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like>? Likes { get; set; }
    }
}
