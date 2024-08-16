using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ETicaret.Authorization
{
    public class ETicaretAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var admin = context.CreatePermission(PermissionNames.Pages_Admin, L("Admin"));
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host); 

            var product = admin.CreateChildPermission(PermissionNames.Pages_Products);
            product.CreateChildPermission(PermissionNames.Pages_Product_Create, L("Create"));
            product.CreateChildPermission(PermissionNames.Pages_Product_Update, L("Update"));
            product.CreateChildPermission(PermissionNames.Pages_Product_Delete, L("Delete"));

            var category = admin.CreateChildPermission(PermissionNames.Pages_Category);
            category.CreateChildPermission(PermissionNames.Pages_Category_Create, L("Create"));
            category.CreateChildPermission(PermissionNames.Pages_Category_Update, L("Update"));
            category.CreateChildPermission(PermissionNames.Pages_Category_Delete, L("Delete"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ETicaretConsts.LocalizationSourceName);
        }
    }
}
