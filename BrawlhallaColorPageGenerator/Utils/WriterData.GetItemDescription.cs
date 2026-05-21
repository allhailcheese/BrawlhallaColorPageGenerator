using System;
using System.Collections.Generic;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemDescription GetItemDescription(string itemName, ItemTypeEnum itemType)
    {
        // get store type
        StoreType? storeType = StoreTypes.ItemToStoreType.GetValueOrDefault((itemType.GetName(), itemName));

        string description;
        DescriptionTypeEnum descriptionType;

        // override description
        if (MISC_ITEM_DESCRIPTIONS.TryGetValue(itemName, out string? itemDescription))
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = itemDescription;
        }
        // weapon skin from a legend skin
        else if (itemType == ItemTypeEnum.WeaponSkin && GetWeaponSkinSourceCostume(itemName) is CostumeType costume)
        {
            descriptionType = DescriptionTypeEnum.Desc;
            ItemNameParams skinNameParams = GetSkinNameParams(costume, false);
            description = "[[" + skinNameParams.Name + "]]";
        }
        // metadev skin
        else if (itemType == ItemTypeEnum.Costume && (UNTAGGED_METADEV_SKINS.Contains(itemName) || CostumeTypes.CostumesMap[itemName].IsMetadev))
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "Not normally obtainable.<br>See [[Metadev]].";
        }
        // chest exclusive
        else if (GetItemChestExclusive(itemName) is string chestName)
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "{{ItemTag|chest|" + chestName + "}}";
        }
        // store
        else if (storeType is not null)
        {
            descriptionType = DescriptionTypeEnum.Cost;
            description = GetStoreTypeDescription(storeType, smallItemTag: itemType switch
            {
                ItemTypeEnum.Costume => false,
                ItemTypeEnum.WeaponSkin => true,
                _ => false,
            });

            description += itemName switch
            {
                "Eivor" => "<br>Comes with [[Eivor|Eivor (Male)]]",
                "EivorMale" => "<br>Comes with [[Eivor|Eivor (Female)]]",
                "Lara" => "<br>Comes with [[Survivor Lara Croft]]",
                "Croft" => "<br>Comes with [[Lara Croft]]",
                _ => null,
            };
        }
        // pack exclusive
        else if (GetItemPackExclusive(itemName, itemType) is string packName)
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "[[" + packName + "]]";
            if (itemType == ItemTypeEnum.Costume) description = "Part of the " + description + ".";
        }
        // unknown
        else
        {
            descriptionType = DescriptionTypeEnum.Desc;
            description = "UNKNOWN";
        }

        RarityEnum rarity = storeType is not null ? storeType.Rarity switch
        {
            "Epic" or "EpicCrossover" or "Crossover" => RarityEnum.Epic,
            "Mythic" => RarityEnum.Mythic,
            _ => RarityEnum.None,
        } : RarityEnum.None;

        // battlepass epic skins
        if (itemType == ItemTypeEnum.Costume && EPIC_BATTLEPASS_SKINS.Contains(itemName))
        {
            rarity = RarityEnum.Epic;
        }

        return new()
        {
            Description = description,
            DescriptionType = descriptionType,
            Rarity = rarity,
        };
    }

    private static readonly HashSet<string> EPIC_BATTLEPASS_SKINS = [
        "DemonQueen",
        "EpicNix",
        "EpicBrynn",
        "EpicDiana",
        "EpicOrion",
        "EpicEmber",
        "EpicWarlock",
        "EpicMordex",
        "EpicRaptor",
        "EgyptianShoujo",
        "EpicWitch",
        "EpicDragon",
    ];

    // metadev skins not marked with IsMetadev
    private static readonly HashSet<string> UNTAGGED_METADEV_SKINS = [
        "MDFait",
        "MetadevNix",
        "MetadevJaeyun",
    ];
}