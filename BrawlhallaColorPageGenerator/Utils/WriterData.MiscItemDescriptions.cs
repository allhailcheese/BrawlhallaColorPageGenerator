using System.Collections.Generic;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    private const string VSR_ICON = "[[File:Icon Viewership.png|16px|link=]]";
    private const string TD_ICON = "[[File:Icon Twitch.png|16px|link=]]";

    private const string TD_DESCRIPTION = $"{TD_ICON} [[Twitch Drops|''Future Twitch Drop'']]";
    private const string GIFTS_OF_ASGARD_DESCRIPTION = "[[The Gifts of Asgard]]";
    private const string BUNDLE_EXCLUSIVE_DESCRIPTION = "[[Store Bundles|Bundle]] exclusive";
    private const string REMOVED_FROM_STORE_DESCRIPTION = "Unobtainable.<br>Removed from Store.";
    private const string UNOBTAINABLE_DESCRIPTION = "''Unobtainable''";

    // TODO: show the track name instead of the generic text?
    private static string VSR(string track) => VSR_ICON + " {{VSR|" + track + "|Viewership Rewards}}";
    private static string TD(string track) => TD_ICON + " {{TD|" + track + "|Twitch Drops}}";

    // for stuff that can't be automated: vsr, twitch drops, bundle exclusives, and battlepass
    public readonly static Dictionary<string, string> MISC_ITEM_DESCRIPTIONS = new()
    {
        #region exclusive weapon skins
        // charity
        ["SpearCharity2020"] = VSR("Charity Stream #2"),
        ["OrbCharity2021"] = VSR("Charity Stream #3"),
        ["BowCharity2021"] = VSR("Charity Stream #4"),
        ["RocketLanceCharity2021"] = VSR("Charity Streams #5"),
        ["GreatswordCharity2022"] = VSR("Charity Stream #6"),
        ["CannonCharity2021"] = VSR("Charity Stream #7"),
        ["KatarCharity2022"] = VSR("Charity Stream #8"),
        ["ScytheCharity"] = VSR("Charity Stream #9"),
        ["AxeCharity"] = VSR("Charity Stream #10"),
        ["HammerCharity"] = VSR("Charity Stream #11"),
        // neo city
        ["GreatswordEsports2026"] = TD("Winter Doubles Championship"),
        ["SwordEsports2026"] = TD("Winter Singles Championship"),
        ["HammerEsports2026"] = TD("Eternal Sports Triples Championship"),
        ["CannonEsports2026"] = TD("Spring Singles"),
        ["KatarEsports2026"] = TD("Spring Doubles"),
        ["SpearEsports2026"] = TD("Midseason 2026 Whitelist"),
        ["ScytheEsports2026"] = TD("Midseason 2026 Whitelist"),
        ["PistolEsports2026"] = TD("Eternal Sports 2026"),
        ["AxeEsports2026"] = TD_DESCRIPTION,
        ["OrbEsports2026"] = TD_DESCRIPTION,
        ["BootsEsports2026"] = TD_DESCRIPTION,
        // magical girl
        ["GreatswordMagicalGirl"] = TD("Summer 2026 Whitelist"),
        // gifts of asgard. TODO: give all of these a proper description
        ["SwordSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["AxeSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["PistolSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["FistsSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["KatarSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["ScytheSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["SpearSocial"] = GIFTS_OF_ASGARD_DESCRIPTION,
        ["BowSocial"] = TD("Brawlhalla Fest 2026 Whitelist"),
        ["ChakramSocial"] = TD("Summer 2026 Whitelist"),
        ["OrbSocial"] = TD_DESCRIPTION,
        #endregion
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
        #region unobtainable
        ["AxeGears"] = UNOBTAINABLE_DESCRIPTION,
        ["AxeFlame"] = UNOBTAINABLE_DESCRIPTION,
        ["PistolValkyrie"] = UNOBTAINABLE_DESCRIPTION,
        ["PistolGear"] = UNOBTAINABLE_DESCRIPTION,
        ["PistolSnakeGod"] = UNOBTAINABLE_DESCRIPTION,
        ["CannonMark1"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerBigSteam"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerWitch"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerScrap"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerHighwayman"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerTitanium"] = UNOBTAINABLE_DESCRIPTION,
        ["HammerNinja"] = UNOBTAINABLE_DESCRIPTION,
        ["KatarEdge"] = UNOBTAINABLE_DESCRIPTION,
        ["KatarFuture"] = UNOBTAINABLE_DESCRIPTION,
        ["RocketLanceCyber"] = UNOBTAINABLE_DESCRIPTION,
        ["SpearGear"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordNapoleon"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordMech"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordWitch"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordBrass"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordValkyrie"] = UNOBTAINABLE_DESCRIPTION,
        ["SwordSalvage"] = UNOBTAINABLE_DESCRIPTION,
        #endregion
        #region battlepass
        // bp 1
        // skins
        ["Demon01"] = "{{BPReward|Season One|gold|Tier 1}}",
        ["Demon02"] = "{{BPReward|Season One|gold|Tier 1}}",
        ["Demon03"] = "{{BPReward|Season One|gold|Tier 1}}",
        ["Fiend"] = "{{BPReward|Season One|gold|Tier 23}}",
        ["BPZariel"] = "{{BPReward|Season One|gold|Tier 47}}",
        ["DemonNinja"] = "{{BPReward|Season One|gold|Tier 71}}",
        ["DemonQueen"] = "{{BPReward|Season One|gold|Tier 85}}",
        // weapon skins
        ["CannonBattlePassSet"] = "{{BPReward|Season One|free|Tier 6}}",
        ["KatarBattlePassSet"] = "{{BPReward|Season One|gold|Tier 9}}",
        ["SwordBattlePassSet"] = "{{BPReward|Season One|gold|Tier 15}}",
        ["RocketLanceBattlePassSet"] = "{{BPReward|Season One|gold|Tier 19}}",
        ["OrbBattlePassSet"] = "{{BPReward|Season One|gold|Tier 25}}",
        ["AxeBattlePassSet"] = "{{BPReward|Season One|gold|Tier 29}}",
        ["SpearBattlePassSet"] = "{{BPReward|Season One|gold|Tier 37}}",
        ["HammerBattlePassSet"] = "{{BPReward|Season One|gold|Tier 41}}",
        ["PistolBattlePassSet"] = "{{BPReward|Season One|gold|Tier 50}}",
        ["BowBattlePassSet"] = "{{BPReward|Season One|gold|Tier 57}}",
        ["FistsBattlePassSet"] = "{{BPReward|Season One|gold|Tier 65}}",
        ["ScytheBattlePassSet"] = "{{BPReward|Season One|gold|Tier 79}}",
        ["GreatswordBattlePassSet"] = "{{BPReward|Season One Classic|gold|Tier 81}}",

        // bp 2
        // skins
        ["Synth01"] = "{{BPReward|Season Two|gold|Tier 1}}",
        ["Synth02"] = "{{BPReward|Season Two|gold|Tier 1}}",
        ["Synth03"] = "{{BPReward|Season Two|gold|Tier 1}}",
        ["SynthIsaiah"] = "{{BPReward|Season Two|gold|Tier 23}}",
        ["SynthElf"] = "{{BPReward|Season Two|gold|Tier 47}}",
        ["SynthYumi"] = "{{BPReward|Season Two|gold|Tier 71}}",
        ["EpicNix"] = "{{BPReward|Season Two|gold|Tier 85}}",
        // weapon skins
        ["BowBattlePassSet2"] = "{{BPReward|Season Two|free|Tier 6}}",
        ["HammerBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 9}}",
        ["AxeBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 15}}",
        ["SwordBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 19}}",
        ["KatarBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 25}}",
        ["RocketLanceBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 29}}",
        ["ScytheBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 37}}",
        ["PistolBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 41}}",
        ["FistsBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 50}}",
        ["CannonBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 57}}",
        ["BootsBattlepassSet2"] = "{{BPReward|Season Two Classic|free|Tier 62}}",
        ["OrbBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 65}}",
        ["SpearBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 79}}",
        ["GreatswordBattlePassSet2"] = "{{BPReward|Season Two|gold|Tier 81}}",

        // bp 3
        // skins
        ["MakoProgression01"] = "{{BPReward|Season Three|gold|Tier 1}}",
        ["MakoProgression02"] = "{{BPReward|Season Three|gold|Tier 1}}",
        ["MakoProgression03"] = "{{BPReward|Season Three|gold|Tier 1}}",
        ["ThorBPJotun"] = "{{BPReward|Season Three|gold|Tier 23}}",
        ["DwarfBPJotun"] = "{{BPReward|Season Three|gold|Tier 47}}",
        ["YggdrasilBodvar"] = "{{BPReward|Season Three|gold|Tier 71}}",
        ["EpicBrynn"] = "{{BPReward|Season Three|gold|Tier 85}}",
        // weapon skins
        ["KatarBPJotun"] = "{{BPReward|Season Three|free|Tier 6}}",
        ["OrbBPJotun"] = "{{BPReward|Season Three|gold|Tier 9}}",
        ["SpearBPJotun"] = "{{BPReward|Season Three|gold|Tier 15}}",
        ["RocketLanceBPJotun"] = "{{BPReward|Season Three|gold|Tier 21}}",
        ["BowBPJotun"] = "{{BPReward|Season Three|gold|Tier 25}}",
        ["SwordBPJotun"] = "{{BPReward|Season Three|gold|Tier 29}}",
        ["AxeBPJotun"] = "{{BPReward|Season Three|gold|Tier 37}}",
        ["CannonBPJotun"] = "{{BPReward|Season Three|gold|Tier 41}}",
        ["PistolBPJotun"] = "{{BPReward|Season Three|gold|Tier 53}}",
        ["GreatswordBPJotun"] = "{{BPReward|Season Three|gold|Tier 57}}",
        ["FistsBPJotun"] = "{{BPReward|Season Three|gold|Tier 69|small}}<br>{{BPReward|Season Three Classic|free|Tier 66|small}}",
        ["BootsBPJotun"] = "{{BPReward|Season Three Classic|gold|Tier 69}}",
        ["HammerBPJotun"] = "{{BPReward|Season Three|gold|Tier 77}}",
        ["ScytheBPJotun"] = "{{BPReward|Season Three|gold|Tier 81}}",

        // bp 4
        // skins
        ["WolfMonster01"] = "{{BPReward|Season Four|gold|Tier 1}}",
        ["WolfMonster02"] = "{{BPReward|Season Four|gold|Tier 1}}",
        ["WolfMonster03"] = "{{BPReward|Season Four|gold|Tier 1}}",
        ["Pyromancer"] = "{{BPReward|Season Four|gold|Tier 23}}",
        ["CultistNai"] = "{{BPReward|Season Four|gold|Tier 47}}",
        ["BP4Azoth"] = "{{BPReward|Season Four|gold|Tier 71}}",
        ["EpicDiana"] = "{{BPReward|Season Four|gold|Tier 85}}",
        // weapon skins
        ["FistsBP4Gauntlet"] = "{{BPReward|Season Four|free|Tier 6}}",
        ["CannonBP4Cannon"] = "{{BPReward|Season Four|gold|Tier 9}}",
        ["HammerBP4Hammer"] = "{{BPReward|Season Four|gold|Tier 15}}",
        ["AxeBP4Axe"] = "{{BPReward|Season Four|gold|Tier 21}}",
        ["PistolBP4Blaster"] = "{{BPReward|Season Four|gold|Tier 25}}",
        ["BowBP4Bow"] = "{{BPReward|Season Four|gold|Tier 29}}",
        ["RocketLanceBP4RocketLance"] = "{{BPReward|Season Four|gold|Tier 37}}",
        ["OrbBP4Orb"] = "{{BPReward|Season Four|gold|Tier 41}}",
        ["ScytheBP4Scythe"] = "{{BPReward|Season Four|gold|Tier 53}}",
        ["KatarBP4Katar"] = "{{BPReward|Season Four|gold|Tier 57}}",
        ["GreatswordBP4GreatSword"] = "{{small|{{BPReward|Season Four|gold|Tier 69}}<br>{{BPReward|Season Four Classic|free|Tier 66}}}}",
        ["BootsBP4"] = "{{BPReward|Season Four Classic|gold|Tier 69}}",
        ["SwordBP4Sword"] = "{{BPReward|Season Four|gold|Tier 77}}",
        ["SpearBP4Spear"] = "{{BPReward|Season Four|gold|Tier 81}}",

        // bp 5
        // skins
        ["BP5DualArt"] = "{{BPReward|Season Five|gold|Tier 1}}",
        ["BP5DualArt02"] = "{{BPReward|Season Five|gold|Tier 1}}",
        ["BP5DualArt03"] = "{{BPReward|Season Five|gold|Tier 1}}",
        ["BP5StarAda"] = "{{BPReward|Season Five|gold|Tier 23}}",
        ["BP5SpaceCadet"] = "{{BPReward|Season Five|gold|Tier 47}}",
        ["BP5GalaxyBeast"] = "{{BPReward|Season Five|gold|Tier 71}}",
        ["EpicOrion"] = "{{BPReward|Season Five|gold|Tier 85}}",
        // weapon skins
        ["PistolBP5Blaster"] = "{{BPReward|Season Five|free|Tier 6}}",
        ["KatarBP5Katar"] = "{{BPReward|Season Five|gold|Tier 9}}",
        ["SwordBP5Sword"] = "{{BPReward|Season Five|gold|Tier 15}}",
        ["GreatswordBP5Greatsword"] = "{{BPReward|Season Five|gold|Tier 21}}",
        ["FistsBP5Fists"] = "{{BPReward|Season Five|gold|Tier 25}}",
        ["RocketLanceBP5RocketLance"] = "{{BPReward|Season Five|gold|Tier 29}}",
        ["ScytheBP5Scythe"] = "{{BPReward|Season Five|gold|Tier 37}}",
        ["CannonBP5Cannon"] = "{{BPReward|Season Five|gold|Tier 41}}",
        ["BowBP5Bow"] = "{{BPReward|Season Five|gold|Tier 53}}",
        ["SpearBP5Spear"] = "<span style=\"font-size:76%\">{{BPReward|Season Five|gold|Tier 69}}<br>{{BPReward|Season Five Classic|free|Tier 65}}</span>",
        ["BPC5Boots"] = "{{BPReward|Season Five Classic|gold|Tier 69}}",
        ["OrbBP5Orb"] = "{{BPReward|Season Five|gold|Tier 57}}",
        ["AxeBP5Axe"] = "{{BPReward|Season Five|gold|Tier 77}}",
        ["HammerBP5Hammer"] = "{{BPReward|Season Five|gold|Tier 81}}",

        // bp 6
        // skins
        ["ElderDragon1"] = "{{BPReward|Season Six|gold|Tier 1}}",
        ["ElderDragon2"] = "{{BPReward|Season Six|gold|Tier 1}}",
        ["ElderDragon3"] = "{{BPReward|Season Six|gold|Tier 1}}",
        ["AutumnFairy"] = "{{BPReward|Season Six|gold|Tier 23}}",
        ["MushroninDusk"] = "{{BPReward|Season Six|gold|Tier 47}}",
        ["MagicalGirlScarlet"] = "{{BPReward|Season Six|gold|Tier 71}}",
        ["EpicEmber"] = "{{BPReward|Season Six|gold|Tier 85}}",
        // weapon skins
        ["FistsBP6weaponset"] = "{{BPReward|Season Six|free|Tier 6}}",
        ["PistolBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 9}}",
        ["RocketLanceBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 15}}",
        ["SwordBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 21}}",
        ["KatarBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 25}}",
        ["BowBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 29}}",
        ["BootsBP6"] = "{{BPReward|Season Six Classic|gold|Tier 35}}",
        ["AxeBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 37}}",
        ["OrbBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 41}}",
        ["ScytheBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 53}}",
        ["CannonBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 57}}",
        ["HammerBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 69}}",
        ["ChakramBP6"] = "{{BPReward|Season Six Classic|gold|Tier 73}}",
        ["GreatswordBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 77}}",
        ["SpearBP6weaponset"] = "{{BPReward|Season Six|gold|Tier 81}}",

        // bp 7
        // skins
        ["T1Paladin"] = "{{BPReward|Season Seven|gold|Tier 1}}",
        ["T2Paladin"] = "{{BPReward|Season Seven|gold|Tier 1}}",
        ["T3Paladin"] = "{{BPReward|Season Seven|gold|Tier 1}}",
        ["HuginBard"] = "{{BPReward|Season Seven|gold|Tier 23}}",
        ["BeastmasterSidra"] = "{{BPReward|Season Seven|gold|Tier 47}}",
        ["ClericKaya"] = "{{BPReward|Season Seven|gold|Tier 71}}",
        ["EpicWarlock"] = "{{BPReward|Season Seven|gold|Tier 85}}",
        // weapon skins
        ["HammerBP7"] = "{{BPReward|Season Seven|free|Tier 6}}",
        ["BootsBP7"] = "{{BPReward|Season Seven|gold|Tier 9}}",
        ["CannonBP7"] = "{{BPReward|Season Seven|gold|Tier 15}}",
        ["PistolBP7"] = "{{BPReward|Season Seven|gold|Tier 17}}",
        ["GreatswordBP7"] = "<span style=\"font-size:75%\">{{BPReward|Season Seven|gold|Tier 21}}<br>{{BPReward|Season Seven Classic|free|Tier 20}}</span>",
        ["ChakramBP7"] = "{{BPReward|Season Seven Classic|gold|Tier 21}}",
        ["RocketLanceBP7"] = "{{BPReward|Season Seven|gold|Tier 29}}",
        ["OrbBP7"] = "{{BPReward|Season Seven|gold|Tier 25}}",
        ["BowBP7"] = "{{BPReward|Season Seven|gold|Tier 37}}",
        ["KatarBP7"] = "{{BPReward|Season Seven|gold|Tier 41}}",
        ["AxeBP7"] = "{{BPReward|Season Seven|gold|Tier 53}}",
        ["FistsBP7"] = "{{BPReward|Season Seven|gold|Tier 57}}",
        ["ScytheBP7"] = "{{BPReward|Season Seven|gold|Tier 69}}",
        ["SpearBP7"] = "{{BPReward|Season Seven|gold|Tier 77}}",
        ["SwordBP7"] = "{{BPReward|Season Seven|gold|Tier 81}}",

        // bp 8
        // skins
        ["TerminusLuchador01"] = "{{BPReward|Season Eight|gold|Tier 1}}",
        ["TerminusLuchador02"] = "{{BPReward|Season Eight|gold|Tier 1}}",
        ["TerminusLuchador03"] = "{{BPReward|Season Eight|gold|Tier 1}}",
        ["TerminusBrute"] = "{{BPReward|Season Eight|gold|Tier 20}}",
        ["TerminusPetra"] = "{{BPReward|Season Eight|gold|Tier 35}}",
        ["TerminusNinja"] = "{{BPReward|Season Eight|gold|Tier 50}}",
        ["TerminusValk"] = "{{BPReward|Season Eight|gold|Tier 65}}",
        ["EpicMordex"] = "{{BPReward|Season Eight|gold|Tier 85}}",
        // weapon skins
        ["OrbTerminus"] = "{{BPReward|Season Eight|free|Tier 4}}",
        ["AxeTerminus"] = "{{BPReward|Season Eight|gold|Tier 13}}",
        ["ChakramTerminus"] = "{{BPReward|Season Eight Classic|gold|Tier 14}}",
        ["CannonTerminus"] = "{{BPReward|Season Eight|gold|Tier 18}}",
        ["BowTerminus"] = "{{BPReward|Season Eight|gold|Tier 26}}",
        ["FistsTerminus"] = "{{BPReward|Season Eight|gold|Tier 33}}",
        ["PistolTerminus"] = "{{BPReward|Season Eight|gold|Tier 37}}",
        ["GreatswordTerminus"] = "{{BPReward|Season Eight|gold|Tier 58}}",
        ["BootsTerminus"] = "{{BPReward|Season Eight|gold|Tier 63}}",
        ["HammerTerminus"] = "{{BPReward|Season Eight|gold|Tier 67}}",
        ["SwordTerminus"] = "{{BPReward|Season Eight|gold|Tier 73}}",
        ["KatarTerminus"] = "{{BPReward|Season Eight|free|Tier 78}}",
        ["ScytheTerminus"] = "{{BPReward|Season Eight|gold|Tier 83}}",

        // bp 9
        // skins
        ["Guardian01"] = "{{BPReward|Season Nine|gold|Tier 1}}",
        ["Guardian02"] = "{{BPReward|Season Nine|gold|Tier 1}}",
        ["MonkGuardian03"] = "{{BPReward|Season Nine|gold|Tier 1}}",
        ["WarRoland"] = "{{BPReward|Season Nine|gold|Tier 20}}",
        ["FamineBarazza"] = "{{BPReward|Season Nine|gold|Tier 35}}",
        ["PestBountyHunter"] = "{{BPReward|Season Nine|gold|Tier 50}}",
        ["BirdBardDeath"] = "{{BPReward|Season Nine|gold|Tier 65}}",
        ["EpicRaptor"] = "{{BPReward|Season Nine|gold|Tier 85}}",
        // weapon skins
        ["HammerDeathrider"] = "{{BPReward|Season Nine|free|Tier 4}}",
        ["SwordDeathrider"] = "{{BPReward|Season Nine|gold|Tier 13}}",
        ["AxeDeathrider"] = "{{BPReward|Season Nine|gold|Tier 18}}",
        ["RocketLanceDeathrider"] = "{{BPReward|Season Nine|gold|Tier 26}}",
        ["PistolDeathrider"] = "{{BPReward|Season Nine|gold|Tier 33}}",
        ["OrbDeathrider"] = "{{BPReward|Season Nine|gold|Tier 37}}",
        ["SpearDeathrider"] = "{{BPReward|Season Nine|gold|Tier 58}}",
        ["BootsDeathrider"] = "{{BPReward|Season Nine|gold|Tier 63}}",
        ["KatarDeathrider"] = "{{BPReward|Season Nine|gold|Tier 67}}",
        ["CannonDeathrider"] = "{{BPReward|Season Nine|gold|Tier 73}}",
        ["FistsDeathrider"] = "{{BPReward|Season Nine|free|Tier 78}}",
        ["GreatSwordDeathrider"] = "{{BPReward|Season Nine|gold|Tier 83}}",

        // bp 10
        // skins
        ["MagicalTeros01"] = "{{BPReward|Season Ten|gold|Tier 1}}",
        ["MagicalTeros02"] = "{{BPReward|Season Ten|gold|Tier 1}}",
        ["MagicalTeros03"] = "{{BPReward|Season Ten|gold|Tier 1}}",
        ["TuxedoThief"] = "{{BPReward|Season Ten|gold|Tier 20}}",
        ["SpellwitchMagi"] = "{{BPReward|Season Ten|gold|Tier 35}}",
        ["MechaArmor"] = "{{BPReward|Season Ten|gold|Tier 50}}",
        ["SpeedsterMagi"] = "{{BPReward|Season Ten|gold|Tier 65}}",
        ["EgyptianShoujo"] = "{{BPReward|Season Ten|gold|Tier 85}}",
        // weapon skins
        ["KatarBP10Mecha"] = "{{BPReward|Season Ten|free|Tier 4}}",
        ["CannonBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 13}}",
        ["RocketlanceBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 18}}",
        ["AxeBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 26}}",
        ["BootsBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 33}}",
        ["PistolBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 37}}",
        ["BowBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 58}}",
        ["OrbBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 63}}",
        ["ScytheBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 67}}",
        ["SpearBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 73}}",
        ["FistsBP10Mecha"] = "{{BPReward|Season Ten|free|Tier 78}}",
        ["SwordBP10Mecha"] = "{{BPReward|Season Ten|gold|Tier 83}}",

        // bp 11
        // skins
        ["BP11Azoth01"] = "{{BPReward|Season Eleven|gold|Tier 1}}",
        ["BP11Azoth02"] = "{{BPReward|Season Eleven|gold|Tier 1}}",
        ["BP11Azoth03"] = "{{BPReward|Season Eleven|gold|Tier 1}}",
        ["CatBP11"] = "{{BPReward|Season Eleven|gold|Tier 20}}",
        ["GolemBP11"] = "{{BPReward|Season Eleven|gold|Tier 35}}",
        ["LuchaPartnerAztec"] = "{{BPReward|Season Eleven|gold|Tier 50}}",
        ["LokiCoatl"] = "{{BPReward|Season Eleven|gold|Tier 65}}",
        ["EpicWitch"] = "{{BPReward|Season Eleven|gold|Tier 85}}",
        // weapon skins
        ["PistolCatBP11"] = "{{BPReward|Season Eleven|free|Tier 4}}",
        ["BowCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 13}}",
        ["GreatswordCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 18}}",
        ["CannonCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 26}}",
        ["RocketLanceCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 33}}",
        ["OrbCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 37}}",
        ["FistsCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 45}}",
        ["BootsCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 58}}",
        ["HammerCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 63}}",
        ["SpearCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 67}}",
        ["ChakramCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 73}}",
        ["AxeCatBP11"] = "{{BPReward|Season Eleven|free|Tier 78}}",
        ["ScytheCatBP11"] = "{{BPReward|Season Eleven|gold|Tier 83}}",

        // bp 12
        // skins
        ["ShinobiBP1201"] = "{{BPReward|Season Twelve|gold|Intro Tier 1}}",
        ["ShinobiBP1202"] = "{{BPReward|Season Twelve|gold|Intro Tier 1}}",
        ["ShinobiBP1203"] = "{{BPReward|Season Twelve|gold|Intro Tier 1}}",
        ["PetraBP12"] = "{{BPReward|Season Twelve|gold|Past Tier 23}}",
        ["TechnoNinjaBP12"] = "{{BPReward|Season Twelve|gold|Present Tier 23}}",
        ["SamuraiBP12"] = "{{BPReward|Season Twelve|gold|Future Tier 23}}",
        ["BladeDancerOni"] = "{{BPReward|Season Twelve|gold|Final Tier 7}}",
        // weapon skins
        ["KatarBP12"] = "{{BPReward|Season Twelve|free|Intro Tier 5}}",
        ["RocketLanceBP12"] = "{{BPReward|Season Twelve|free|Past Tier 4}}",
        ["HammerBP12"] = "{{BPReward|Season Twelve|gold|Past Tier 8}}",
        ["BootsBP12"] = "{{BPReward|Season Twelve|gold|Past Tier 14}}",
        ["ChakramBP12"] = "{{BPReward|Season Twelve|gold|Past Tier 22}}",
        ["BowBP12"] = "{{BPReward|Season Twelve|free|Past Tier 23}}",
        ["PistolBP12"] = "{{BPReward|Season Twelve|free|Present Tier 4}}",
        ["AxeBP12"] = "{{BPReward|Season Twelve|gold|Present Tier 6}}",
        ["GreatswordBP12"] = "{{BPReward|Season Twelve|free|Present Tier 23}}",
        ["CannonBP12"] = "{{BPReward|Season Twelve|free|Future Tier 1}}",
        ["SpearBP12"] = "{{BPReward|Season Twelve|gold|Future Tier 4}}",
        ["ScytheBP12"] = "{{BPReward|Season Twelve|gold|Future Tier 21}}",
        ["SwordBP12"] = "{{BPReward|Season Twelve|free|Future Tier 23}}",

        // bp 13
        // skins
        ["ImugiDragon1"] = "{{BPReward|Season Thirteen|gold|Intro Tier 1}}",
        ["ImugiDragon2"] = "{{BPReward|Season Thirteen|gold|Intro Tier 1}}",
        ["ImugiDragon3"] = "{{BPReward|Season Thirteen|gold|Intro Tier 1}}",
        ["WuxiaDragon"] = "{{BPReward|Season Thirteen|gold|Light Tier 23}}",
        ["NinetailsDragon"] = "{{BPReward|Season Thirteen|gold|Twilight Tier 23}}",
        ["GargoyleDragon"] = "{{BPReward|Season Thirteen|gold|Dark Tier 23}}",
        ["EpicDragon"] = "{{BPReward|Season Thirteen|gold|Final Tier 7}}",
        // weapon skins
        ["GreatswordBP13"] = "{{BPReward|Season Thirteen|free|Intro Tier 5}}",
        ["BowBP13"] = "{{BPReward|Season Thirteen|free|Light Tier 4}}",
        ["PistolBP13"] = "{{BPReward|Season Thirteen|gold|Light Tier 8}}",
        ["RocketLanceBP13"] = "{{BPReward|Season Thirteen|gold|Light Tier 14}}",
        ["AxeBP13"] = "{{BPReward|Season Thirteen|gold|Light Tier 22}}",
        ["OrbBP13"] = "{{BPReward|Season Thirteen|free|Light Tier 23}}",
        ["ChakramBP13"] = "{{BPReward|Season Thirteen|free|Twilight Tier 4}}",
        ["ScytheBP13"] = "{{BPReward|Season Thirteen|gold|Twilight Tier 6}}",
        ["HammerBP13"] = "{{BPReward|Season Thirteen|free|Twilight Tier 23}}",
        ["BootsBP13"] = "{{BPReward|Season Thirteen|free|Dark Tier 1}}",
        ["SwordBP13"] = "{{BPReward|Season Thirteen|gold|Dark Tier 4}}",
        ["SpearBP13"] = "{{BPReward|Season Thirteen|gold|Dark Tier 21}}",
        ["KatarBP13"] = "{{BPReward|Season Thirteen|free|Dark Tier 23}}",
        #endregion
        #region misc
        ["Yetee"] = "Bonus with purchase Brawlhalla merchandise from [https://theyetee.com/collections/brawlhalla The Yetee]",
        ["SteelSeries"] = "SteelSeries 2022 reward",
        ["OrbLogitech"] = "Tournament Reward",
        #endregion
    };
}