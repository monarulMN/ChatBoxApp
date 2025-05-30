using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using ChatBoxApp.Models.Entities;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SendMessage(string receiverId, string message)
    {
        var sender = await _userManager.GetUserAsync(Context.User);
        if (sender == null || string.IsNullOrWhiteSpace(message)) return;

        var chatMessage = new ChatMessage
        {
            SenderId = sender.Id,
            ReceiverId = receiverId,
            Message = message,
            Timestamp = DateTime.UtcNow
        };

        _context.ChatMessages.Add(chatMessage);
        await _context.SaveChangesAsync();

        await Clients.User(receiverId).SendAsync("ReceiveMessage", sender.Email, message);
        await Clients.Caller.SendAsync("ReceiveMessage", sender.Email, message);
    }
}