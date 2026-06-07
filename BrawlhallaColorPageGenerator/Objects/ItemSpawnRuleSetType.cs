using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class ItemSpawnRuleSetType
{
    public string RuleSetName { get; }
    public string[] WeaponList { get; }
    public string[] WeaponSpawnRateTypes { get; }
    public string[] GadgetList { get; }
    public string[] GadgetSpawnRateTypes { get; }

    public ItemSpawnRuleSetType(XElement element)
    {
        RuleSetName = element.Attribute(nameof(RuleSetName))!.Value;
        WeaponList = element.Element(nameof(WeaponList))?.Value.Split(',') ?? [];
        WeaponSpawnRateTypes = element.Element(nameof(WeaponSpawnRateTypes))?.Value.Split(',') ?? [];
        GadgetList = element.Element(nameof(GadgetList))?.Value.Split(',') ?? [];
        GadgetSpawnRateTypes = element.Element(nameof(GadgetSpawnRateTypes))?.Value.Split(',') ?? [];
    }
}

public sealed class ItemSpawnRuleSetTypes
{
    public ItemSpawnRuleSetType[] ItemSpawnRuleSets { get; }
    public Dictionary<string, ItemSpawnRuleSetType> ItemSpawnRuleSetsMap { get; }

    public ItemSpawnRuleSetTypes(string content)
    {
        XElement element = XElement.Parse(content);
        ItemSpawnRuleSets = [.. element.Elements(nameof(ItemSpawnRuleSetType)).Select((e) => new ItemSpawnRuleSetType(e))];
        ItemSpawnRuleSetsMap = ItemSpawnRuleSets.ToDictionary((r) => r.RuleSetName);
    }
}