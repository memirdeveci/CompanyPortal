using Microsoft.AspNetCore.SignalR;

namespace CompanyPortal.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(string message, string groupName)
        {
            //await Clients.All.SendAsync("ReceiveMessage",message);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
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
