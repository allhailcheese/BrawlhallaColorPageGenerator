namespace BrawlhallaColorPageGenerator;

public readonly struct ItemDescription
{
    public required string Description { get; init; }
    public required DescriptionTypeEnum DescriptionType { get; init; }
    public required RarityEnum Rarity { get; init; }
}