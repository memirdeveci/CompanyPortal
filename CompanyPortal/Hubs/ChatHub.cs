using Microsoft.AspNetCore.SignalR;

namespace CompanyPortal.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",message);
        }

        public override async Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;
            await Clients.All.SendAsync("UserJoined", id);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id = Context.ConnectionId;
            await Clients.All.SendAsync("UserLeft", id);
        }
    }
}
