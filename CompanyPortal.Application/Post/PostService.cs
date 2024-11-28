using AutoMapper;
using CompanyPortal.Application.Abstractions.Post;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Post;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Application.Post
{
    public class PostService : IPostService
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IMapper _mapper;

        public PostService(
            IPostReadRepository postReadRepository,
            IPostWriteRepository postWriteRepository,
            IMapper mapper)
        {
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddPost(PostDto post)
        {
            if (post is null)
                return false;

            try
            {
                var mappedResult = _mapper.Map<PostDto, Domain.Entities.Post>(post);
                
                var response = await _postWriteRepository.AddAsync(mappedResult);
                
                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePost(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                return await _postWriteRepository.RemoveAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditPost(PostDto post)
        {
            if (post is null)
                return false;

            try
            {
                var postEntity = await _postReadRepository.GetFirstOrDefaultAsync(x => x.Id == post.Id);

                if (postEntity is null)
                    return false;

                var updateEntity = _mapper.Map<PostDto, Domain.Entities.Post>(post);

                var response = _postWriteRepository.Update(updateEntity);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PostDto>> GetAllPosts()
        {
            try
            {
                var posts = await _postReadRepository.GetQueryable()
                                                     .Where(x => x.Status)
                                                     .ToListAsync();

                if (posts is null)
                    return [];

                var mappedResult = _mapper.Map<List<Domain.Entities.Post>, List<PostDto>>(posts);

                return mappedResult;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<PostDto> GetPostById(Guid id)
        {
            if (id == Guid.Empty)
                return new PostDto();

            try
            {
                var post = await _postReadRepository.GetByIdAsync(id);

                var mappedResult = _mapper.Map<Domain.Entities.Post, PostDto>(post);

                return mappedResult;
            }
            catch (Exception)
            {
                return new PostDto();
            }
        }

        public async Task<bool> SoftDeletePost(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                var postEntity = await _postReadRepository.GetByIdAsync(id);

                if (postEntity is null)
                    return false;

                postEntity.Status = false;

                await _postWriteRepository.SaveAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
