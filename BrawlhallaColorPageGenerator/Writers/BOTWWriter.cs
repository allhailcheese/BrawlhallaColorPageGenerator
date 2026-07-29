using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers;

public sealed class BOTWWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };
        writer.WriteLine("<includeonly>{{#switch:{{lc:{{{1|}}}}}");
        foreach (GameModeType gamemode in data.GameModeTypes.Gamemodes)
        {
            WriteGamemodeType(writer, gamemode);

            if (gamemode.GameModeName == "BOTWSnowbrawlNewMap")
            {
                WriteGamemodeType(writer, BOTWSnowbrawlNewMap_Old);
            }
        }
        writer.Write(@"}}</includeonly><noinclude>
{{doc}}
{{BOTW/top}}
{{BOTW/bottom}}

[[Category:Templates]]</noinclude>");
    }

    private void WriteGamemodeType(StreamWriter writer, GameModeType gamemode)
    {
        if (
            !gamemode.GameModeName.Contains("BOTW") ||
            // duplicate of BOTWVolleyBattle2v2NewMap but allows all maps, which doesn't make sense with its name and description
            gamemode.GameModeName == "BOTWVolleyBattle2v2"
        ) return;

        string gamemodeName = gamemode.GameModeName switch
        {
            // These are all named like another BOTW gamedmode, but are on a specific map
            "BOTW2v2CrewBattleTMNT" => "TMNT Crew Battle",
            "BOTWFixedStaminaGamemodeNewMap" => "Bustling Side Street Street Brawl",
            "BOTWHeatwaveSnowbrawlLavaFFA" => "Mustafar Water Balloon Fight!",
            // Fake gamemode types to keep older ones
            "BOTWSnowbrawlNewMap_Old" => gamemode.DisplayNameKey,
            // Real
            _ => data.LangFile.Entries[gamemode.DisplayNameKey],
        };

        ScoringType scoringType = data.ScoringTypes.ScoringsMap[gamemode.ScoringType];
        string scoringTypeName = data.LangFile.Entries[scoringType.DisplayNameKey];

        // key
        string gamemodeNameKey = gamemodeName.ToLowerInvariant().TrimEnd('!').Replace('’', '\'');
        writer.Write('|');
        writer.Write(gamemodeNameKey);
        writer.WriteLine('=');
        // new row
        writer.WriteLine("{{!}}-");
        // title
        writer.Write("{{!}} '''");
        writer.Write(gamemodeName);
        writer.WriteLine("'''");
        // thumbnail
        writer.Write("{{!}} [[File:BOTW ");
        string thumbnailName = gamemodeName.Replace('’', '\'');
        writer.Write(thumbnailName);
        writer.WriteLine(".jpg|200px]]");
        // description
        if (gamemode.DescriptionKey is not null)
        {
            string gamemodeDescription = gamemode.GameModeName switch
            {
                // Fake gamemode types to keep older ones
                "BOTWSnowbrawlNewMap_Old" => gamemode.DescriptionKey,
                // Real
                _ => data.LangFile.Entries[gamemode.DescriptionKey],
            };
            writer.Write("{{!}}''");
            writer.Write(gamemodeDescription);
            writer.WriteLine("''");
        }
        // scoring type
        writer.Write("*{{gamemodes|");
        writer.Write(scoringTypeName.ToLowerInvariant());
        writer.Write("|16px}}");
        // variation
        if (gamemode.Variation is not null)
        {
            writer.Write(" {{gamemodes|");
            writer.Write(gamemode.Variation switch
            {
                "Relay" => "strikeout",
                "Shift" => "morph",
                "Scramble" => "switchcraft",
                _ => "ERROR"
            });
            writer.Write("|16px}}");
        }
        writer.WriteLine();

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
        if (gamemode.Teams && gamemode.ScoringType != "BUDDY" && gamemode.MaxPlayers > 2)
        {
            // with an unbalanced team, give first team more players to handle 2v1
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

        WriteItemSpawnRuleSetText(writer, gamemode);
    }

    private void WriteLevelSetText(StreamWriter writer, GameModeType gamemode)
    {
        string? levelSetName = gamemode.LevelSet;

        // map set is all maps for the gamemode
        if (levelSetName is null || levelSetName.EndsWith("All") || levelSetName == "VolleyBattle")
            return;

        // link to map set page
        if (LEVEL_SET_TO_MAP_SET_PAGE_ANCHOR.TryGetValue(levelSetName, out string? mapSetPageHeader))
        {
            writer.Write("*Map Set: [[Map_Set#");
            writer.Write(mapSetPageHeader);
            writer.Write("_Map_Set|");
            writer.Write(mapSetPageHeader);
            writer.WriteLine("]]");
        }
        // just list out the levels
        else
        {
            string[]? levelList = GAMEMODE_LEVEL_LIST_OVERRIDE.GetValueOrDefault(gamemode.GameModeName);
            if (levelList is null)
            {
                // make sure we get all of them
                if (levelSetName.Contains("New"))
                {
                    writer.WriteLine("AN OVERRIDE NEEDS TO BE ADDED FOR THIS");
                    return;
                }

                LevelSetType levelSet = data.LevelSetTypes.LevelSetsMap[levelSetName];
                levelList = levelSet.LevelTypes;
            }

            writer.Write("*Map");
            if (levelList.Length > 1)
                writer.Write(" Set");
            writer.Write(": ");
            if (levelList.Length > 1)
                writer.WriteLine();

            foreach (string levelName in levelList)
            {
                if (!data.LevelTypes.LevelsMap.TryGetValue(levelName, out LevelType? level))
                    continue;

                if (levelList.Length > 1)
                    writer.Write("**");

                writer.Write("{{MapListing|");
                writer.Write(level.DisplayName.ToLowerInvariant());
                writer.WriteLine("}}");
            }
        }
    }

    private static readonly Dictionary<string, string> LEVEL_SET_TO_MAP_SET_PAGE_ANCHOR = new()
    {
        ["Standard1v1"] = "1v1",
        ["Standard2v2"] = "2v2",
        ["Standard3v3"] = "3v3",
        ["StandardFFA"] = "FFA",
        ["StandardBig"] = "Big",
        ["Ranked1v1"] = "Ranked 1v1",
        ["Ranked2v2"] = "Ranked 2v2",
        ["Ranked3v3"] = "Ranked 3v3",
        ["Experimental1v1"] = "Experimental 1v1",
        ["Unranked2v2"] = "Friendly 2v2",
        ["Crazy"] = "Mayhem",
        ["TableTop1v1"] = "Dice & Destruction 1v1",
        ["TableTop2v2"] = "Dice & Destruction 2v2",
        ["TableTop3v3"] = "Dice & Destruction 3v3",
        ["TableTopFFA"] = "Dice & Destruction FFA",
        ["TableTopBig"] = "Dice & Destruction Big",
        ["SnowbrawlNewSmall"] = "Gadget Mayhem Small",
        ["SnowbrawlNewBig"] = "Gadget Mayhem Big",
    };

    // For gamemodes that use a level set that is later changed (like "NewMap1v1")
    private static readonly Dictionary<string, string[]> GAMEMODE_LEVEL_LIST_OVERRIDE = new()
    {
        ["BOTW1v1Switch"] = ["BP9EndTimesTiny"], // Dangerous Duel
        ["BOTW3v3NewMap"] = ["RefineryDoors"], // Theed City Skirmish
        ["BOTWTableTop1v1"] = ["Lavabrawl3"], // Dwarven Duel
        ["BOTWBombMania"] = ["BP8ThreePlatformFFABig"], // Terminus-plosions!
        ["BOTWShift1v1NewMap"] = ["TriPlatBattle"], // Mishima Dojo Skirmish
        ["BOTWVolleyBattle2v2"] = ["VolleyBattleSmall"], // TEKKEN Brawl
        ["BOTWVolleyBattle2v2NewMap"] = ["VolleyBattleSmall"], // TEKKEN Brawl
        ["BOTWTableTop2v2NewMap"] = ["Norse1v1Spike"], // Jötunheimr's Doom
        ["BOTWTagRelay2v2NewMap"] = ["SpongebobMap"], // Bubble Tag Relay
        ["BOTW1v1NewMap"] = ["Mustafar"], // Brawl of the Heroes
        ["BOTWBounty6Bombs200"] = ["BP9EndTimesBig"], // Apocalyptic Target
        ["BOTW2v2NewMap"] = ["RefineryDoors"], // Rule of 2v2
        ["BOTWSnowbrawlNewMap_Old"] = ["ThreeShips"], // Starlight Snowbrawl Scuffle
        ["BOTWSnowbrawlNewMap"] = ["MudBrawl2"], // Muddy Gadget Mayhem
        ["BOTWTableTopFFA6NewMap"] = ["ThreeShips"], // Starlight Selection Trials
        ["BOTWThreeForAllRelayNewMap"] = ["SmallMovingPlatform"], // Lichlord's Relay
        ["BOTW2v2KungFootNewMap"] = ["NorseSoccer"], // Jötunn Winter Kung Foot
        ["BOTW1v1GhostNewMap"] = ["GroveSinglePlat"], // Jikoku Ghost Duel
        ["BOTW2v2Ghost200NewMap"] = ["Atlas_2v2"], // Hidden in the Walls
        ["BOTWTableTop3v3NewMap"] = ["Atlas_3v3"], // Shiganshina Clash
        ["BOTW4FFANewMap"] = ["MudBrawl2"], // Swamp Mud Brawl
    };

    private void WriteItemSpawnRuleSetText(StreamWriter writer, GameModeType gamemode)
    {
        // Morph will always have NoItems
        if (gamemode.Variation == "Shift")
            return;

        string? itemSpawnRuleSetName = gamemode.OverrideItemSpawnRuleSet;
        if (itemSpawnRuleSetName is null)
            return;

        // check if given item spawn rule set is the default
        ScoringType scoringType = data.ScoringTypes.ScoringsMap[gamemode.ScoringType];
        ItemSpawnRuleSetType defaultRuleSet = data.ItemSpawnRuleSetTypes.ItemSpawnRuleSetsMap[scoringType.ItemSpawnRuleSet];
        ItemSpawnRuleSetType itemSpawnRuleSet = data.ItemSpawnRuleSetTypes.ItemSpawnRuleSetsMap[itemSpawnRuleSetName];

        // write weapon and gadget spawn rate and spawn list. each will only be written if different from default.
        string spawnText = gamemode.ScoringType == "SNOWBALL" ? "appear" : "spawn";
        WriteItemSpawnRate(writer, itemSpawnRuleSet.WeaponSpawnRateTypes, defaultRuleSet.WeaponSpawnRateTypes, "Weapon");
        WriteItemListText(writer, itemSpawnRuleSet.WeaponList, defaultRuleSet.WeaponList, spawnText, "Weapons");
        WriteItemSpawnRate(writer, itemSpawnRuleSet.GadgetSpawnRateTypes, defaultRuleSet.GadgetSpawnRateTypes, "Gadget");
        WriteItemListText(writer, itemSpawnRuleSet.GadgetList, defaultRuleSet.GadgetList, spawnText, "Gadgets");
    }

    private void WriteItemSpawnRate(StreamWriter writer, string[] itemSpawnRates, string[] defaultItemSpawnRates, string itemTypeText)
    {
        string? itemSpawnRate = itemSpawnRates.ElementAtOrDefault(0);
        string? defaultItemSpawnRate = defaultItemSpawnRates.ElementAtOrDefault(0);
        if (itemSpawnRate is null || itemSpawnRate == defaultItemSpawnRate || itemSpawnRate == "WaterBombGadgets")
            return;

        writer.Write('*');
        writer.Write(itemTypeText);
        writer.Write(" spawns set to ");
        if (itemSpawnRate.EndsWith("Low"))
            writer.WriteLine("Low");
        else if (itemSpawnRate.EndsWith("Medium"))
            writer.WriteLine("Medium");
        else if (itemSpawnRate.EndsWith("High"))
            writer.WriteLine("High");
        else
            writer.WriteLine("UNKNOWN");
    }

    private void WriteItemListText(StreamWriter writer, string[] itemList, string[] defaultItemList, string spawnText, string itemTypeText)
    {
        if (Enumerable.SequenceEqual(itemList, defaultItemList))
            return;

        if (itemList.Length == 0)
        {
            writer.Write("*[[");
            writer.Write(itemTypeText);
            writer.Write("]] cannot ");
            writer.WriteLine(spawnText);
            return;
        }

        writer.Write("*Only ");
        if (itemList.Length > 1)
        {
            writer.Write("the following [[");
            writer.Write(itemTypeText);
            writer.Write("]] can ");
            writer.Write(spawnText);
            writer.WriteLine(':');
        }

        foreach (string itemName in itemList)
        {
            ItemType item = data.ItemTypes.ItemsMap[itemName];

            if (itemList.Length > 1) writer.Write("**");
            writer.Write("{{items|");
            writer.Write(item.ItemName switch
            {
                "WaterBomb" => "water bomb",
                _ => data.LangFile.Entries[item.DisplayNameKey!].ToLowerInvariant(),
            });
            writer.Write("}}");
            if (itemList.Length > 1) writer.WriteLine();
        }

        if (itemList.Length == 1)
        {
            writer.Write(" can ");
            writer.WriteLine(spawnText);
        }
    }

    // BOTWSnowbrawlNewMap was replaced, this is the original one
    private static readonly GameModeType BOTWSnowbrawlNewMap_Old = new()
    {
        GameModeName = "BOTWSnowbrawlNewMap_Old",
        DisplayNameKey = "Starlight Snowbrawl Scuffle",
        DescriptionKey = "Cool off with your fellow Starlight Champions in this 4 player, 3 minute free-for-all! Score 1 point for hitting someone with a snowball, 3 points for getting a KO, and lose 1 point for being KO'd. Most points at the end wins!",
        ScoringType = "SNOWBALL",
        LevelSet = "",
        MaxPlayers = 4,
        Duration = 180,
        DamageRatio = 100,
    };
}