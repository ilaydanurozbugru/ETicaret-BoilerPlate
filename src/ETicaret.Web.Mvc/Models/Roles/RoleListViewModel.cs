using System.Collections.Generic;
using ETicaret.Roles.Dto;

namespace ETicaret.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
