using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class StancesWriter(WriterData data)
{
    private static readonly int[] LEVELS_FOR_INDEX = [3, 4, 6, 8];
    private static readonly int[] LEVELS_FOR_INDEX_SUPER = [11, 13, 15, 17];

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
            List<RuneType> normalRunes = [.. runes.Where((r) => !r.IsSuper && !r.IsBase && !r.IsChallenge)];
            List<RuneType> superRunes = [.. runes.Where((r) => r.IsSuper && !r.IsBase && !r.IsChallenge)];

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

            int strStanceIndex = normalRunes.FindIndex((r) => r.ShortName == "str" && !r.IsSuper);
            RuneType strStance = normalRunes[strStanceIndex];
            string strTakeFrom = strStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|str_take=");
            writer.Write(strTakeFrom);

            int dexStanceIndex = normalRunes.FindIndex((r) => r.ShortName == "dex" && !r.IsSuper);
            RuneType dexStance = normalRunes[dexStanceIndex];
            string dexTakeFrom = dexStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|dex_take=");
            writer.Write(dexTakeFrom);

            int defStanceIndex = normalRunes.FindIndex((r) => r.ShortName == "def" && !r.IsSuper);
            RuneType defStance = normalRunes[defStanceIndex];
            string defTakeFrom = defStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|def_take=");
            writer.Write(defTakeFrom);

            int spdStanceIndex = normalRunes.FindIndex((r) => r.ShortName == "spd" && !r.IsSuper);
            RuneType spdStance = normalRunes[spdStanceIndex];
            string spdTakeFrom = spdStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|spd_take=");
            writer.Write(spdTakeFrom);

            int superStrStanceIndex = superRunes.FindIndex((r) => r.ShortName == "str" && r.IsSuper);
            RuneType superStrStance = superRunes[superStrStanceIndex];
            string superStrTakeFrom = superStrStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|super_str_take=");
            writer.Write(superStrTakeFrom);

            int superDexStanceIndex = superRunes.FindIndex((r) => r.ShortName == "dex" && r.IsSuper);
            RuneType superDexStance = superRunes[superDexStanceIndex];
            string superDexTakeFrom = superDexStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|super_dex_take=");
            writer.Write(superDexTakeFrom);

            int superDefStanceIndex = superRunes.FindIndex((r) => r.ShortName == "def" && r.IsSuper);
            RuneType superDefStance = superRunes[superDefStanceIndex];
            string superDefTakeFrom = superDefStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|super_def_take=");
            writer.Write(superDefTakeFrom);

            int superSpdStanceIndex = superRunes.FindIndex((r) => r.ShortName == "spd" && r.IsSuper);
            RuneType superSpdStance = superRunes[superSpdStanceIndex];
            string superSpdTakeFrom = superSpdStance.TakesFrom(hero.Strength, hero.Dexterity, hero.Weight, hero.Speed);
            writer.Write("|super_spd_take=");
            writer.Write(superSpdTakeFrom);

            writer.Write("|levels={{{levels|}}}|str_level=");
            writer.Write(LEVELS_FOR_INDEX[strStanceIndex]);
            writer.Write("|dex_level=");
            writer.Write(LEVELS_FOR_INDEX[dexStanceIndex]);
            writer.Write("|def_level=");
            writer.Write(LEVELS_FOR_INDEX[defStanceIndex]);
            writer.Write("|spd_level=");
            writer.Write(LEVELS_FOR_INDEX[spdStanceIndex]);
            writer.Write("|super_str_level=");
            writer.Write(LEVELS_FOR_INDEX_SUPER[superStrStanceIndex]);
            writer.Write("|super_dex_level=");
            writer.Write(LEVELS_FOR_INDEX_SUPER[superDexStanceIndex]);
            writer.Write("|super_def_level=");
            writer.Write(LEVELS_FOR_INDEX_SUPER[superDefStanceIndex]);
            writer.Write("|super_spd_level=");
            writer.Write(LEVELS_FOR_INDEX_SUPER[superSpdStanceIndex]);

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