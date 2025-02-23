﻿using CompanyPortal.Application.Abstractions.Comment;
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
            comments.Sort((x, y) => DateTime.Compare(y.CreatedDate, x.CreatedDate));
            return PartialView(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto comment)
        {
            await _commentService.AddComment(comment, User);
            return Json(new { post = comment.PostId});
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _commentService.DeleteComment(id);
            return RedirectToAction("Index", "Post");
        }
    }
}
