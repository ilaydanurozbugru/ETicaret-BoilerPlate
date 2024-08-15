using Abp.Authorization;
using ETicaret.Authorization.Roles;
using ETicaret.Authorization.Users;

namespace ETicaret.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
