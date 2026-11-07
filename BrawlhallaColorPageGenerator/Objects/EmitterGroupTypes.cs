using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

// this is the the "trail effect"

public sealed class EmitterGroupType
{
    public string EmitterGroupName { get; }
    public string DisplayNameKey { get; }

    public EmitterGroupType(XElement element)
    {
        EmitterGroupName = element.Attribute(nameof(EmitterGroupName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
    }
}

public sealed class EmitterGroupTypes
{
    public EmitterGroupType[] EmitterGroups { get; }
    public Dictionary<string, EmitterGroupType> EmitterGroupsMap { get; }

    public EmitterGroupTypes(string content)
    {
        XElement element = XElement.Parse(content);
        EmitterGroups = [.. element.Elements(nameof(EmitterGroupType)).Select((e) => new EmitterGroupType(e))];
        EmitterGroupsMap = EmitterGroups.ToDictionary((t) => t.EmitterGroupName);
    }
}