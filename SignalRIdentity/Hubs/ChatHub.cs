using Microsoft.AspNetCore.SignalR;
using SignalRIdentity.Data;
using SignalRIdentity.Models;

namespace SignalRIdentity.Hubs
{ 
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
                await Clients.All.SendAsync("receiveMessage", message);
    }
}
 