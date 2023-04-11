using Microsoft.AspNetCore.Identity;

namespace doan.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
