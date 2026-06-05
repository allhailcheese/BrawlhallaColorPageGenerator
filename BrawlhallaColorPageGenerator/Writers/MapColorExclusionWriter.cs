using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public sealed class MapColorExclusionWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };
        writer.WriteLine("<includeonly><onlyinclude>{{#arraymap:{{#switch:{{lc:{{{1}}}}}");
        foreach (LevelType levelType in data.LevelTypes.Levels)
        {
            if (levelType.ColorExclusionList.Length == 0 || EXCLUDED_LEVEL_TYPES.Contains(levelType.LevelName))
                continue;

            writer.Write('|');
            string levelDisplayName = levelType.LevelName switch
            {
                "Zombie" => "walker attack",
                "Ring" => "brawldown ring",
                _ => levelType.DisplayName.ToLowerInvariant(),
            };
            writer.Write(levelDisplayName);
            writer.Write(" = ");
            bool first = true;
            foreach (string colorSchemeName in levelType.ColorExclusionList)
            {
                if (!first) writer.Write(',');

                ColorSchemeType colorScheme = data.ColorSchemeTypes.ColorSchemesMap[colorSchemeName];
                string displayName = colorScheme.DisplayNameKey;

                writer.Write(data.LangFile.Entries[displayName]);

                first = false;
            }
            writer.WriteLine();
        }
        writer.Write(
"""
}}|,|@|<span style="display:inline-block;margin-right:1.5em">{{Colors|@}}</span>|}}</onlyinclude></includeonly><noinclude>
* <code><nowiki>{{Map Color Exclusion/List|shipwreck falls}}</nowiki></code> = {{Map Color Exclusion/List|shipwreck falls}}
[[Category:Templates]]</noinclude>
""");
    }

    private static readonly HashSet<string> EXCLUDED_LEVEL_TYPES = [
        "Flipped90BP6GiantSword",
        "NTLostLabyrinth2",
        "TutorialBloodMoonFFA",
        "TutorialDashJump",
        "NTLostLabyrinth",
        "TutorialBP6GiantSword"
    ];
}