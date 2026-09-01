using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class CompanionType
{
    public string CompanionName { get; }
    public string DisplayNameKey { get; }

    public CompanionType(XElement element)
    {
        CompanionName = element.Attribute(nameof(CompanionName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
    }
}

public sealed class CompanionTypes
{
    public CompanionType[] Companions { get; }
    public Dictionary<string, CompanionType> CompanionsMap { get; }

    public CompanionTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Companions = [.. element.Elements(nameof(CompanionType)).Select((e) => new CompanionType(e))];
        CompanionsMap = Companions.ToDictionary((c) => c.CompanionName);
    }
}