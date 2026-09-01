using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class PodiumType
{
    public string PodiumName { get; }
    public string DisplayNameKey { get; }

    public PodiumType(XElement element)
    {
        PodiumName = element.Attribute(nameof(PodiumName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
    }
}

public sealed class PodiumTypes
{
    public PodiumType[] Podiums { get; }
    public Dictionary<string, PodiumType> PodiumsMap { get; }
    // TODO: levelups

    public PodiumTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Podiums = [.. element.Elements(nameof(PodiumType)).Select((e) => new PodiumType(e))];
        PodiumsMap = Podiums.ToDictionary((t) => t.PodiumName);
    }
}