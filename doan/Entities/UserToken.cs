using Microsoft.AspNetCore.Identity;

namespace doan.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public AppUser user { set; get; }
    }
}
