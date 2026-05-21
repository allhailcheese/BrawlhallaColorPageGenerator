using System.Collections.Generic;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetWeaponSkinNameParams(WeaponSkinType weaponSkinType, bool colorMode)
    {
        string displayNameKey = weaponSkinType.DisplayNameKey!;

        string weaponSkinName = LangFile.Entries[displayNameKey];
        string imageName = weaponSkinName;
        ImageExtensionEnum extension = ImageExtensionEnum.Png;
        string displayName = weaponSkinName;

        bool useLevelSuffix = true;

        switch (weaponSkinType.WeaponSkinName)
        {
            case "AxeSimon":
                displayName = imageName = weaponSkinName = "Battle Axe (Simon Belmont)";
                break;
            case "AxeGilded":
                weaponSkinName = "Gilded Glory (Axe Skin)";
                if (!colorMode) imageName = "Gilded Glory (Axe)";
                break;
            case "AxeActualValk":
                weaponSkinName = "Glory (Weapon Skin)";
                break;
            case "PistolSerape":
                weaponSkinName = "Snake Eyes (Weapon Skin)";
                if (colorMode) imageName = weaponSkinName;
                break;
            case "BowOldKoji":
                weaponSkinName = "Heirloom (Bow Skin)";
                if (!colorMode) imageName = "Heirloom (Bow)";
                break;
            case "CannonDestinyTitan":
                imageName = "Dragon's Breath (Titan)";
                break;
            case "FistsVolcano":
                weaponSkinName = "Hot Lava (Weapon Skin)";
                break;
            case "FistsOrb4":
                weaponSkinName = "Knockouts (Weapon Skin)";
                if (!colorMode) imageName = "Knockouts (Weapon Skin)";
                break;
            case "HammerMadame":
                weaponSkinName = "Heirloom (Hammer Skin)";
                imageName = colorMode ? weaponSkinName : "Heirloom (Hammer)";
                break;
            case "RocketLanceMotorcycle":
                weaponSkinName = "Burnout (Weapon Skin)";
                break;
            case "SpearGem":
                weaponSkinName = "Dusk (Weapon Skin)";
                imageName = colorMode ? weaponSkinName : "Dusk Spear";
                break;
            case "SpearViral":
                weaponSkinName = "Vector (Weapon Skin)";
                imageName = "Vector Spear";
                break;
            case "SwordBladeDancerCelestial":
                imageName = weaponSkinName = "Moonbeam Blade (Chakora Priya)";
                break;
            case "FistsSantaShang":
                imageName = weaponSkinName = "Holly Jolly (Santa Wu Shang)";
                break;
            case "AxeJotun":
                weaponSkinName = displayName = "World Cleaver (Jotun Ulgrim)";
                break;
            case "AxeHolidayXull":
                displayName = imageName = weaponSkinName = "World Cleaver (Abominable Jötunn Xull)";
                break;
            case "BowTrickOrTreat":
                if (!colorMode) imageName = "ded";
                break;
            case "PistolBubblegum":
            case "BowCyberSam":
            case "FistsDemon01":
            case "FistsDemon02":
            case "FistsDemon03":
            case "OrbDemon01":
            case "OrbDemon02":
            case "OrbDemon03":
            case "HammerScientist":
            case "OrbBattlePassSet2":
            case "RocketLanceScientist":
            case "ScythePaleRider":
            case "SpearDemonQueen":
            case "SpearPaleRider":
            case "SwordDemonQueen":
            case "SwordCyberSam":
                if (!colorMode)
                {
                    imageName = "Ani" + imageName;
                    extension = ImageExtensionEnum.Gif;
                    useLevelSuffix = false;
                }
                break;
            case "PistolMythicNix":
            case "FistsMythicWuShang":
            case "FistsMythicWerewolf":
            case "OrbOddball":
            case "OrbLilith":
            case "ScytheMythicNix":
            case "ScytheMythicWerewolf":
            case "SpearMythicWuShang":
                if (!colorMode)
                {
                    extension = ImageExtensionEnum.Gif;
                    useLevelSuffix = false;
                }
                break;
        }

        // Poppin’ TNTina
        weaponSkinName = weaponSkinName.Replace('’', '\'');
        displayName = displayName.Replace('’', '\'');
        imageName = imageName.Replace('’', '\'');

        // progression level
        if (WeaponSkinTypes.UpgradeLevel.TryGetValue(weaponSkinType.WeaponSkinName, out int upgradeLevel) && upgradeLevel != 0)
        {
            if (colorMode) displayName += " (Lvl " + upgradeLevel + ")";
            if (colorMode || useLevelSuffix) imageName += " Level " + upgradeLevel;
        }

        // names that are too long
        if (!colorMode)
        {
            if (SUPER_LONG_WEAPON_SKIN_NAMES.Contains(weaponSkinType.WeaponSkinName))
                displayName = "<span style=\"font-size:69%\">" + displayName + "</span>";
            else if (LONG_WEAPON_SKIN_NAMES.Contains(weaponSkinType.WeaponSkinName))
                displayName = "{{small|" + displayName + "}}";
        }

        // html escape for the template
        if (colorMode)
        {
            weaponSkinName = weaponSkinName.Replace(":", "&#58;");
            displayName = displayName.Replace(":", "&#58;");
        }

        // no : in image names
        imageName = imageName.Replace(":", "");

        return new()
        {
            Name = weaponSkinName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }

    private static readonly HashSet<string> SUPER_LONG_WEAPON_SKIN_NAMES = [
        "AxeHolidayXull",
        "SpearDarthMaul",
        "GreatswordMagicalGirl",
    ];

    private static readonly HashSet<string> LONG_WEAPON_SKIN_NAMES = [
        "AxeMagicalGirl",
        "AxeSpringAxe21Viewer",
        "AxeJotun",
        "BootsMagicalGirl",
        "PistolHighwayman",
        "PistolChewbacca",
        "PistolHanSolo",
        "PistolEmperor",
        "PistolBP7",
        "PistolLilith",
        "PistolMando",
        "PistolPearlDerringer",
        "PistolKayVess",
        "PistolBP10Mecha",
        "PistolBP12",
        "CannonBP7",
        "CannonRoboPuppet",
        "CannonBP12",
        "CannonBP10Mecha",
        "FistsBP7",
        "FistsPetraBP12",
        "FistsObiWan",
        "FistsBP10Mecha",
        "GreatswordAsgardSaber",
        "GreatswordBP7",
        "GreatswordBP12",
        "GreatswordMechaArmor",
        "HammerMagicalGirl",
        "KatarAhsoka",
        "KatarAsgardSaber",
        "OrbMagicalGirl",
        "RocketLanceBP12",
        "RocketlanceBP10Mecha",
        "ScytheEgyptianShoujo",
        "ScytheIceSkates",
        "ScytheBP10Mecha",
        "SpearMagicalGirl",
        "SwordMagicalGirl",
        "SwordDarthMaul",
        "GreatswordLuke",
    ];
}