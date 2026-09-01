using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetKOEffectNameParams(TrailEffectType koEffectType)
    {
        string displayNameKey = koEffectType.DisplayNameKey!;

        string koEffectName = LangFile.Entries[displayNameKey];
        string imageName = koEffectName;
        ImageExtensionEnum extension = ImageExtensionEnum.Gif;
        switch (koEffectType.TrailEffectName)
        {
            case "BHFest2026":
            case "GingerBreadMan1":
                extension = ImageExtensionEnum.Webp;
                break;
        }
        string displayName = koEffectName;

        imageName = "KO " + imageName;

        return new()
        {
            Name = koEffectName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }
}