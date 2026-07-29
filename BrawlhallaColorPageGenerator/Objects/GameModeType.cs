using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class GameModeType
{
    public required string GameModeName { get; init; }
    public required string DisplayNameKey { get; init; }
    public string? DescriptionKey { get; init; }
    public bool Teams { get; init; }
    public uint MaxPlayers { get; init; }
    public uint Duration { get; init; }
    public uint RoundDuration { get; init; }
    public uint StartingLives { get; init; }
    public required string ScoringType { get; init; }
    public string? Variation { get; init; }
    public uint ScoreToWin { get; init; }
    public string? OverrideItemSpawnRuleSet { get; init; }
    public string? LevelSet { get; init; }
    public uint DamageRatio { get; init; }
    public bool GhostRule { get; init; }

    public GameModeType() { }

    [SetsRequiredMembers]
    public GameModeType(XElement element)
    {
        GameModeName = element.Attribute(nameof(GameModeName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
        DescriptionKey = element.Element(nameof(DescriptionKey))?.Value;
        Teams = string.Equals("TRUE", element.Element(nameof(Teams))?.Value, StringComparison.OrdinalIgnoreCase);
        MaxPlayers = uint.Parse(element.Element(nameof(MaxPlayers))!.Value);
        Duration = uint.Parse(element.Element(nameof(Duration))!.Value);
        RoundDuration = uint.TryParse(element.Element(nameof(RoundDuration))?.Value, out uint rd) ? rd : 0;
        StartingLives = uint.TryParse(element.Element(nameof(StartingLives))?.Value, out uint sl) ? sl : 0;
        ScoringType = element.Element(nameof(ScoringType))!.Value;
        Variation = element.Element(nameof(Variation))?.Value;
        ScoreToWin = uint.TryParse(element.Element(nameof(ScoreToWin))?.Value, out uint sw) ? sw : 0;
        OverrideItemSpawnRuleSet = element.Element(nameof(OverrideItemSpawnRuleSet))?.Value;
        LevelSet = element.Element(nameof(LevelSet))?.Value;
        DamageRatio = uint.TryParse(element.Element(nameof(DamageRatio))?.Value, out uint dr) ? dr : 100;
        GhostRule = string.Equals("TRUE", element.Element(nameof(GhostRule))?.Value, StringComparison.OrdinalIgnoreCase);
    }
}

public sealed class GameModeTypes
{
    public GameModeType[] Gamemodes { get; }

    public GameModeTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Gamemodes = [.. element.Elements(nameof(GameModeType)).Select((e) => new GameModeType(e))];
    }
}