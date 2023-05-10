using Microsoft.AspNetCore.Identity;

namespace doan.Entities
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public AppUser user { set; get; }
    }
}
