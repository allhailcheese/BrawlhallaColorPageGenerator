using System;

namespace BrawlhallaColorPageGenerator;

public enum ItemTypeEnum
{
    Costume,
    WeaponSkin,
}

public static class ItemTypeEnumExtensions
{
    public static string GetName(this ItemTypeEnum itemType) => itemType switch
    {
        ItemTypeEnum.Costume => "Costume",
        ItemTypeEnum.WeaponSkin => "WeaponSkin",
        _ => throw new IndexOutOfRangeException(),
    };
}