using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetPodiumNameParams(PodiumType podiumType)
    {
        string displayNameKey = podiumType.DisplayNameKey!;

        string podiumName = LangFile.Entries[displayNameKey];
        string imageName = podiumName;
        ImageExtensionEnum extension = ImageExtensionEnum.Png;
        string displayName = podiumName;

        switch (podiumType.PodiumName)
        {
            case "Carbonite":
                podiumName = imageName = "Bespin Carbon Freezing Chamber";
                displayName = "<span style=\"font-size:82%\">" + podiumName + "</span>";
                break;
        }

        imageName = "Podium " + imageName;
        if (podiumName.StartsWith("Heatwave")) podiumName += " (Podium)";

        // TODO: levelups

        return new()
        {
            Name = podiumName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }
}