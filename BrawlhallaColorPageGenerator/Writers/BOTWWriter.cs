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
            // TODO: template climb and showdown
            // TODO: morph, switchcraft, and strikeout
            writer.Write("{{!}} [[File:ModeThumb ");
            bool hasUniqueThumbnail = gamemode.GhostRule || GAMEMODES_WITH_UNIQUE_THUMBNAILS.Contains(gamemode.GameModeName);
            writer.Write(hasUniqueThumbnail ? gamemodeName : scoringTypeName);
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

            // TODO: 2 player vs bot gamemodes
            // teams
            if (gamemode.Teams && gamemode.ScoringType != "BUDDY")
            {
                writer.Write("*Teams enabled (");
                writer.Write(gamemode.MaxPlayers / 2);
                writer.Write('v');
                writer.Write(gamemode.MaxPlayers / 2);
                writer.WriteLine(')');
            }

            // TODO: map set / maps

            // TODO: custom spawn rule sets
        }
        writer.Write(@"}}</includeonly><noinclude>
{{doc}}
{{BOTW/top}}
{{BOTW/bottom}}

[[Category:Templates]]</noinclude>");
    }

    // Ghost rule gamemodes also do, but they're checked by themselves
    // There exist BOTW gamemodes with unique thumbnails that we are missing
    private static readonly HashSet<string> GAMEMODES_WITH_UNIQUE_THUMBNAILS = [
        "BOTWTableTop3v3NewMap", // Shiganshina Clash
        "BOTWHeatwaveFFA", // Water Bomb Bash
        "BOTWBotMatch2v1", // Brawl of the Fates
        "BOTW1v1NewMap", // Brawl of the Heroes
        "BOTW1v1Relay5", // Strikeout Mania!
        "BOTW1v1Relay5SW", // Scum & Villainy Strikeout
    ];
}