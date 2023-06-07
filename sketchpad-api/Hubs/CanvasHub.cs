using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;
using sketchpad_api.Database;
using sketchpad_api.Database.Models;



namespace SignalRWebpack.Hubs;

public class CanvasHub : Hub
{
    public async Task SendSketchCanvasDrawings(string sketchName, decimal mouseX, decimal mouseY, decimal pmouseX, decimal pmouseY)
    {
        await Clients.Group(sketchName).SendAsync("newSketchCanvasDrawings", mouseX, mouseY, pmouseX, pmouseY);

    }

    public async Task AddToSketchCanvasGroup(string sketchName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sketchName);
    }

}

