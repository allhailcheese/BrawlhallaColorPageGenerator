using System;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class GameModeType
{
    public string GameModeName { get; }
    public string DisplayNameKey { get; }
    public string? DescriptionKey { get; }
    public bool Teams { get; }
    public uint MaxPlayers { get; }
    public uint Duration { get; }
    public uint RoundDuration { get; }
    public uint StartingLives { get; }
    public string ScoringType { get; }
    public uint ScoreToWin { get; }
    public string? LevelSet { get; }
    public uint DamageRatio { get; }
    public bool GhostRule { get; }

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
        ScoreToWin = uint.TryParse(element.Element(nameof(ScoreToWin))?.Value, out uint sw) ? sw : 0;
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