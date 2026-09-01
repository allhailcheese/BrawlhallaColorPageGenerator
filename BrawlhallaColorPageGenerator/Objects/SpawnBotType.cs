using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class SpawnBotType
{
    public string SpawnBotName { get; }
    public string DisplayNameKey { get; }

    public SpawnBotType(XElement element)
    {
        SpawnBotName = element.Attribute(nameof(SpawnBotName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
    }
}

public sealed class SpawnBotTypes
{
    public SpawnBotType[] SpawnBots { get; }
    public Dictionary<string, SpawnBotType> SpawnBotsMap { get; }

    public SpawnBotTypes(string content)
    {
        XElement element = XElement.Parse(content);
        SpawnBots = [.. element.Elements(nameof(SpawnBotType)).Select((e) => new SpawnBotType(e))];
        SpawnBotsMap = SpawnBots.ToDictionary((t) => t.SpawnBotName);
    }
}