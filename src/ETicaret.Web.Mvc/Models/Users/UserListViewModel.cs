using System.Collections.Generic;
using ETicaret.Roles.Dto;

namespace ETicaret.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
