using System.Collections.Generic;

namespace BrawlhallaColorPageGenerator;

public static class Utils
{
    // TODO: read from itemTypes.csv?
    public static Dictionary<string, string> BASE_WEAPON_NAME { get; } = new()
    {
        ["Axe"] = "Axe",
        ["Boots"] = "Battle Boots",
        ["Bow"] = "Bow",
        ["Cannon"] = "Cannon",
        ["Chakram"] = "Chakram",
        ["Fists"] = "Gauntlets",
        ["Greatsword"] = "Greatsword",
        ["Hammer"] = "Hammer",
        ["Katar"] = "Katars",
        ["Orb"] = "Orb",
        ["Pistol"] = "Blasters",
        ["RocketLance"] = "Rocket Lance",
        ["Scythe"] = "Scythe",
        ["Spear"] = "Spear",
        ["Sword"] = "Sword",
    };
}