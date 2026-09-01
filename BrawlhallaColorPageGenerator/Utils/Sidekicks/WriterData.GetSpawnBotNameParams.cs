using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetSpawnBotNameParams(SpawnBotType spawnBotType)
    {
        string displayNameKey = spawnBotType.DisplayNameKey!;

        string spawnBotName = LangFile.Entries[displayNameKey];
        string imageName = spawnBotName;
        ImageExtensionEnum extension = ImageExtensionEnum.Png;
        string displayName = spawnBotName;

        // TODO: incomplete animated list

        switch (spawnBotType.SpawnBotName)
        {
            case "LumKing":
            case "LucySidekick":
            case "FuDogs":
                extension = ImageExtensionEnum.Gif;
                break;
        }

        switch (spawnBotType.SpawnBotName)
        {
            case "LumKing":
            case "LucySidekick":
            case "FuDogs":
                imageName = "AniBot " + imageName;
                break;
            default:
                imageName = "Bot " + imageName;
                break;
        }

        return new()
        {
            Name = spawnBotName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }
}