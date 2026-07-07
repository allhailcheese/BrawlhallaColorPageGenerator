using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public enum StatQuestType
{
    High, // 8+
    Mid, // 5 or 6
    Low, // 3-
}

public sealed class QuestListWriter(WriterData data)
{
    public void WriteTo(string path, StatEnum stat, StatQuestType level)
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
                    StatEnum.Strength => rune.Strength,
                    StatEnum.Dexterity => rune.Dexterity,
                    StatEnum.Defense => rune.Weight,
                    StatEnum.Speed => rune.Speed,
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
                    string? runeName = rune.Name;
                    if (runeName is not null)
                    {
                        writer.Write(" (");
                        writer.Write(runeName);
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