using AutoMapper;
using CompanyPortal.Application.Abstractions.Like;
using CompanyPortal.Application.Abstractions.Like.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Like;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CompanyPortal.Application.Like
{
    public class LikeService : ILikeService
    {
        private readonly ILikeWriteRepository _likeWriteRepository;
        private readonly ILikeReadRepository _likeReadRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public LikeService(ILikeWriteRepository likeWriteRepository, ILikeReadRepository likeReadRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _likeReadRepository = likeReadRepository;
            _likeWriteRepository = likeWriteRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> AddLike(LikeDto like, ClaimsPrincipal principal, string itemType)
        {
            if (like is null)
                return false;

            try
            {
                var user = await _userManager.GetUserAsync(principal);

                var mappedResult = _mapper.Map<LikeDto, Domain.Entities.Like>(like);
                mappedResult.User = user;

                var response = await _likeWriteRepository.AddAsync(mappedResult);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLike(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                return await _likeWriteRepository.RemoveAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
