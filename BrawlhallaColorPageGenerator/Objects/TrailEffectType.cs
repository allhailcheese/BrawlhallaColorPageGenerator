using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

// this is the KO effects. the thing more commonly known as a trail effect is "Emitter"

public sealed class TrailEffectType
{
    public string TrailEffectName { get; }
    public string DisplayNameKey { get; }

    public TrailEffectType(XElement element)
    {
        TrailEffectName = element.Attribute(nameof(TrailEffectName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
    }
}

public sealed class TrailEffectTypes
{
    public TrailEffectType[] TrailEffects { get; }
    public Dictionary<string, TrailEffectType> TrailEffectsMap { get; }

    public TrailEffectTypes(string content)
    {
        XElement element = XElement.Parse(content);
        TrailEffects = [.. element.Elements(nameof(TrailEffectType)).Select((e) => new TrailEffectType(e))];
        TrailEffectsMap = TrailEffects.ToDictionary((t) => t.TrailEffectName);
    }
}