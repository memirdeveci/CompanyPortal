using CompanyPortal.Application.Abstractions.Post;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using CompanyPortal.ExternalServices;
using CompanyPortal.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPhotoService _photoService;

        public PostController(IPostService postService, IPhotoService photoService)
        {
            _postService = postService;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPosts();
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
            var imageResult = await _photoService.AddPhotoAsync(post.Image);

            post.ImageUrl = imageResult.Url.ToString();

            await _postService.AddPost(post, User);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            await _postService.DeletePost(id);

            return RedirectToAction("Index");
        }
    }
}
