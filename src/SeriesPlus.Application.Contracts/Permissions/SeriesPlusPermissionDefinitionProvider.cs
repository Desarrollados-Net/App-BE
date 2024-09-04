using SeriesPlus.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace SeriesPlus.Permissions;

public class SeriesPlusPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SeriesPlusPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(SeriesPlusPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SeriesPlusResource>(name);
    }
}
