namespace BrawlhallaColorPageGenerator;

public readonly struct ItemNameParams
{
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
    public required string Image { get; init; }
    public required ImageExtensionEnum Extension { get; init; }
    // TODO: add nolink?
}