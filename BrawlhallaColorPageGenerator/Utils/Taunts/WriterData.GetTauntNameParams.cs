using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetTauntNameParams(TauntType tauntType)
    {
        string displayNameKey = tauntType.DisplayNameKey!;

        string tauntName = LangFile.Entries[displayNameKey].Replace('’', '\'');
        string imageName = tauntName;
        ImageExtensionEnum extension = ImageExtensionEnum.Png;
        string displayName = tauntName;

        imageName = "Taunt " + imageName + " Still";

        return new()
        {
            Name = tauntName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }
}