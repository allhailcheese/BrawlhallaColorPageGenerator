using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class ScoringType
{
    public string ScoringName { get; }
    public string DisplayNameKey { get; }

    public ScoringType(XElement element)
    {
        ScoringName = element.Attribute(nameof(ScoringName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
    }
}

public sealed class ScoringTypes
{
    public ScoringType[] Scorings { get; }
    public Dictionary<string, ScoringType> ScoringsMap { get; }

    public ScoringTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Scorings = [.. element.Elements(nameof(ScoringType)).Select((e) => new ScoringType(e))];
        ScoringsMap = Scorings.ToDictionary((s) => s.ScoringName);
    }
}