using System.Collections.Generic;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    private const string BUNDLE_EXCLUSIVE_DESCRIPTION = "[[Store Bundles|Bundle]] exclusive";
    private const string REMOVED_FROM_STORE_DESCRIPTION = "Unobtainable.<br>Removed from Store.";

    // for unobtainable or bundle-exclusive stuff
    public readonly static Dictionary<string, string> OVERRIDE_ITEM_DESCRIPTIONS = new()
    {
        #region bundle exclusive
        ["CannonGjallahorn"] = BUNDLE_EXCLUSIVE_DESCRIPTION,
        #endregion
        #region removed from store
        // castlevania
        ["Simon"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["Alucard"] = REMOVED_FROM_STORE_DESCRIPTION,
        // gi joe
        ["SnakeEyes"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["StormShadow"] = REMOVED_FROM_STORE_DESCRIPTION,
        // halo
        ["MasterChief"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["Arbiter"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["PistolSMG"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["PistolNeedler"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["OrbOddball"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["OrbGrifball"] = REMOVED_FROM_STORE_DESCRIPTION,
        ["HammerGravHammer"] = REMOVED_FROM_STORE_DESCRIPTION,
        #endregion
    };
}