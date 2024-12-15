using AutoMapper;
using CompanyPortal.Application.Abstractions.Chat;
using CompanyPortal.Application.Abstractions.Chat.Dtos;
using CompanyPortal.Application.Abstractions.User;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;
        public ChatController(IUserService userService, IMapper mapper, IChatService chatService)
        {
            _userService = userService;
            _chatService = chatService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateChat()
        {
            var user = await _userService.GetAllAppUsers();

            var chat = new ChatDto { Users = user };

            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(ChatDto chat)
        {
            if (chat is null || chat.UserIds is null)
                return RedirectToAction("CreateChat");

            var users = await _userService.GetAllAppUsersWithIds(chat.UserIds);
            chat.Users = users;
            await _chatService.AddChat(chat, User);

            return View(chat);
        }
    }
}
