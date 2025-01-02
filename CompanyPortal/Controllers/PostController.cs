using CloudinaryDotNet.Actions;
using CompanyPortal.Application.Abstractions.Post;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using CompanyPortal.Domain.Entities;
using CompanyPortal.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;

        public PostController(IPostService postService, IPhotoService photoService, UserManager<AppUser> userManager)
        {
            _postService = postService;
            _photoService = photoService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPosts();
            posts.Sort((x, y) => DateTime.Compare(y.CreatedDate, x.CreatedDate));

            var user = await _userManager.GetUserAsync(User);

            foreach (var item in posts)
            {
                item.isLiked = item.Likes.Any(x => x.User == user && x.LikeType == 'L');
                item.isDisliked = item.Likes.Any(x => x.User == user && x.LikeType == 'D');
            }

            return View(posts);
        }

        public IActionResult CreatePost()
        {
            var post = new PostDto();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDto post)
        {
            await PrepareContent(post);

            await _postService.AddPost(post, User);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            await _postService.DeletePost(id);

            return RedirectToAction("Index");
        }

        #region Utility
        private async Task PrepareContent(PostDto post)
        {
            if (post.Image != null)
            {
                if (post.Image.ContentType.Contains("video"))
                {
                    var videoResult = await _photoService.AddVideoAsync(post.Image);
                    post.ImageUrl = videoResult.Url.ToString();
                    post.ContentType = nameof(ResourceType.Video);
                }
                else
                {
                    var imageResult = await _photoService.AddPhotoAsync(post.Image);
                    post.ImageUrl = imageResult.Url.ToString();
                    post.ContentType = nameof(ResourceType.Image);
                }
            }
        }
        #endregion
    }
}
