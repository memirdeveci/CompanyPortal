using AutoMapper;
using CompanyPortal.Application.Abstractions.Comment;
using CompanyPortal.Application.Abstractions.Comment.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Comment;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompanyPortal.Application.Comment
{
    public class CommentService : ICommentService
    {
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICommentWriteRepository _commentWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CommentService(ICommentReadRepository commentReadRepository, ICommentWriteRepository commentWriteRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _commentReadRepository = commentReadRepository;
            _commentWriteRepository = commentWriteRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> AddComment(CommentDto comment, ClaimsPrincipal principal)
        {
            if (comment is null)
                return false;

            try
            {
                var user = await _userManager.GetUserAsync(principal);
                var mappedResult = _mapper.Map<CommentDto, Domain.Entities.Comment>(comment);
                mappedResult.User = user;

                var response = await _commentWriteRepository.AddAsync(mappedResult);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                return await _commentWriteRepository.RemoveAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CommentDto>> GetAllComments(Guid PostId)
        {
            try
            {
                var comments = await _commentReadRepository.GetQueryable()
                                                           .Include(x => x.User)
                                                           .Include(x => x.Likes)   //?
                                                           .Where(x => x.Status)
                                                           .ToListAsync();

                if (comments is null)
                    return [];

                var mappedResult = _mapper.Map<List<Domain.Entities.Comment>, List<CommentDto>>(comments);

                return mappedResult;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<CommentDto> GetCommentById(Guid id)
        {
            if (id == Guid.Empty)
                return new CommentDto();

            try
            {
                var comment = await _commentReadRepository.GetByIdAsync(id);

                var mappedResult = _mapper.Map<Domain.Entities.Comment, CommentDto>(comment);

                return mappedResult;
            }
            catch (Exception)
            {
                return new CommentDto();
            }
        }
    }
}
