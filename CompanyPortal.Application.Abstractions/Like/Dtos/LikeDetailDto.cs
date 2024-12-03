using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPortal.Application.Abstractions.Like.Dtos
{
    public class LikeDetailDto 
    {
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
    }
}
