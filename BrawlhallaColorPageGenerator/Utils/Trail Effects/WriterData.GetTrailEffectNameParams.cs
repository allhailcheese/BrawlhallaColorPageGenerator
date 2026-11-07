using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetTrailEffectNameParams(EmitterGroupType trailEffectType)
    {
        string displayNameKey = trailEffectType.DisplayNameKey!;

        string emitterGroupName = LangFile.Entries[displayNameKey];
        string imageName = emitterGroupName;
        ImageExtensionEnum extension = ImageExtensionEnum.Png;
        string displayName = emitterGroupName;

        imageName = "TrailIcon " + imageName;

        return new()
        {
            Name = emitterGroupName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }
}