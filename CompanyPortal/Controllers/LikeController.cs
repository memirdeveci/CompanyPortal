using CompanyPortal.Application.Abstractions.Like;
using CompanyPortal.Application.Abstractions.Like.Dtos;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLike(LikeDto likeDto, string itemType)
        {
            var response = await _likeService.AddLike(likeDto, User, itemType);

            return Json(new {});
        }

        [HttpPost]
        public IActionResult DeleteLike()
        {
            return Json(new { });
        }
    }
}
