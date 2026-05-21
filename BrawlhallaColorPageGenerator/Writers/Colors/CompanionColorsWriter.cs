using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class CompanionColorsWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("<includeonly><onlyinclude>");
        writer.WriteLine("The following is a list of all companions in {{{1|}}}. ''Click an image to view it in higher resolution.''");
        writer.WriteLine();
        writer.WriteLine("{{List to itembox|color={{{1|}}}|");
        foreach (CompanionType companion in data.CompanionTypes.Companions)
        {
            if (companion.CompanionName == "Template") continue;

            ItemNameParams itemName = data.GetCompanionNameParams(companion);

            writer.Write(itemName.Name);
            if (itemName.Name != itemName.DisplayName)
            {
                writer.Write(" && displayname:");
                writer.Write(itemName.DisplayName);
            }
            writer.Write(" && image:Companion ");
            writer.Write(itemName.Image);
            writer.Write(" Idle $1.");
            writer.Write(itemName.Extension.GetName());
            writer.WriteLine();
        }
        writer.WriteLine("}}");

        writer.WriteLine("[[Category:Companions in all colors]]</onlyinclude></includeonly>");
        writer.WriteLine("<noinclude>");
        writer.WriteLine("{{doc}}");
        writer.WriteLine("</noinclude>");
    }
}