using Microsoft.AspNetCore.Identity;

namespace doan.Entities
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public AppUser user { set; get; }
    }
}
