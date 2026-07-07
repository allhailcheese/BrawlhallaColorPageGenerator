using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class StancesWriter(WriterData data)
{
    private static readonly int[] LEVELS_FOR_RUNES = [3, 4, 6, 8, 11, 13, 15, 17];

    private static readonly string[] RUNE_NAMES = [
        "str",
        "dex",
        "def",
        "spd",
        "super_str",
        "super_dex",
        "super_def",
        "super_spd",
    ];

    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };
        writer.WriteLine(
"""
<includeonly><onlyinclude>
{{#switch:{{lc:{{{1}}}}}
"""
        );
        foreach (HeroType hero in data.HeroTypes.Heroes.OrderBy((h) => h.ReleaseOrderID))
        {
            if (!data.RuneTypes.HeroRunes.TryGetValue(hero.HeroName, out var runes) || hero.BioName is null)
                continue;

            writer.Write('|');
            writer.Write(hero.BioName.ToLowerInvariant());
            if (hero.HeroName == "Viking")
                writer.Write("|bodvar");

            writer.Write(" = {{LegendStancesRow|{{#ifeq:{{{nohead|}}}|true||");
            writer.Write(hero.BioName);
            writer.Write("}}|str=");
            writer.Write(hero.Strength);
            writer.Write("|dex=");
            writer.Write(hero.Dexterity);
            writer.Write("|def=");
            writer.Write(hero.Weight);
            writer.Write("|spd=");
            writer.Write(hero.Speed);

            // what each rune takes from
            foreach (string runeName in RUNE_NAMES)
            {
                RuneType rune = runes.Find((r) => r.ShortName == runeName)!;
                string takesFrom = rune.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
                writer.Write('|');
                writer.Write(runeName);
                writer.Write("_take=");
                writer.Write(takesFrom);
            }

            writer.Write("|levels={{{levels|}}}");

            // the level to get each rune
            foreach (string runeName in RUNE_NAMES)
            {
                int runeIndex = runes.FindIndex((r) => r.ShortName == runeName);
                writer.Write('|');
                writer.Write(runeName);
                writer.Write("_level=");
                // base is at index 0, so the normal runes start at 1
                writer.Write(LEVELS_FOR_RUNES[runeIndex - 1]);
            }

            writer.WriteLine("}}");
        }
        writer.WriteLine(
"""
}}</onlyinclude></includeonly><noinclude>
{| class="wikitable" style="text-align:center;"
{{LegendStancesRowByName|Bodvar}}
{{LegendStancesRowByName|Xull}}
{{LegendStancesRowByName|Lady Vera}}
|}

{| class="wikitable" style="text-align:center;"
{{LegendStancesRowByName|Lady Vera|nohead=true}}
|}

{| class="wikitable" style="text-align:center;"
{{LegendStancesRowByName|Lady Vera|nohead=true|levels=true}}
|}

[[Category:Templates]]</noinclude>
"""
        );
    }
}