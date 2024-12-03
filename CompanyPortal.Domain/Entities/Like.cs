using CompanyPortal.Domain.Entities.Common;

namespace CompanyPortal.Domain.Entities
{
    public class Like : BaseEntity
    {
        public char? LikeType { get; set; }     //Like or Dislike

        //Connections
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
        public Guid? PostId { get; set; }
        public Post? Post { get; set; }
        public Guid? CommentId { get; set; }
        public Comment? Comment { get; set; }
    }
}
