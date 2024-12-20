using CompanyPortal.Application.Abstractions.ChatMessage;
using CompanyPortal.Application.Abstractions.ChatMessage.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace CompanyPortal.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _messageService;
        public ChatHub(IChatMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessageAsync(string message, string groupName, string userId)
        {
            var chatMessage = new ChatMessageDto 
            {
                Text = message,
                UserId = Guid.Parse(userId),
                ChatId = Guid.Parse(groupName)
            };

            await _messageService.AddMessage(chatMessage);

            //await Clients.All.SendAsync("ReceiveMessage",message);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message, userId);
        }

        public override async Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;
            //await Clients.All.SendAsync("UserJoined", id);
            await Clients.Caller.SendAsync("GetConnectionId", id);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id = Context.ConnectionId;
            await Clients.All.SendAsync("UserLeft", id);
        }

        public async Task AddNewGroup(string connectionId, string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }
    }
}
