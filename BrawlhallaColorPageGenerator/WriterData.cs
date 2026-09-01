using System;
using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;
using BrawlhallaSwz;

namespace BrawlhallaColorPageGenerator;

public sealed partial class WriterData
{
    public required CostumeTypes CostumeTypes { get; init; }
    public required WeaponSkinTypes WeaponSkinTypes { get; init; }
    public required HeroTypes HeroTypes { get; init; }
    public required RuneTypes RuneTypes { get; init; }
    public required CompanionTypes CompanionTypes { get; init; }
    public required StoreTypes StoreTypes { get; init; }
    public required EntitlementTypes EntitlementTypes { get; init; }
    public required ChanceBoxTypes ChanceBoxTypes { get; init; }
    public required ColorSchemeTypes ColorSchemeTypes { get; init; }
    public required LevelTypes LevelTypes { get; init; }
    public required LevelSetTypes LevelSetTypes { get; init; }
    public required GameModeTypes GameModeTypes { get; init; }
    public required ScoringTypes ScoringTypes { get; init; }
    public required ItemTypes ItemTypes { get; init; }
    public required ItemSpawnRuleSetTypes ItemSpawnRuleSetTypes { get; init; }
    public required TauntTypes TauntTypes { get; init; }
    public required SpawnBotTypes SpawnBotTypes { get; init; }
    public required TrailEffectTypes TrailEffectTypes { get; init; }
    public required AvatarTypes AvatarTypes { get; init; }
    public required PodiumTypes PodiumTypes { get; init; }
    public required EmojiTypes EmojiTypes { get; init; }
    public required LangFile LangFile { get; init; }

    public static WriterData ReadFromBrawlhallaDir(string path, uint swzKey)
    {
        Dictionary<string, string> files = [];

        // load Game.swz
        string gameSwz = Path.Combine(path, "Game.swz");
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
        string initSwz = Path.Combine(path, "Init.swz");
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
        string lang = Path.Combine(path, "languages", "language.1.bin");
        LangFile langFile;
        using (FileStream file = File.OpenRead(lang))
            langFile = LangFile.Load(file);

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

        // Taunt types
        string tauntTypesContent = files["TauntTypes.xml"];
        TauntTypes tauntTypes = new(tauntTypesContent);

        // Spawn bot types
        string spawnBotTypesContent = files["SpawnBotTypes.xml"];
        SpawnBotTypes spawnBotTypes = new(spawnBotTypesContent);

        // KO effect types
        string trailEffectTypesContent = files["TrailEffectTypes.xml"];
        TrailEffectTypes trailEffectTypes = new(trailEffectTypesContent);

        // Avatar types
        string avatarTypesContent = files["avatarTypes.csv"];
        AvatarTypes avatarTypes = new(avatarTypesContent);

        // Podium types
        string podiumTypesContent = files["PodiumTypes.xml"];
        PodiumTypes podiumTypes = new(podiumTypesContent);

        // Emoji types
        string emojiTypesContent = files["EmojiTypes.xml"];
        EmojiTypes emojiTypes = new(emojiTypesContent);

        return new()
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
            TauntTypes = tauntTypes,
            SpawnBotTypes = spawnBotTypes,
            TrailEffectTypes = trailEffectTypes,
            AvatarTypes = avatarTypes,
            PodiumTypes = podiumTypes,
            EmojiTypes = emojiTypes,
            LangFile = langFile,
        };
    }
}