using AutoMapper;
using CompanyPortal.Application.Abstractions.Like;
using CompanyPortal.Application.Abstractions.Like.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Like;
using CompanyPortal.Application.Abstractions.Repositories.Post;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompanyPortal.Application.Like
{
    public class LikeService : ILikeService
    {
        private readonly ILikeWriteRepository _likeWriteRepository;
        private readonly ILikeReadRepository _likeReadRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPostReadRepository _postReadRepository;
        private readonly IMapper _mapper;

        public LikeService(ILikeWriteRepository likeWriteRepository, ILikeReadRepository likeReadRepository, IMapper mapper, UserManager<AppUser> userManager, IPostReadRepository postReadRepository)
        {
            _likeReadRepository = likeReadRepository;
            _likeWriteRepository = likeWriteRepository;
            _userManager = userManager;
            _postReadRepository = postReadRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddLike(LikeDto like, ClaimsPrincipal principal, string itemType)
        {
            if (like is null)
                return false;

            try
            {
                var user = await _userManager.GetUserAsync(principal);
                var post = await _postReadRepository.GetByIdAsync(like.PostId);

                var canAdd = await LikeControl(post.Id, user.Id, like.LikeType);

                if (!canAdd)
                    return true;

                var mappedResult = _mapper.Map<LikeDto, Domain.Entities.Like>(like);
                mappedResult.User = user;
                
                if (like.LikeType == 'L')
                    post.LikeCount++;
                else
                    post.DislikeCount++;

                var response = await _likeWriteRepository.AddAsync(mappedResult);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLike(LikeDto like)
        {
            if (like.Id == Guid.Empty)
                return false;

            try
            {
                var post = await _postReadRepository.GetByIdAsync(like.PostId);

                if (like.LikeType == 'L')
                    post.LikeCount--;
                else
                    post.DislikeCount--;

                return await _likeWriteRepository.RemoveAsync(like.Id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<LikeDetailDto> GetLikeDetail(LikeDto like, ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            var likeDetail = await _postReadRepository.GetQueryable()
                                                      .Include(x => x.Likes)
                                                      .Include(x => x.User)
                                                      .Where(x => x.Id == like.PostId)
                                                      .Select(x => new LikeDetailDto
                                                      {
                                                          LikeCount = x.LikeCount,
                                                          DislikeCount = x.DislikeCount,
                                                          IsLiked = (x.Likes != null && x.Likes.Where(x => x.User == user && x.LikeType == 'L').Any()),
                                                          IsDisliked = (x.Likes != null && x.Likes.Where(x => x.User == user && x.LikeType == 'D').Any())
                                                      }).FirstOrDefaultAsync();

            return likeDetail;
        }

        private async Task<bool> LikeControl(Guid postId, Guid userId, char? type)
        {
            var like = await _likeReadRepository.GetFirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
            var mappedResult = _mapper.Map<Domain.Entities.Like, LikeDto>(like);

            if (like == null)
                return true;

            await DeleteLike(mappedResult);

            return like.LikeType != type;
        }
    
        //private bool CheckLike(Domain.Entities.Post post, char type)
        //{
        //    var like = _likeReadRepository.GetQueryable().Where(x => x.User )
        //}
    }
}
