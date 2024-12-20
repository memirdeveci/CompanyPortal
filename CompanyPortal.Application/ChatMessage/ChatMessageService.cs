using AutoMapper;
using CompanyPortal.Application.Abstractions.ChatMessage;
using CompanyPortal.Application.Abstractions.ChatMessage.Dtos;
using CompanyPortal.Application.Abstractions.Post.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.ChatMessage;
using CompanyPortal.Application.Abstractions.Repositories.Post;
using CompanyPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompanyPortal.Application.ChatMessage
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageReadRepository _messageReadRepository;
        private readonly IChatMessageWriteRepository _messageWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ChatMessageService(IChatMessageReadRepository messageReadRepository,
            IChatMessageWriteRepository messageWriteRepository, IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _messageReadRepository = messageReadRepository;
            _messageWriteRepository = messageWriteRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> AddMessage(ChatMessageDto chatMessage)
        {
            if (chatMessage is null)
                return false;

            try
            {
                var mappedResult = _mapper.Map<ChatMessageDto, Domain.Entities.ChatMessage>(chatMessage);

                var response = await _messageWriteRepository.AddAsync(mappedResult);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ChatMessageDto>> GetAllChatMessages(Guid chatId, ClaimsPrincipal principal)
        {
            try
            {
                var user = await _userManager.GetUserAsync(principal);

                var messages = await _messageReadRepository.GetQueryable()
                                                           .Include(x => x.User)
                                                           .Where(x => x.Status && x.ChatId == chatId)
                                                           .OrderBy(x => x.CreatedDate)
                                                           .Select(x => new ChatMessageDto {
                                                               Text = x.Text,
                                                               User = x.User,
                                                               CreatedDate = x.CreatedDate,
                                                               SendDate = x.SendDate,
                                                               ChatId = chatId,
                                                               UserId = x.UserId,
                                                               IsOwnMessage = x.UserId == user.Id,
                                                               Id = x.Id
                                                           })
                                                           .ToListAsync();
                if (messages is null)
                    return [];

                //var mappedResult = _mapper.Map<List<Domain.Entities.ChatMessage>, List<ChatMessageDto>>(messages);

                return messages;
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}
