using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class LevelSetType
{
    public string LevelSetName { get; }
    public string DisplayNameKey { get; }
    public string[] LevelTypes { get; }

    public LevelSetType(XElement element)
    {
        LevelSetName = element.Attribute(nameof(LevelSetName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
        LevelTypes = element.Element(nameof(LevelTypes))?.Value.Split(',') ?? [];
    }
}

public sealed class LevelSetTypes
{
    public LevelSetType[] LevelSets { get; }
    public Dictionary<string, LevelSetType> LevelSetsMap { get; }

    public LevelSetTypes(string content)
    {
        XElement element = XElement.Parse(content);
        LevelSets = [.. element.Elements(nameof(LevelSetType)).Select((e) => new LevelSetType(e))];
        LevelSetsMap = LevelSets.ToDictionary((ls) => ls.LevelSetName);
    }
}