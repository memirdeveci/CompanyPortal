using CompanyPortal.Application.Abstractions.Post;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
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
