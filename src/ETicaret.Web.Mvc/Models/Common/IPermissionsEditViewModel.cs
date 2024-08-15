using System.Collections.Generic;
using ETicaret.Roles.Dto;

namespace ETicaret.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}