using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public sealed class SkinsWriter(WriterData data)
{
    public void WriteTo(string path, string heroName)
    {
        using StreamWriter writer = new(path) { NewLine = "\n" };
        writer.WriteLine("{{Itembox/top}}");
        foreach (CostumeType costumeType in data.CostumeTypes.Costumes)
        {
            if (
                costumeType.OwnerHero != heroName || // not my hero
                costumeType.DisplayNameKey is null || // default skin
                costumeType.CostumeName.StartsWith("ZombieWalker") ||
                costumeType.CostumeName.EndsWith("Stance2") ||
                // only show level 3
                costumeType.UpgradesTo is not null
            ) continue;

            ProcessCostumeType(costumeType, writer);
        }
        writer.WriteLine("{{Itembox/bottom}}");
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