using System;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetAvatarNameParams(AvatarType avatarType)
    {
        string displayNameKey = avatarType.DisplayNameKey!;

        string avatarName = LangFile.Entries[displayNameKey];
        string imageName = avatarName;
        ImageExtensionEnum extension = ImageExtensionEnum.Png;
        string displayName = avatarName;

        // TODO: incomplete animated list

        switch (avatarType.AvatarName)
        {
            // internal name matches image name
            case "DoodleBodvar":
            case "DoodleCassidy":
            case "DoodleOrion":
            case "DoodleVraxx":
            case "DoodleGnash":
            case "DoodleNai":
            case "DoodleHattori":
            case "DoodleRoland":
            case "DoodleScarlet":
            case "DoodleThatch":
            case "DoodleAda":
            case "DoodleSentinel":
            case "DoodleLucien":
            case "DoodleTeros":
            case "DoodleBrynn":
            case "DoodleAsuri":
            case "DoodleBarraza":
            case "DoodleEmber":
            case "DoodleAzoth":
            case "DoodleKoji":
            case "DoodleUlgrim":
            case "DoodleDiana":
            case "DoodleJhala":
            case "DoodleKor":
            case "DoodleVal":
            case "DoodleRagnir":
            case "DoodleCross":
            case "DoodleMirage":
            case "DoodleNix":
            case "DoodleMordex":
            case "DoodleYumiko":
            case "DoodleArtemis":
            case "DoodleCaspian":
            case "DoodleSidra":
            case "DoodleXull":
            case "DoodleKaya":
            case "DoodleIsaiah":
            case "DoodleJiro":
            case "DoodleLinFei":
            case "DoodleZariel":
            case "DoodleRayman":
            case "DoodleDusk":
            case "DoodleFait":
            case "DoodleThor":
            case "DoodlePetra":
            case "DoodleVector":
            case "DoodleVolkov":
            case "DoodleOnyx":
            case "DoodleJaeyun":
            case "DoodleMako":
            case "DoodleMagyar":
            case "DoodleReno":
            case "DoodleMunin":
            case "DoodleHugin":
            case "DoodleArcadia":
            case "DoodleEzio":
            case "DoodleTezca":
            case "DoodleChel":
            case "DoodleThea":
            case "DoodleLoki":
            case "DoodleSeven":
            case "DoodleVivi":
            case "DoodleImugi":
            case "DoodlePriya":
            case "DoodleRansom":
            case "DoodleRandom":
                imageName = avatarType.AvatarName;
                break;
            // require mapping
            case "DoodleWushang":
                imageName = "DoodleWuShang";
                break;
            case "DoodleRaptor":
                imageName = "DoodleRedRaptor";
                break;
            case "DoodleAfricanKing":
                imageName = "DoodleZuva";
                break;
            case "DoodleCleric":
                imageName = "DoodleLadyVera";
                break;
            case "DoodleDarkheart":
                imageName = "DoodleRupture";
                break;
            case "DoodleActualGladiator":
                imageName = "DoodleAurus";
                break;
            case "DoodleAstro":
                imageName = "DoodleQinghuaBaobao";
                break;
            case "DoodleBob":
                break;
            case "HeatWave":
                imageName = "Heatwave";
                extension = ImageExtensionEnum.Gif;
                break;
            case "Flipbook":
                imageName = "Flipbook Cat";
                extension = ImageExtensionEnum.Gif;
                break;
            case "Jack-O-Lantern":
                imageName = "Pumpkin";
                extension = ImageExtensionEnum.Gif;
                break;
            case "Snowman":
                imageName = "Snowman";
                extension = ImageExtensionEnum.Gif;
                break;
            case "CandyHearts":
            case "PotOGold":
            case "SpringDaisy":
                extension = ImageExtensionEnum.Gif;
                break;
            default:
                if (avatarType.AvatarName.StartsWith("Doodle"))
                    throw new ArgumentException($"Unhandled doodle avatar {avatarType.AvatarName}");
                break;
        }

        switch (avatarType.AvatarName)
        {
            case "CandyHearts":
            case "PotOGold":
            case "SpringDaisy":
            case "HeatWave":
            case "Flipbook":
            case "Jack-O-Lantern":
            case "Snowman":
                imageName = "AniAvatar " + imageName;
                break;
            default:
                imageName = "Avatar " + imageName;
                break;
        }

        return new()
        {
            Name = avatarName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }
}