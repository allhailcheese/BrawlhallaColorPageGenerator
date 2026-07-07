using System;
using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaColorPageGenerator.Writers;
using BrawlhallaColorPageGenerator.Writers.Colors;
using BrawlhallaLangReader;
using BrawlhallaSwz;

const string BRAWLHALLA_FOLDER = "C:/Program Files (x86)/Steam/steamapps/common/Brawlhalla";

uint swzKey;
if (args.Length < 1)
{
    Console.WriteLine("Please insert the swz key");
    swzKey = uint.Parse(Console.ReadLine()!);
}
else
{
    swzKey = uint.Parse(args[1]);
}

#region File loading

Dictionary<string, string> files = [];

// load Game.swz
string gameSwz = Path.Combine(BRAWLHALLA_FOLDER, "Game.swz");
using (FileStream file = File.OpenRead(gameSwz))
{
    using SwzReader swzReader = new(file, swzKey);
    foreach (string fileContent in swzReader.ReadFiles())
    {
        string fileName = SwzUtils.GetFileName(fileContent);
        files[fileName] = fileContent;
    }
}

// load Init.swz
string initSwz = Path.Combine(BRAWLHALLA_FOLDER, "Init.swz");
using (FileStream file = File.OpenRead(initSwz))
{
    using SwzReader swzReader = new(file, swzKey);
    foreach (string fileContent in swzReader.ReadFiles())
    {
        string fileName = SwzUtils.GetFileName(fileContent);
        files[fileName] = fileContent;
    }
}

// load english language
string lang = Path.Combine(BRAWLHALLA_FOLDER, "languages", "language.1.bin");
LangFile langFile;
using (FileStream file = File.OpenRead(lang))
    langFile = LangFile.Load(file);

#endregion
#region Parsing

// Costumes
string costumeTypesContent = files["costumeTypes.csv"];
CostumeTypes costumeTypes = new(costumeTypesContent);
Array.Sort(costumeTypes.Costumes, Comparer<CostumeType>.Create((a, b) =>
{
    if (a.OwnerHero != b.OwnerHero) return string.Compare(a.OwnerHero, b.OwnerHero);

    if (a.DisplayNameKey == b.DisplayNameKey)
    {
        int upgradeLevelA = costumeTypes.UpgradeLevel.GetValueOrDefault(a.CostumeName, 0);
        int upgradeLevelB = costumeTypes.UpgradeLevel.GetValueOrDefault(b.CostumeName, 0);
        if (upgradeLevelA != upgradeLevelB)
            return upgradeLevelA.CompareTo(upgradeLevelB);
    }

    return a.CostumeIndex.CompareTo(b.CostumeIndex);
}));

// Weapon skins
string weaponSkinTypesContent = files["weaponSkinTypes.csv"];
WeaponSkinTypes weaponSkinTypes = new(weaponSkinTypesContent);
Array.Sort(weaponSkinTypes.WeaponSkins, Comparer<WeaponSkinType>.Create((a, b) =>
{
    if (a.BaseWeapon != b.BaseWeapon) return string.Compare(
        Utils.BASE_WEAPON_NAME[a.BaseWeapon],
        Utils.BASE_WEAPON_NAME[b.BaseWeapon]
    );

    if (a.DisplayNameKey == b.DisplayNameKey)
    {
        int upgradeLevelA = weaponSkinTypes.UpgradeLevel.GetValueOrDefault(a.WeaponSkinName, 0);
        int upgradeLevelB = weaponSkinTypes.UpgradeLevel.GetValueOrDefault(b.WeaponSkinName, 0);
        return upgradeLevelA.CompareTo(upgradeLevelB);
    }
    else
    {
        string aName = langFile.Entries.GetValueOrDefault(a.DisplayNameKey ?? "", "~" + a.WeaponSkinName);
        string bName = langFile.Entries.GetValueOrDefault(b.DisplayNameKey ?? "", "~" + b.WeaponSkinName);
        if (aName != bName)
            return string.Compare(aName, bName);

        return a.WeaponSkinID.CompareTo(b.WeaponSkinID);
    }
}));

// Companions
string companionTypesContent = files["CompanionTypes.xml"];
CompanionTypes companionTypes = new(companionTypesContent);
Array.Sort(companionTypes.Companions, Comparer<CompanionType>.Create((a, b) =>
{
    string aName = langFile.Entries.GetValueOrDefault(a.DisplayNameKey, "~" + a.CompanionName);
    string bName = langFile.Entries.GetValueOrDefault(b.DisplayNameKey, "~" + b.CompanionName);
    return string.Compare(aName, bName);
}));

// Heros
string heroTypesContent = files["HeroTypes.xml"];
HeroTypes heroTypes = new(heroTypesContent);
Array.Sort(heroTypes.Heroes, Comparer<HeroType>.Create(static (a, b) =>
{
    return string.Compare(a.BioName, b.BioName);
}));

// Runes
string runeTypesContent = files["RuneTypes.xml"];
RuneTypes runeTypes = new(runeTypesContent);

// Store types
string storeTypesContent = files["storeTypes.csv"];
StoreTypes storeTypes = new(storeTypesContent);

// Entitlement types
string entitlementTypesContent = files["EntitlementTypes.xml"];
EntitlementTypes entitlementTypes = new(entitlementTypesContent);

// Chance box types
string chanceBoxTypesContent = files["ChanceBoxTypes.xml"];
ChanceBoxTypes chanceBoxTypes = new(chanceBoxTypesContent);

// Color scheme types
string colorSchemeTypesContent = files["ColorSchemeTypes.xml"];
ColorSchemeTypes colorSchemeTypes = new(colorSchemeTypesContent);

