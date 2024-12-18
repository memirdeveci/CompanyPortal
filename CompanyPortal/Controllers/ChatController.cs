using AutoMapper;
using CompanyPortal.Application.Abstractions.Chat;
using CompanyPortal.Application.Abstractions.Chat.Dtos;
using CompanyPortal.Application.Abstractions.User;
using CompanyPortal.Domain.Entities;
using CompanyPortal.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public ChatController(IUserService userService, IMapper mapper, IChatService chatService,
            IPhotoService photoService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _chatService = chatService;
            _photoService = photoService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateChat()
        {
            var users = await _userService.GetAllAppUsers();

            

            var chat = new ChatDto { Users = users };

            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(ChatDto chat)
        {
            if (chat is null || chat.UserIds is null)
                return RedirectToAction("CreateChat", new ChatDto());

            var currUser = await _userManager.GetUserAsync(User);

            var users = await _userService.GetAllAppUsersWithIds(chat.UserIds);

            users.Add(currUser);

            chat.Users = users;
            
            await PrepareContent(chat);

            await _chatService.AddChat(chat, User);

            return View(chat);
        }

        public async Task<IActionResult> ChatList()
        {
            var chats = await _chatService.GetUserChats(User);

            return View(chats);
        }

        private async Task PrepareContent(ChatDto chat)
        {
            if (chat.ChatPhotoFile != null)
            {
                var imageResult = await _photoService.AddPhotoAsync(chat.ChatPhotoFile);
                chat.ChatPhoto = imageResult.Url.ToString();
            }
        }
    }
}
