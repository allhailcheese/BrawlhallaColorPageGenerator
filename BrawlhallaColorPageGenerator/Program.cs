using System;
using System.IO;
using BrawlhallaColorPageGenerator;
using BrawlhallaColorPageGenerator.Writers;
using BrawlhallaColorPageGenerator.Writers.Bundles;
using BrawlhallaColorPageGenerator.Writers.Colors;

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

WriterData data = WriterData.ReadFromBrawlhallaDir(BRAWLHALLA_FOLDER, swzKey);


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

MapSetWriter mapSetWriter = new(data);
mapSetWriter.WriteTo("outputs/pages/Map Set.mediawiki");

BundleCostWriter bundleCostWriter = new(data);
bundleCostWriter.WriteTo("outputs/pages/Template BundleCost.mediawiki");

Directory.CreateDirectory("outputs/pages/bundles");
BundlePageWriter bundlePageWriter = new(data);
bundlePageWriter.WriteTo("outputs/pages/bundles");
