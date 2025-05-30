using Microsoft.AspNetCore.Identity;

namespace ChatBoxApp.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
    }
}
