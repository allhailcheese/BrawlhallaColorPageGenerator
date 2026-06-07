using BrawlhallaColorPageGenerator.Objects;
using BrawlhallaLangReader;

namespace BrawlhallaColorPageGenerator;

public sealed partial class WriterData
{
    public required CostumeTypes CostumeTypes { get; init; }
    public required WeaponSkinTypes WeaponSkinTypes { get; init; }
    public required HeroTypes HeroTypes { get; init; }
    public required RuneTypes RuneTypes { get; init; }
    public required CompanionTypes CompanionTypes { get; init; }
    public required StoreTypes StoreTypes { get; init; }
    public required EntitlementTypes EntitlementTypes { get; init; }
    public required ChanceBoxTypes ChanceBoxTypes { get; init; }
    public required ColorSchemeTypes ColorSchemeTypes { get; init; }
    public required LevelTypes LevelTypes { get; init; }
    public required LevelSetTypes LevelSetTypes { get; init; }
    public required GameModeTypes GameModeTypes { get; init; }
    public required ScoringTypes ScoringTypes { get; init; }
    public required ItemTypes ItemTypes { get; init; }
    public required ItemSpawnRuleSetTypes ItemSpawnRuleSetTypes { get; init; }
    public required LangFile LangFile { get; init; }
}