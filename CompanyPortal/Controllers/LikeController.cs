using CompanyPortal.Application.Abstractions.Like;
using CompanyPortal.Application.Abstractions.Like.Dtos;
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
            await _likeService.AddLike(likeDto, User, itemType);

            var result = await _likeService.GetLikeDetail(likeDto, User);

            return Json(new { likeInfo = result});
        }
    }
}
