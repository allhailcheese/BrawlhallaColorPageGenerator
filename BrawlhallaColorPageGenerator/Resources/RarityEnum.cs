namespace BrawlhallaColorPageGenerator;

public enum RarityEnum
{
    None,
    Epic,
    Mythic,
}

public static class RarityEnumExtensions
{
    public static string GetName(this RarityEnum rarity) => rarity switch
    {
        RarityEnum.None => "",
        RarityEnum.Epic => "epic",
        RarityEnum.Mythic => "mythic",
        _ => "ERROR",
    };
}