namespace BrawlhallaColorPageGenerator;

public enum StatEnum
{
    Strength,
    Dexterity,
    Defense,
    Speed,
};

public static class StatEnumExtensions
{
    public static string GetShortName(this StatEnum stat) => stat switch
    {
        StatEnum.Strength => "str",
        StatEnum.Dexterity => "dex",
        StatEnum.Defense => "def",
        StatEnum.Speed => "spd",
        _ => "ERROR",
    };
}