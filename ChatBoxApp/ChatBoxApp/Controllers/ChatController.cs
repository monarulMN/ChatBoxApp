using ChatBoxApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatBoxApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Chat()
        {
            var currentUserId = _userManager.GetUserId(User);
            var users = _userManager.Users.Where(u => u.Id != currentUserId).ToList();
            return View("~/Views/Chat/Chat.cshtml", users);
        }
    }
}