// Level types
string levelTypesContent = files["LevelTypes.xml"];
LevelTypes levelTypes = new(levelTypesContent);

// Level set types
string levelSetTypesContent = files["LevelSetTypes.xml"];
LevelSetTypes levelSetTypes = new(levelSetTypesContent);

// Gamemode types
string gamemodeTypesContent = files["GameModeTypes.xml"];
GameModeTypes gameModeTypes = new(gamemodeTypesContent);

// Scoring types
string scoringTypesContent = files["ScoringTypes.xml"];
ScoringTypes scoringTypes = new(scoringTypesContent);

// Item types
string itemTypesContent = files["itemTypes.csv"];
ItemTypes itemTypes = new(itemTypesContent);

// Item spawn rule set types
string itemSpawnRuleSetTypesContent = files["ItemSpawnRuleSetTypes.xml"];
ItemSpawnRuleSetTypes itemSpawnRuleSetTypes = new(itemSpawnRuleSetTypesContent);

WriterData data = new()
{
    CostumeTypes = costumeTypes,
    WeaponSkinTypes = weaponSkinTypes,
    HeroTypes = heroTypes,
    RuneTypes = runeTypes,
    CompanionTypes = companionTypes,
    StoreTypes = storeTypes,
    EntitlementTypes = entitlementTypes,
    ChanceBoxTypes = chanceBoxTypes,
    ColorSchemeTypes = colorSchemeTypes,
    LevelTypes = levelTypes,
    LevelSetTypes = levelSetTypes,
    GameModeTypes = gameModeTypes,
    ScoringTypes = scoringTypes,
    ItemTypes = itemTypes,
    ItemSpawnRuleSetTypes = itemSpawnRuleSetTypes,
    LangFile = langFile,
};

#endregion
#region Writing

Directory.CreateDirectory("outputs/pages");

{
    SkinColorsWriter skinsColorWriter = new(data);
    skinsColorWriter.WriteTo("outputs/pages/Template Color_Skins.mediawiki");

    WeaponSkinColorsWriter weaponSkinsColorWriter = new(data);
    weaponSkinsColorWriter.WriteTo("outputs/pages/Template Color_Weapon_Skins.mediawiki");

    CompanionColorsWriter companionsColorWriter = new(data);
    companionsColorWriter.WriteTo("outputs/pages/Template Color_Companions.mediawiki");
}

LevelingWriter levelingWriter = new(data);
levelingWriter.WriteTo("outputs/pages/Template LegendLevelingRowByName.mediawiki");

StancesWriter stancesWriter = new(data);
stancesWriter.WriteTo("outputs/pages/Template LegendStancesRowByName.mediawiki");

QuestListWriter questListWriter = new(data);
Directory.CreateDirectory("outputs/pages/Template QuestList");
// TODO: make this just be two for loops
questListWriter.WriteTo("outputs/pages/Template QuestList/HighStrength.mediawiki", StatEnum.Strength, StatQuestType.High);
questListWriter.WriteTo("outputs/pages/Template QuestList/HighDexterity.mediawiki", StatEnum.Dexterity, StatQuestType.High);
questListWriter.WriteTo("outputs/pages/Template QuestList/HighDefense.mediawiki", StatEnum.Defense, StatQuestType.High);
questListWriter.WriteTo("outputs/pages/Template QuestList/HighSpeed.mediawiki", StatEnum.Speed, StatQuestType.High);
questListWriter.WriteTo("outputs/pages/Template QuestList/MidStrength.mediawiki", StatEnum.Strength, StatQuestType.Mid);
questListWriter.WriteTo("outputs/pages/Template QuestList/MidDexterity.mediawiki", StatEnum.Dexterity, StatQuestType.Mid);
questListWriter.WriteTo("outputs/pages/Template QuestList/MidDefense.mediawiki", StatEnum.Defense, StatQuestType.Mid);
questListWriter.WriteTo("outputs/pages/Template QuestList/MidSpeed.mediawiki", StatEnum.Speed, StatQuestType.Mid);
questListWriter.WriteTo("outputs/pages/Template QuestList/LowStrength.mediawiki", StatEnum.Strength, StatQuestType.Low);
questListWriter.WriteTo("outputs/pages/Template QuestList/LowDexterity.mediawiki", StatEnum.Dexterity, StatQuestType.Low);
questListWriter.WriteTo("outputs/pages/Template QuestList/LowDefense.mediawiki", StatEnum.Defense, StatQuestType.Low);
questListWriter.WriteTo("outputs/pages/Template QuestList/LowSpeed.mediawiki", StatEnum.Speed, StatQuestType.Low);

SkinsWriter skinsWriter = new(data);
skinsWriter.WriteTo("outputs/pages/Skins.mediawiki");

{
    Directory.CreateDirectory("outputs/pages/Weapon_Skins");

    WeaponSkinWriter weaponSkinWriter = new(data);
    foreach ((string baseWeapon, string weaponName) in Utils.BASE_WEAPON_NAME)
        weaponSkinWriter.WriteTo($"outputs/pages/Weapon_Skins/{weaponName}.mediawiki", baseWeapon);

    DefaultWeaponSkinsWriter defaultWeaponSkinsWriter = new(data);
    defaultWeaponSkinsWriter.WriteTo("outputs/pages/Weapon_Skins/Default_Weapons.mediawiki");
}

MapColorExclusionWriter mapColorExclusionWriter = new(data);
Directory.CreateDirectory("outputs/pages/Template Map_Color_Exclusion");
mapColorExclusionWriter.WriteTo("outputs/pages/Template Map_Color_Exclusion/List.mediawiki");

BOTWWriter botwWriter = new(data);
botwWriter.WriteTo("outputs/pages/Template BOTW.mediawiki");

#endregion