using Microsoft.AspNetCore.Identity;

namespace SignalRIdentity.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
