using Microsoft.AspNetCore.Identity;

namespace doan.Entities
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser user { set; get; }
        public AppRole role { set; get; }
    }
}
