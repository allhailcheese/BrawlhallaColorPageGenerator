using System;
using System.Globalization;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public sealed class SkinsWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine(
"""
[[File:Mallhalla_Skins.png|thumb|right|550px|Skins in the Store.]]

'''Skins''' are cosmetics that are unique to a Legend, and change how they look in game. Skins can be obtained in several ways, with most being purchasable from the [[Store]] for {{Coin|mammoth|mammoth coins}}. They can be equipped at the character select screen. All skins come with two unique [[Weapon Skins]].

In addition to regular skins there are [[Epic Skins]], [[Epic Crossover Skins]], [[Mythic Skins]] and [[Progression Skins]]. These are priced higher but can contain unique vanities, such as animated Weapon Skins, custom Signature FX or a dedicated roster spot. 

When a new Legend is released, that Legend will receive three skins. Legends can receive new skins with any update, with most skins releasing during [[Events]] and new [[Battle Pass|Battle Passes]].

==List of Skins==
''To quickly get to a Legend in this list, use the Table of Contents above.''
""");

        foreach (HeroType hero in data.HeroTypes.Heroes)
        {
            if (!hero.IsActive || hero.HeroName == "Random") continue;

            ArgumentNullException.ThrowIfNull(hero.BioName);
            string name = hero.BioName;
            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            string titleCaseName = textInfo.ToTitleCase(name);
            writer.WriteLine();
            writer.Write("===[[");
            writer.Write(titleCaseName);
            writer.WriteLine("]]===");
            writer.WriteLine("{{Itembox/top}}");
            foreach (CostumeType costumeType in data.CostumeTypes.Costumes)
            {
                if (
                    costumeType.OwnerHero != hero.HeroName || // not my hero
                    costumeType.DisplayNameKey is null || // default skin
                    costumeType.CostumeName.StartsWith("ZombieWalker") ||
                    costumeType.CostumeName.EndsWith("Stance2") ||
                    // only show level 3
                    costumeType.UpgradesTo is not null
                ) continue;

                ProcessCostumeType(costumeType, writer);
            }
            writer.WriteLine("{{Itembox/bottom}}");
            writer.WriteLine();
        }

        writer.WriteLine("[[Category:Brawlhalla]] [[Category:Cosmetics]]");
    }

    private void ProcessCostumeType(CostumeType costumeType, StreamWriter writer)
    {
        ItemNameParams nameParams = data.GetSkinNameParams(costumeType, false);

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

        ItemDescription description = data.GetItemDescription(costumeType.CostumeName, ItemTypeEnum.Costume);

        writer.Write('|');
        writer.Write(description.DescriptionType.GetName());
        writer.Write('=');
        writer.Write(description.Description);

        writer.Write("|");
        writer.Write(description.DescriptionType.GetName());
        writer.Write("height=49px");

        if (description.Rarity != RarityEnum.None)
        {
            writer.Write('|');
            writer.Write(description.Rarity.GetName());
            writer.Write("=true");
        }

        writer.WriteLine("}}");
    }
}