using AutoMapper;
using CompanyPortal.Application.Abstractions.Comment;
using CompanyPortal.Application.Abstractions.Comment.Dtos;
using CompanyPortal.Application.Abstractions.Department.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Comment;
using CompanyPortal.Application.Abstractions.Repositories.Department;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Application.Comment
{
    public class CommentService : ICommentService
    {
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICommentWriteRepository _commentWriteRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentReadRepository commentReadRepository, ICommentWriteRepository commentWriteRepository, IMapper mapper)
        {
            _commentReadRepository = commentReadRepository;
            _commentWriteRepository = commentWriteRepository;
            _mapper = mapper;
        }

        public Task<bool> AddComment(CommentDto comment)
        {
            throw new NotImplementedException();
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

        public Task<bool> EditComment(CommentDto comment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommentDto>> GetAllComments()
        {
            try
            {
                var comments = await _commentReadRepository.GetQueryable()
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
