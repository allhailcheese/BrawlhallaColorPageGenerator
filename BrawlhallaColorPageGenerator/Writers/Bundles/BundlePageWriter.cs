using System;
using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Bundles;

public sealed class BundlePageWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        foreach (AvatarType avatar in data.AvatarTypes.Avatars)
        {
            if (avatar.AvatarName == "Template") continue;
            _ = data.GetAvatarNameParams(avatar);
        }

        foreach (StoreType bundle in BundleUtils.Bundles(data))
        {
            WriteBundlePage(path, bundle);
        }
    }

    private void WriteBundlePage(string basePath, StoreType bundle)
    {
        string bundleName = data.LangFile.Entries[bundle.DisplayNameKey!];
        string filePath = Path.ChangeExtension(Path.Join(basePath, bundleName), ".mediawiki");
        using StreamWriter writer = new(filePath) { NewLine = "\n" };

        writer.WriteLine("{{Store Item Infobox");
        writer.Write("| image = Bundle ");
        string imageName = bundleName.EndsWith(" Bundle") ? bundleName[..^" Bundle".Length] : bundleName;
        writer.Write(imageName);
        writer.WriteLine(".jpg");
        writer.WriteLine("""
| blank_name1 = Max Cost
| blank1 = {{BundleCost|{{PAGENAME}}|mammoth}}
| blank_name2 = Discount
| blank2 = {{BundleCost|{{PAGENAME}}|discount}} off
| blank_name3 = Store Description
""");
        writer.Write("| blank3      = ");
        writer.WriteLine(data.LangFile.Entries[bundle.DescriptionKey!]);
        writer.WriteLine("}}");
        writer.WriteLine();

        writer.WriteLine("""
'''{{PAGENAME}}''' was one of the [[Store Bundles|Bundles]] available in the game [[Brawlhalla]]. 

This bundle can cost a maximum of {{BundleCost|{{PAGENAME}}|mammoth}}, which is a {{BundleCost|{{PAGENAME}}|discount}} discount. If some items in the bundle are already owned, the price will be lowered accordingly.

""");
        if (BUNDLES_NO_LONGER_AVAIL.Contains(bundle.StoreName))
        {
            writer.WriteLine("'''This bundle is no longer available'''");
            writer.WriteLine();
        }
        else if (bundle.LockToTimedPromotion || bundle.StoreName == "BHFest25ValhallaDevilsBundle")
        {
            writer.Write("'''This bundle is only available for purchase during ");
            writer.Write(data.GetBundleEvent(bundle.StoreName));
            writer.WriteLine("'''");
            writer.WriteLine();
        }

        writer.WriteLine("==Contents==");
        writer.WriteLine();

        writer.Write("This bundle contains ");
        writer.Write(bundle.ItemList.Length);
        writer.WriteLine(" items:");
        writer.WriteLine();

        writer.WriteLine("{{Itembox/top|op=justify-content:unset;align-items:unset}}");
        foreach (string itemName in bundle.ItemList)
        {
            StoreType item = data.StoreTypes.StoresMap[itemName];
            WriteBundleItem(writer, item);
        }
        writer.WriteLine("{{itembox/bottom}}");
        writer.WriteLine();
        writer.WriteLine("[[Category:Bundles]]");
    }

    private void WriteBundleItem(StreamWriter writer, StoreType item)
    {
        ItemNameParams nameParams = item.Type switch
        {
            "Costume" or "Hero" => data.GetSkinNameParams(data.CostumeTypes.CostumesMap[item.Item!], false),
            "WeaponSkin" => data.GetWeaponSkinNameParams(data.WeaponSkinTypes.WeaponSkinsMap[item.Item!], false),
            "Taunt" => data.GetTauntNameParams(data.TauntTypes.TauntsMap[item.Item!]),
            "SpawnBot" => data.GetSpawnBotNameParams(data.SpawnBotTypes.SpawnBotsMap[item.Item!]),
            "KOEffect" => data.GetKOEffectNameParams(data.TrailEffectTypes.TrailEffectsMap[item.Item!]),
            "Avatar" => data.GetAvatarNameParams(data.AvatarTypes.AvatarsMap[item.Item!]),
            "Podium" => data.GetPodiumNameParams(data.PodiumTypes.PodiumsMap[item.Item!]),
            "Emoji" => data.GetEmojiNameParams(data.EmojiTypes.EmojisMap[item.Item!]),
            "EmitterGroup" => default, // TODO
            "Companion" => data.GetCompanionNameParams(data.CompanionTypes.CompanionsMap[item.Item!]),
            "Moniker" => default, // TODO
            _ => throw new ArgumentException($"Unsupported bundle item type {item.Type}"),
        };

        writer.Write("{{itembox|width=220|height=270|name=");
        writer.Write(nameParams.Name);
        if (nameParams.Name != nameParams.DisplayName)
        {
            writer.Write("|displayname=");
            writer.Write(nameParams.DisplayName);
        }
        writer.Write("|image=");
        writer.Write(nameParams.Image);
        writer.Write('.');
        writer.Write(nameParams.Extension.GetName());
        if (item.Type == "Avatar" || item.Type == "Emoji")
        {
            writer.Write("|nolink=true");
        }
        writer.WriteLine("}}");
    }

    private static readonly HashSet<string> BUNDLES_NO_LONGER_AVAIL = [
        "ShinigamiJiroBundle", // Shinigami Jiro Bundle
        "KitsuneHattoriBundle", // Kitsune Hattori Bundle
        "NewLegendBundle", // Lady Vera Launch Bundle
        "DarkheartMonsterBundle", // Rupture Launch Bundle
        "ActualGladiatorBundle", // Aurus Launch Bundle
        "LightSideBundle", // Light Side Bundle
        "RaymesisBundle", // Alter Ego Bundle
        "GloboxBundle", // Best Friend Bundle
        "EpicRaymanBundle", // Super Metal Rayman Bundle
    ];
}