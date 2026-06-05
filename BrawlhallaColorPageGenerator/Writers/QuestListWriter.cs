using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public enum StatType
{
    Strength,
    Dexterity,
    Defense,
    Speed,
};

public enum StatQuestType
{
    High, // 8+
    Mid, // 5 or 6
    Low, // 3-
}

public sealed class QuestListWriter(WriterData data)
{
    public void WriteTo(string path, StatType stat, StatQuestType level)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };
        foreach (HeroType hero in data.HeroTypes.Heroes)
        {
            if (!data.RuneTypes.HeroRunes.TryGetValue(hero.HeroName, out var runes) || hero.BioName is null)
                continue;

            foreach (RuneType rune in runes)
            {
                int statValue = stat switch
                {
                    StatType.Strength => rune.Strength,
                    StatType.Dexterity => rune.Dexterity,
                    StatType.Defense => rune.Weight,
                    StatType.Speed => rune.Speed,
                    _ => throw new System.IndexOutOfRangeException(),
                };

                bool runeWorks = level switch
                {
                    StatQuestType.High => statValue >= 8,
                    StatQuestType.Mid => statValue is 5 or 6,
                    StatQuestType.Low => statValue <= 3,
                    _ => false,
                };

                if (runeWorks)
                {
                    writer.Write("*[[");
                    writer.Write(hero.BioName);
                    writer.Write("]]");
                    if (rune.IconName != "a_StanceIcon_Base")
                    {
                        writer.Write(" (");
                        writer.Write(rune.IconName switch
                        {
                            "a_StanceIcon_Strength" => "Strength",
                            "a_StanceIcon_SuperStrength" => "Super Strength",
                            "a_StanceIcon_Dexterity" => "Dexterity",
                            "a_StanceIcon_SuperDexterity" => "Super Dexterity",
                            "a_StanceIcon_Weight" => "Defense",
                            "a_StanceIcon_SuperWeight" => "Super Defense",
                            "a_StanceIcon_Speed" => "Speed",
                            "a_StanceIcon_SuperSpeed" => "Super Speed",
                            "a_StanceIcon_Challenge" => "Challenge",
                            _ => "ERROR"
                        });
                        writer.Write(" stance)");
                    }
                    writer.WriteLine();
                    break;
                }
            }
        }
        writer.WriteLine("<noinclude>[[Category:Templates]]</noinclude>");
    }
}