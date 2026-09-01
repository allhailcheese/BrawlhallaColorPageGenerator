using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class TauntType
{
    public string TauntName { get; }
    public string DisplayNameKey { get; }

    public TauntType(XElement element)
    {
        TauntName = element.Attribute(nameof(TauntName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
    }
}

public sealed class TauntTypes
{
    public TauntType[] Taunts { get; }
    public Dictionary<string, TauntType> TauntsMap { get; }

    public TauntTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Taunts = [.. element.Elements(nameof(TauntType)).Select((e) => new TauntType(e))];
        TauntsMap = Taunts.ToDictionary((t) => t.TauntName);
    }
}