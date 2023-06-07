using Microsoft.AspNetCore.SignalR;

namespace SignalRWebpack.Hubs;

public class ChatHub : Hub
{
    public async Task NewMessage(string sketchName, string username, string content)
    {
        await Clients.Group(sketchName).SendAsync("messageReceived", username, content);
    }

    public async Task AddToSketchChatGroup(string sketchName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sketchName);
    }
}