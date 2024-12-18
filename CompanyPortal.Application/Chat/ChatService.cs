using AutoMapper;
using CompanyPortal.Application.Abstractions.Chat;
using CompanyPortal.Application.Abstractions.Chat.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Chat;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompanyPortal.Application.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatReadRepository _chatReadRepository;
        private readonly IChatWriteRepository _chatWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ChatService(IChatReadRepository chatReadRepository,
            IChatWriteRepository chatWriteRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _chatReadRepository = chatReadRepository;
            _chatWriteRepository = chatWriteRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> AddChat(ChatDto chat, ClaimsPrincipal principal)
        {
            if (chat is null)
                return false;

            try
            {
                var user = await _userManager.GetUserAsync(principal);
                var mappedResult = _mapper.Map<ChatDto, Domain.Entities.Chat>(chat);

                //mappedResult.User = user;

                var response = await _chatWriteRepository.AddAsync(mappedResult);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteChat(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                return await _chatWriteRepository.RemoveAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditChat(ChatDto chat)
        {
            if (chat is null)
                return false;

            try
            {
                var chatEntity = await _chatReadRepository.GetFirstOrDefaultAsync(x => x.Id == chat.Id);

                if (chatEntity is null)
                    return false;

                var updateEntity = _mapper.Map(chat, chatEntity);

                var response = _chatWriteRepository.Update(updateEntity);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ChatDto>> GetAllChat()
        {
            try
            {
                var chats = await _chatReadRepository.GetQueryable()
                                                     .Include(x => x.Users)
                                                     .Where(x => x.Status)
                                                     .ToListAsync();

                if (chats is null)
                    return [];

                var mappedResult = _mapper.Map<List<Domain.Entities.Chat>, List<ChatDto>>(chats);

                return mappedResult;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public Task<ChatDto> GetChatById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChatDto>> GetUserChats(ClaimsPrincipal principal)
        {
            try
            {
                var user = await _userManager.GetUserAsync(principal);

                var chats = _chatReadRepository.GetQueryable()
                                               .Include(x => x.Users)
                                               .Where(x => x.Status)
                                               .AsEnumerable()
                                               .Where(x => x.Users.Contains(user))
                                               .ToList();

                if (chats is null)
                    return [];

                var mappedResult = _mapper.Map<List<Domain.Entities.Chat>, List<ChatDto>>(chats);

                return mappedResult;
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}
