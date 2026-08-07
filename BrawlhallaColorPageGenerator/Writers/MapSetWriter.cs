using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public sealed class MapSetWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };

        writer.WriteLine(
"""
The '''Map Set''' is the pool of [[Maps]] players can select from in [[Custom Online]] and [[Couch Party]] lobbies. This can be configured in the settings menu in the lobby of these gamemodes.

By default, the map set is configured to "Auto", which will automatically select a map set based on how many players are in the lobby. If you do not want to play by a specific map set, the "All" map set will simply allow any map from the gamemode to be selected.
""");
        writer.WriteLine();

        writer.WriteLine("==Standard Map Sets==");
        writer.WriteLine();

        writer.WriteLine("===FFA Map Set===");
        writer.WriteLine("This map set is intended for 3 to 4 players. It is currently being used in [[Free-for-All]].");
        writer.WriteLine();
        WriteMapSet(writer, "StandardFFA");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===1v1 Map Set===");
        writer.WriteLine("This map set is intended for 2 players.");
        writer.WriteLine();
        WriteMapSet(writer, "Standard1v1");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===2v2 Map Set===");
        writer.WriteLine("This map set is intended for 4 players with teams enabled.");
        writer.WriteLine();
        WriteMapSet(writer, "Standard2v2");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===3v3 Map Set===");
        writer.WriteLine("This map set is intended for 6 players with teams enabled, however it does not get used if the \"Auto\" map set is enabled.");
        writer.WriteLine();
        WriteMapSet(writer, "Standard3v3");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Big Map Set===");
        writer.WriteLine("This map set is intended for matches with 5 or more players. It is used if the \"Auto\" map set is enabled regardless if teams are also enabled.");
        writer.WriteLine();
        WriteMapSet(writer, "StandardBig");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Mayhem Map Set===");
        writer.WriteLine("This map set is intended for any number of players, and contains maps that are more chaotic.");
        writer.WriteLine();
        WriteMapSet(writer, "Crazy");
        writer.WriteLine();

        writer.WriteLine("==Casual Queue Map Sets==");
        writer.WriteLine();

        writer.WriteLine("===Experimental 1v1 Map Set===");
        writer.WriteLine("This map set is intended for 2 players. It is used in [[Experimental 1v1]] when it is available.");
        writer.WriteLine();
        WriteMapSet(writer, "Experimental1v1");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Friendly 2v2 Map Set===");
        writer.WriteLine("This map set is used in [[Friendly 2v2]].");
        writer.WriteLine();
        WriteMapSet(writer, "Unranked2v2");
        writer.WriteLine();

        writer.WriteLine("==Ranked Map Sets==");
        writer.WriteLine();

        writer.WriteLine("===Ranked 1v1 Map Set===");
        writer.WriteLine("This map set is used in [[Ranked 1v1]] matches, in [[Unranked 1v1]], and in [[Strikeout 1v1]].");
        writer.WriteLine();
        WriteMapSet(writer, "Ranked1v1");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Ranked 2v2 Map Set===");
        writer.WriteLine("This map set is used in [[Ranked 2v2]] matches.");
        writer.WriteLine();
        WriteMapSet(writer, "Ranked2v2");
        writer.WriteLine();

        writer.WriteLine("==Tournament Map Sets==");
        writer.WriteLine();

        writer.WriteLine("===Tournament 1v1 Map Set===");
        writer.WriteLine("This map set is used in 1v1 matches for official tournaments.");
        writer.WriteLine();
        WriteMapSet(writer, "Tournament1v1");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Tournament 2v2 Map Set===");
        writer.WriteLine("This map set is used in 2v2 matches for official tournaments.");
        writer.WriteLine();
        WriteMapSet(writer, "Tournament2v2");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Tournament 3v3 Map Set===");
        writer.WriteLine("This map set is intended to be used in 3v3 matches for tournaments.");
        writer.WriteLine();
        WriteMapSet(writer, "Tournament3v3");
        writer.WriteLine();

        writer.WriteLine("==Gamemode Specific==");
        writer.WriteLine("Non-standard gamemodes typically have different map sets than regular. Some of these gamemodes can only be played on custom maps, and thus do not have any selection for map sets. Other gamemodes, however, can be played on regular maps, and have unique map sets.");
        writer.WriteLine();

        writer.WriteLine("===Dice & Destruction===");
        writer.WriteLine("[[Dice & Destruction]] features 6 unique map sets, alongside using the standard Tournament 1v1 and Tournament 2v2 sets. These map sets are similar to their standard counterparts, typically only removing maps with ceilings or high item spawns from the pool.");
        writer.WriteLine();

        writer.WriteLine("====Dice & Destruction All Map Set====");
        writer.WriteLine("This map set is used in place of the typical \"All\" map set, removing maps that may not work well with the gamemode.");
        writer.WriteLine();
        WriteMapSet(writer, "TableTopALL");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Dice & Destruction FFA Map Set====");
        writer.WriteLine("This map set is used for lobbies of 3-4 players with teams disabled.");
        writer.WriteLine();
        WriteMapSet(writer, "TableTopFFA");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Dice & Destruction 1v1 Map Set====");
        writer.WriteLine("This map set is used for lobbies of 2 players.");
        writer.WriteLine();
        WriteMapSet(writer, "TableTop1v1");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Dice & Destruction 2v2 Map Set====");
        writer.WriteLine("This map set is used for lobbies of 3-4 players with teams enabled.");
        writer.WriteLine();
        WriteMapSet(writer, "TableTop2v2");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Dice & Destruction 3v3 Map Set====");
        writer.WriteLine("This map set is used for lobbies of 5-6 players with teams enabled.");
        writer.WriteLine();
        WriteMapSet(writer, "TableTop3v3");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Dice & Destruction Big Map Set====");
        writer.WriteLine("This map set is used for lobbies of 5-8 players with teams disabled, or 7-8 players with teams enabled.");
        writer.WriteLine();
        WriteMapSet(writer, "TableTopBig");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Gadget Mayhem===");
        writer.WriteLine("[[Gadget Mayhem]] features 2 unique maps sets: \"small\", for 1-4 player matches, and \"big\", for 5-8 player matches.");
        writer.WriteLine();

        writer.WriteLine("====Gadget Mayhem Small Map Set====");
        writer.WriteLine("This map set is used for lobbies with 1-4 players.");
        writer.WriteLine();
        WriteMapSet(writer, "SnowbrawlNewSmall");
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Gadget Mayhem Big Map Set====");
        writer.WriteLine("This map set is used for lobbies with 5-8 players.");
        writer.WriteLine();
        WriteMapSet(writer, "SnowbrawlNewBig");
        writer.WriteLine();

        writer.WriteLine("==Removed Map Sets==");
        writer.WriteLine("Certain map sets previously existed in the game, and were later removed.");
        writer.WriteLine();

        writer.WriteLine("===Bubble Tag===");
        writer.WriteLine("[[Bubble Tag]] previously featured three unique map sets: '''All''', '''2v2''', and '''Big'''.");
        writer.WriteLine();
        writer.WriteLine("These were removed in [[Patch 8.14]]. Bubble Tag now uses standard map sets.");
        writer.WriteLine();

        writer.WriteLine("====Bubble Tag All Map Set====");
        writer.WriteLine();
        WriteMapSet(writer, BUBBLE_TAG_ALL);
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Bubble Tag 2v2 Map Set====");
        writer.WriteLine("This map set was used for lobbies of 4 players or less.");
        writer.WriteLine();
        WriteMapSet(writer, BUBBLE_TAG_2v2);
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("====Bubble Tag Big Map Set====");
        writer.WriteLine("This map set was used for lobbies of 5 players or more.");
        writer.WriteLine();
        WriteMapSet(writer, BUBBLE_TAG_BIG);
        writer.WriteLine();
        writer.WriteLine();

        writer.WriteLine("===Snowbrawl FFA Map Set===");
        writer.WriteLine("[[Snowbrawl]] previously featured a unique '''FFA''' map set, alongside a '''Big''' map set which was identical to the regular Big map set.");
        writer.WriteLine();
        writer.WriteLine("These were removed in [[Patch 9.01]], replaced with standard map sets.");
        writer.WriteLine();
        writer.WriteLine("Despite its removal, this map set still remained in the files and was updated for a few patches. It was finally removed in [[Patch 9.06]]");
        writer.WriteLine();
        WriteMapSet(writer, SNOWBALL_FFA);

        writer.WriteLine();
        writer.WriteLine("[[Category:Maps]]");
    }

    private void WriteMapSet(StreamWriter writer, string levelSetName)
    {
        LevelSetType levelSet = data.LevelSetTypes.LevelSetsMap[levelSetName];
        WriteMapSet(writer, levelSet);
    }

    private void WriteMapSet(StreamWriter writer, LevelSetType levelSet)
    {
        writer.WriteLine("{{MapListing/top}}");
        foreach (string levelTypeName in levelSet.LevelTypes)
        {
            LevelType? levelType = data.LevelTypes.LevelsMap.GetValueOrDefault(levelTypeName);
            if (levelType is not null && levelType.DevOnly)
                continue;

            string? levelName = levelTypeName switch
            {
                "BloodMoon1v1" => "Small Fortress of Lions",
                _ => levelType?.DisplayName,
            };
            if (levelName is null) continue;

            writer.Write("{{MapListing|");
            writer.Write(levelName);
            writer.WriteLine("}}");
        }
        writer.WriteLine("{{MapListing/bottom}}");
    }

    #region removed map sets

    private static readonly LevelSetType BUBBLE_TAG_ALL = new()
    {
        LevelSetName = null!,
        DisplayNameKey = null!,
        LevelTypes = [
            "CrystalTemple",
            "BigCrystalTemple",
            "SmallBrawlhaven",
            "SmallTemple",
            "Grove",
            "KingsPass",
            "Blackguard",
            "SmallEnigma",
            "Fortress",
            "GreatHall",
            "ShipwreckFalls",
            "MiamiDome",
            "SmallFangwild",
            "BattleHill",
            "WarShuttle",
            "Brawlhaven",
            "Temple",
            "BigGrove",
            "BigTitansEnd",
            "BigGreatHall",
            "Fangwild",
            "Buddy",
            "Soccer",
            "SmallGalvanPrime",
            "GalvanPrime",
            "SpiritRealm",
            "SmallTMNT",
            "BloodMoon1v1",
            "BP52v2",
            "BP5FFA",
            "Norse1v1Spike",
            "BP9EndTimesSmall",
            "RefineryDoors",
        ],
    };

    private static readonly LevelSetType BUBBLE_TAG_2v2 = new()
    {
        LevelSetName = null!,
        DisplayNameKey = null!,
        LevelTypes = [
            "CrystalTemple",
            "BigCrystalTemple",
            "SmallBrawlhaven",
            "SmallTemple",
            "Grove",
            "KingsPass",
            "Blackguard",
            "SmallEnigma",
            "Fortress",
            "GreatHall",
            "ShipwreckFalls",
            "MiamiDome",
            "SmallFangwild",
            "BattleHill",
            "WarShuttle",
            "Buddy",
            "SmallGalvanPrime",
            "SpiritRealm",
            "SmallTMNT",
            "BloodMoon1v1",
            "BP52v2",
            "Norse1v1Spike",
            "BP9EndTimesSmall",
        ],
    };

    private static readonly LevelSetType BUBBLE_TAG_BIG = new()
    {
        LevelSetName = null!,
        DisplayNameKey = null!,
        LevelTypes = [
            "BigCrystalTemple",
            "Brawlhaven",
            "Temple",
            "BigGrove",
            "BigTitansEnd",
            "BigGreatHall",
            "Fangwild",
            "Soccer",
            "GalvanPrime",
            "BP5FFA",
            "RefineryDoors",
        ],
    };

    private static readonly LevelSetType SNOWBALL_FFA = new()
    {
        LevelSetName = null!,
        DisplayNameKey = null!,
        LevelTypes = [
            "Brawlhaven",
            "Temple",
            "Grove",
            "KingsPass",
            "Stadium",
            "BigTitansEnd",
            "Blackguard",
            "Enigma",
            "Fortress",
            "GreatHall",
            "ShipwreckFalls",
            "LostLabyrinth",
            "Fangwild",
            "SmallFangwild",
            "BP9EndTimesSmall",
            "Mustafar",
            "ThreeShips",
            "Climb",
            "BloodMoonClimb",
            "Showdown",
            "ShowdownNoTraps",
            "GIDoJoe",
            "SmallMovingPlatform",
        ],
    };

    #endregion
}