using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class LevelSetType
{
    public required string LevelSetName { get; init; }
    public required string DisplayNameKey { get; init; }
    public required string[] LevelTypes { get; init; }

    public LevelSetType() { }

    [SetsRequiredMembers]
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