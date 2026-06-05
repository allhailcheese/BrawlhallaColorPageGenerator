using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class LevelType
{
    public string LevelName { get; }
    public bool DevOnly { get; }
    public string DisplayName { get; }
    public string[] ColorExclusionList { get; }

    public LevelType(XElement element)
    {
        LevelName = element.Attribute(nameof(LevelName))!.Value;
        DevOnly = string.Equals(element.Attribute(nameof(DevOnly))?.Value, "true", System.StringComparison.OrdinalIgnoreCase);
        DisplayName = element.Element(nameof(DisplayName))!.Value;
        ColorExclusionList = element.Element(nameof(ColorExclusionList))?.Value.Split(',') ?? [];
    }
}

public sealed class LevelTypes
{
    public LevelType[] Levels { get; }
    public Dictionary<string, LevelType> LevelsMap { get; }

    public LevelTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Levels = [.. element.Elements(nameof(LevelType)).Select((e) => new LevelType(e)).Where(l => !l.DevOnly)];
        LevelsMap = Levels.ToDictionary((l) => l.LevelName);
    }
}