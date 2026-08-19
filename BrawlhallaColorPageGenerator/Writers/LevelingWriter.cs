using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class LevelingWriter(WriterData data)
{
    private static readonly string[] LEVELING_COLORS = [
        "Blue",
        "Yellow",
        "Green",
        "Brown",
        "Orange",
        "Purple",
        "Cyan",
        "Sunset",
        "Grey",
        "Pink",
        "Red",
    ];

    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };
        writer.WriteLine("<includeonly><onlyinclude>\n{{#switch:{{lc:{{{1}}}}}");
        foreach (HeroType hero in data.HeroTypes.Heroes.OrderBy((h) => h.ReleaseOrderID))
        {
            if (!data.RuneTypes.HeroRunes.TryGetValue(hero.HeroName, out var runes) || hero.BioName is null)
                continue;

            writer.Write('|');
            writer.Write(hero.BioNameKey);
            if (hero.HeroName == "Viking")
                writer.Write("|bodvar");

            writer.Write(" = {{LegendLevelingRow|");
            writer.Write(hero.BioName);
            writer.Write("|nohead={{{nohead|}}}");

            int stanceNumber = 1;
            int superStanceNumber = 1;
            foreach (RuneType rune in runes)
            {
                if (rune.IsBase || rune.IsChallenge) continue;

                if (rune.IsSuper)
                {
                    writer.Write("|superstance");
                    writer.Write(superStanceNumber++);
                }
                else
                {
                    writer.Write("|stance");
                    writer.Write(stanceNumber++);
                }
                writer.Write('=');
                writer.Write(rune.Stat.GetShortName());
            }

            HashSet<string> colorRewardsSet = [.. hero.ColorRewards];
            string[] leftoverColors = [.. LEVELING_COLORS.Where((c) => !colorRewardsSet.Contains(c))];
            IEnumerable<string> colors = leftoverColors.Concat(hero.ColorRewards);

            int colorNumber = 1;
            foreach (string color in colors)
            {
                writer.Write("|color");
                writer.Write(colorNumber++);
                writer.Write('=');
                writer.Write(color.ToLower());
            }

            writer.WriteLine("|extra_labels={{{extra_labels|}}}|icon_size={{{icon_size|25px}}}}}");
        }
        writer.WriteLine(
"""
}}</onlyinclude></includeonly><noinclude>
{| class="wikitable" style="text-align:center;"
{{LegendLevelingRowByName|Bodvar}}
{{LegendLevelingRowByName|Lady Vera}}
|}

{| class="wikitable" style="text-align:center;"
{{LegendLevelingRowByName|Bodvar|extra_labels=true}}
|}
[[Category:Templates]]</noinclude>
"""
);
    }
}