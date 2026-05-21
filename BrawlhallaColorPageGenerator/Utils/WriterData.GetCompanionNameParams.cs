using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetCompanionNameParams(CompanionType companionType)
    {
        // string companion = companionType.CompanionName;
        string displayNameKey = companionType.DisplayNameKey;

        string companionName = LangFile.Entries[displayNameKey];
        string imageName = companionName;
        string displayName = companionName;

        companionName = companionName.Replace(":", "&#58;");
        displayName = displayName.Replace(":", "&#58;");
        imageName = imageName.Replace(":", "");

        return new()
        {
            Name = companionName,
            DisplayName = displayName,
            Image = imageName,
            Extension = ImageExtensionEnum.Png,
        };
    }
}