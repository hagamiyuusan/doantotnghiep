using Microsoft.AspNetCore.Authorization;

namespace doan.Services
{
    public class CanUseServiceRequirementcs : IAuthorizationRequirement
    {
        public bool AdminCanUse { set; get; }
        public bool UserCanUse { set; get; }
        public CanUseServiceRequirementcs(bool adminCanUse = true, bool userCanUse = true)
        {
            AdminCanUse = adminCanUse;
            UserCanUse = userCanUse;
        }



    }
}
