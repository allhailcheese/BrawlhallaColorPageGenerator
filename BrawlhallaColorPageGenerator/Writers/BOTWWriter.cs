using System;
using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class BOTWWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly>{{#switch:{{lc:{{{1|}}}}}");
        foreach (GameModeType gamemode in data.GameModeTypes.Gamemodes)
        {
            if (!gamemode.GameModeName.Contains("BOTW")) continue;

            string gamemodeName = data.LangFile.Entries[gamemode.DisplayNameKey];

            ScoringType scoringType = data.ScoringTypes.ScoringsMap[gamemode.ScoringType];
            string scoringTypeName = data.LangFile.Entries[scoringType.DisplayNameKey];

            // key
            writer.Write('|');
            writer.Write(gamemodeName.ToLowerInvariant().TrimEnd('!'));
            writer.WriteLine('=');
            // new row
            writer.WriteLine("{{!}}-");
            // title
            writer.Write("{{!}} '''");
            writer.Write(gamemodeName);
            writer.WriteLine("'''");
            // thumbnail
            writer.Write("{{!}} [[File:BOTW ");
            writer.Write(gamemodeName);
            writer.WriteLine(".jpg|200px]]");
            // description
            if (gamemode.DescriptionKey is not null)
            {
                string gamemodeDescription = data.LangFile.Entries[gamemode.DescriptionKey];
                writer.Write("{{!}}''");
                writer.Write(gamemodeDescription);
                writer.WriteLine("''");
            }
            // scoring type
            writer.Write("*{{gamemodes|");
            writer.Write(scoringTypeName.ToLowerInvariant());
            writer.WriteLine("|16px}}");
            // variation
            if (gamemode.Variation is not null)
            {
                writer.Write("*{{gamemodes|");
                writer.Write(gamemode.Variation switch
                {
                    "Relay" => "strikeout",
                    "Shift" => "morph",
                    "Scramble" => "switchcraft",
                    _ => "ERROR"
                });
                writer.WriteLine("|16px}}");
            }

            // players
            writer.Write('*');
            writer.Write(gamemode.MaxPlayers);
            writer.WriteLine(" players");

            // lives
            if (gamemode.StartingLives > 0)
            {
                writer.Write('*');
                writer.Write(gamemode.StartingLives);
                writer.WriteLine(" lives");
            }

            // score to win
            if (gamemode.ScoreToWin > 0)
            {
                writer.Write("*Score to win: ");
                writer.WriteLine(gamemode.ScoreToWin);
            }

            // time
            uint minutes = gamemode.Duration / 60;
            uint seconds = gamemode.Duration % 60;
            writer.Write('*');
            writer.Write(minutes);
            writer.Write(':');
            writer.Write("{0:D2}", seconds);
            writer.Write(" minutes");
            // round time
            if (gamemode.RoundDuration > 0)
            {
                uint roundMinutes = gamemode.RoundDuration / 60;
                uint roundSeconds = gamemode.RoundDuration % 60;
                writer.Write(", max ");
                writer.Write(roundMinutes);
                writer.Write(':');
                writer.Write("{0:D2}", roundSeconds);
                writer.Write(" minute rounds");
            }
            writer.WriteLine();

            // damage multiplier
            if (gamemode.DamageRatio != 100)
            {
                writer.Write('*');
                writer.Write(gamemode.DamageRatio);
                writer.WriteLine("% damage");
            }

            // teams
            if (gamemode.Teams && gamemode.ScoringType != "BUDDY")
            {
                writer.Write("*Teams enabled (");
                writer.Write(Math.Ceiling(gamemode.MaxPlayers / 2f));
                writer.Write('v');
                writer.Write(Math.Floor(gamemode.MaxPlayers / 2f));
                writer.WriteLine(')');
            }

            // 2 player vs bot gamemodes
            if (gamemode.GameModeName == "BOTW2v1DarthMaul")
            {
                writer.WriteLine("*2 Players vs A [[Darth Maul]] Chosen bot");
                writer.WriteLine("*Scoreboard header changed to \"JEDI WIN!\" or \"DARTH MAUL WINS!\"");
            }
            else if (gamemode.GameModeName == "BOTW2v1Mordex")
            {
                writer.WriteLine("*2 Players vs An [[Ascended Mordex]] Chosen bot");
                writer.WriteLine("*Scoreboard header changed to \"EXALTED HUNTERS WIN!\" or \"MORDEX WINS!\"");
            }

            WriteLevelSetText(writer, gamemode);

            // TODO: custom spawn rule sets
        }
        writer.Write(@"}}</includeonly><noinclude>
{{doc}}
{{BOTW/top}}
{{BOTW/bottom}}

[[Category:Templates]]</noinclude>");
    }

    public void WriteLevelSetText(StreamWriter writer, GameModeType gamemode)
    {
        string? levelSetName = gamemode.LevelSet;

        // map set is all maps for the gamemode
        if (levelSetName is null || levelSetName.EndsWith("All") || levelSetName == "VolleyBattle") return;

        writer.Write("*Map Set: ");

        // link to map set page
        if (LEVEL_SET_TO_MAP_SET_PAGE_ANCHOR.TryGetValue(levelSetName, out string? mapSetPageHeader))
        {
            writer.Write("[[Map_Set#");
            writer.Write(mapSetPageHeader);
            writer.Write("_Map_Set|");
            writer.Write(mapSetPageHeader);
            writer.WriteLine("]]");
        }
        // custom list ("new map" level sets)
        else if (levelSetName.Contains("NewMap"))
        {
            writer.WriteLine("TODO");
        }
        // just list out the levels
        else
        {
            int index = 0;
            LevelSetType levelSet = data.LevelSetTypes.LevelSetsMap[levelSetName];
            foreach (string levelName in levelSet.LevelTypes)
            {
                if (!data.LevelTypes.LevelsMap.TryGetValue(levelName, out LevelType? level))
                    continue;
                // comma
                if (index != 0)
                    writer.Write(", ");
                index++;
                // level link
                writer.Write("[[");
                writer.Write(level.DisplayName);
                writer.Write("]]");
            }
            writer.WriteLine();
        }
    }

    private static readonly Dictionary<string, string> LEVEL_SET_TO_MAP_SET_PAGE_ANCHOR = new()
    {
        ["Standard1v1"] = "1v1",
        ["Standard2v2"] = "2v2",
        ["Standard3v3"] = "3v3",
        ["StandardFFA"] = "FFA",
        ["StandardBig"] = "Big",
    };

    private static readonly Dictionary<string, string> GAMEMODE_TYPE_TO_MAP_LIST = new()
    {

    };
}