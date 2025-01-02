using AutoMapper;
using CompanyPortal.Application.Abstractions.Post;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Post;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompanyPortal.Application.Post
{
    public class PostService : IPostService
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PostService(
            IPostReadRepository postReadRepository,
            IPostWriteRepository postWriteRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> AddPost(PostDto post, ClaimsPrincipal principal)
        {
            if (post is null)
                return false;

            try
            {
                var user = await _userManager.GetUserAsync(principal);

                var mappedResult = _mapper.Map<PostDto, Domain.Entities.Post>(post);

                mappedResult.User = user;

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

                var updateEntity = _mapper.Map(post, postEntity);

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
                                                     .Include(x => x.User)
                                                     .Include(x => x.Likes)
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
