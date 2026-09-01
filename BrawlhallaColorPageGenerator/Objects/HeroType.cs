using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class HeroType
{
    public string HeroName { get; }
    public int ReleaseOrderID { get; }
    public string? BioName { get; }
    public string CostumeName { get; }
    public string[] ColorRewards { get; }
    public bool IsActive { get; }
    public string? BaseWeapon1 { get; }
    public string? BaseWeapon2 { get; }
    public int Strength { get; }
    public int Dexterity { get; }
    public int Weight { get; }
    public int Speed { get; }

    public string? BioNameKey => BioName?.ToLowerInvariant();

    public HeroType(XElement element)
    {
        HeroName = element.Attribute(nameof(HeroName))!.Value;
        ReleaseOrderID = int.Parse(element.Element(nameof(ReleaseOrderID))!.Value);
        BioName = element.Element(nameof(BioName))?.Value;
        CostumeName = element.Element(nameof(CostumeName))!.Value;
        ColorRewards = element.Element(nameof(ColorRewards))?.Value.Split(',') ?? [];
        IsActive = string.Equals(element.Element(nameof(IsActive))?.Value, "TRUE", System.StringComparison.OrdinalIgnoreCase);
        BaseWeapon1 = element.Element(nameof(BaseWeapon1))?.Value;
        BaseWeapon2 = element.Element(nameof(BaseWeapon2))?.Value;
        Strength = int.Parse(element.Element(nameof(Strength))?.Value ?? "0");
        Dexterity = int.Parse(element.Element(nameof(Dexterity))?.Value ?? "0");
        Weight = int.Parse(element.Element(nameof(Weight))?.Value ?? "0");
        Speed = int.Parse(element.Element(nameof(Speed))?.Value ?? "0");
    }
}

public sealed class HeroTypes
{
    public HeroType[] Heroes { get; }
    public Dictionary<string, HeroType> HeroesMap { get; }

    public HeroTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Heroes = [.. element.Elements(nameof(HeroType)).Select((e) => new HeroType(e))];
        HeroesMap = Heroes.ToDictionary((h) => h.HeroName);
    }
}