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
            admin.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));
            admin.CreateChildPermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            admin.CreateChildPermission(PermissionNames.Pages_Roles, L("Roles"));
            admin.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var product = admin.CreateChildPermission(PermissionNames.Pages_Products, L("Products"));
            product.CreateChildPermission(PermissionNames.Pages_Product_Create, L("ProductCreate"));
            product.CreateChildPermission(PermissionNames.Pages_Product_Update, L("ProductUpdate"));
            product.CreateChildPermission(PermissionNames.Pages_Product_Delete, L("ProductDelete"));

            var category = admin.CreateChildPermission(PermissionNames.Pages_Category, L("Categories"));
            category.CreateChildPermission(PermissionNames.Pages_Category_Create, L("CategoryCreate"));
            category.CreateChildPermission(PermissionNames.Pages_Category_Update, L("CategoryUpdate"));
            category.CreateChildPermission(PermissionNames.Pages_Category_Delete, L("CategoryDelete"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ETicaretConsts.LocalizationSourceName);
        }
    }
}
