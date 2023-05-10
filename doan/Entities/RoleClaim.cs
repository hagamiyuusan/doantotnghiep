using Microsoft.AspNetCore.Identity;

namespace doan.Entities
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public AppRole role { set; get; }
    }
}
