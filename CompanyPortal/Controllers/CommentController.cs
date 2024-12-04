using CompanyPortal.Application.Abstractions.Comment;
using CompanyPortal.Application.Abstractions.Comment.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> GetComments(string postId)
        {
            var comments = await _commentService.GetAllComments(Guid.Parse(postId));
            return PartialView(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto comment)
        {
            await _commentService.AddComment(comment, User);
            return Json(new { post = comment.PostId});
        }
    }
}
