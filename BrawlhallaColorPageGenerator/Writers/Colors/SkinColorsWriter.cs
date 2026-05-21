using System;
using System.Globalization;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class SkinColorsWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly>");
        writer.WriteLine("The following is a list of all skins in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        writer.WriteLine();
        writer.WriteLine("{{Compact TOC}}");
        char currentLetter = '~';
        foreach (HeroType hero in data.HeroTypes.Heroes)
        {
            if (!hero.IsActive || hero.HeroName == "Random") continue;

            ArgumentNullException.ThrowIfNull(hero.BioName);
            string name = hero.BioName;
            char firstLetter = name[0];
            if (currentLetter != firstLetter)
            {
                currentLetter = firstLetter;
                writer.Write("<span id=\"");
                writer.Write(currentLetter);
                writer.WriteLine("\"></span>");
            }

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            string titleCaseName = textInfo.ToTitleCase(name);
            writer.Write("===[[");
            writer.Write(titleCaseName);
            writer.WriteLine("]]===");
            writer.WriteLine("{{List to itembox|color={{{1|}}}|");
            foreach (CostumeType costumeType in data.CostumeTypes.Costumes)
            {
                if (
                    costumeType.OwnerHero != hero.HeroName || // not my hero
                    costumeType.CostumeName.StartsWith("ZombieWalker") ||
                    costumeType.CostumeName.EndsWith("Stance2")
                ) continue;

                ItemNameParams nameParams = data.GetSkinNameParams(costumeType, true);

                writer.Write(nameParams.Name);
                if (nameParams.Name != nameParams.DisplayName)
                {
                    writer.Write(" && displayname:");
                    writer.Write(nameParams.DisplayName);
                }
                if (nameParams.Name != nameParams.Image)
                {
                    writer.Write(" && image:");
                    writer.Write(nameParams.Image);
                    writer.Write(" $1.");
                    writer.Write(nameParams.Extension.GetName());
                }
                writer.WriteLine();
            }
            writer.WriteLine("}}");
        }
        writer.WriteLine("[[Category:Skins in all colors]]</includeonly>");
        writer.WriteLine("<noinclude>");
        writer.WriteLine("{{doc}}");
        writer.WriteLine("</noinclude>");
    }
}