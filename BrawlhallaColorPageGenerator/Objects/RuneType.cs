using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class RuneType
{
    public string IconName { get; }
    public int Strength { get; }
    public int Dexterity { get; }
    public int Weight { get; }
    public int Speed { get; }

    public RuneType(XElement element)
    {
        IconName = element.Element(nameof(IconName))!.Value;
        Strength = int.Parse(element.Element(nameof(Strength))!.Value);
        Dexterity = int.Parse(element.Element(nameof(Dexterity))!.Value);
        Weight = int.Parse(element.Element(nameof(Weight))!.Value);
        Speed = int.Parse(element.Element(nameof(Speed))!.Value);
    }

    public StatEnum Stat => IconName switch
    {
        "a_StanceIcon_Strength" or "a_StanceIcon_SuperStrength" => StatEnum.Strength,
        "a_StanceIcon_Dexterity" or "a_StanceIcon_SuperDexterity" => StatEnum.Dexterity,
        "a_StanceIcon_Weight" or "a_StanceIcon_SuperWeight" => StatEnum.Defense,
        "a_StanceIcon_Speed" or "a_StanceIcon_SuperSpeed" => StatEnum.Speed,
        _ => throw new ArgumentException($"Rune type {IconName} does not represent a specific stat"),
    };

    public string? Name => IconName switch
    {
        "a_StanceIcon_Strength" => "Strength",
        "a_StanceIcon_SuperStrength" => "Super Strength",
        "a_StanceIcon_Dexterity" => "Dexterity",
        "a_StanceIcon_SuperDexterity" => "Super Dexterity",
        "a_StanceIcon_Weight" => "Defense",
        "a_StanceIcon_SuperWeight" => "Super Defense",
        "a_StanceIcon_Speed" => "Speed",
        "a_StanceIcon_SuperSpeed" => "Super Speed",
        "a_StanceIcon_Challenge" => "Challenge",
        "a_StanceIcon_Base" => null,
        _ => "ERROR",
    };

    public string? ShortName => IconName switch
    {
        "a_StanceIcon_Strength" => "str",
        "a_StanceIcon_SuperStrength" => $"super_str",
        "a_StanceIcon_Dexterity" => "dex",
        "a_StanceIcon_SuperDexterity" => $"super_dex",
        "a_StanceIcon_Weight" => "def",
        "a_StanceIcon_SuperWeight" => $"super_def",
        "a_StanceIcon_Speed" => "spd",
        "a_StanceIcon_SuperSpeed" => $"super_spd",
        "a_StanceIcon_Challenge" => "chal",
        "a_StanceIcon_Base" => null,
        _ => "ERROR",
    };

    public bool IsSuper => IconName.StartsWith("a_StanceIcon_Super");
    public bool IsBase => IconName == "a_StanceIcon_Base";
    public bool IsChallenge => IconName == "a_StanceIcon_Challenge";

    public string TakesFrom(int str, int dex, int def, int spd)
    {
        List<string> reduced = [];
        for (int i = 0; i < str - Strength; ++i) reduced.Add("str");
        for (int i = 0; i < dex - Dexterity; ++i) reduced.Add("dex");
        for (int i = 0; i < def - Weight; ++i) reduced.Add("def");
        for (int i = 0; i < spd - Speed; ++i) reduced.Add("spd");
        return string.Join(',', reduced);
    }
}

public sealed class RuneTypes
{
    public Dictionary<string, List<RuneType>> HeroRunes { get; } = [];

    public RuneTypes(string content)
    {
        string heroName = null!;
        XElement element = XElement.Parse(content);
        foreach (XElement rune in element.Elements())
        {
            string? newHeroName = rune.Element("HeroName")?.Value;
            if (newHeroName is not null) heroName = newHeroName;

            RuneType runeType = new(rune);

            HeroRunes.TryAdd(heroName, []);
            HeroRunes[heroName].Add(runeType);
        }
    }
}