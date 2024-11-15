using CompanyPortal.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace CompanyPortal.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Gender Gender { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; } = true;

        //Connections
        public Guid DeptId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Chat>? Chats { get; set; }
        public ICollection<ChatMessage>? Messages {get; set;}
    }
}

