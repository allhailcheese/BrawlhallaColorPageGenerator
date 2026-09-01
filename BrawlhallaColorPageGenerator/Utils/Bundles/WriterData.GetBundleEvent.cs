namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    private (string @event, int year) GetBundleEventParams(string bundleName)
    {
        switch (bundleName)
        {
            case "VdayOrionSmallBundle":
            case "VdayAxeBundle":
            case "VdayMegaBundle":
                return ("love", 2025);
            case "MegaLuckyBundle":
            case "DullahanJiroBundle":
            case "CelticKojiBundle":
                return ("march", 2025);
            case "MegaBloomhallaBundle":
            case "RenoBloomhallaBundle":
            case "MagyarBloomhallaBundle":
                return ("spring", 2025);
            case "BHFest25ValhallaDevilsBundle":
                return ("fest", 2025);
            case "MegaHeatwaveBundle":
            case "HeatwaveDianaBundle":
            case "HeatwaveVraxxNaiBundle":
                return ("summer", 2025);
            case "B2SchoolBoysBundle":
            case "B2SchoolMirageBundle":
            case "B2SchoolBrynnBundle":
                return ("school", 2025);
            case "MegaBundleHalloween":
            case "WitchBundleHalloween":
            case "SkeletonBundleHalloween":
                return ("halloween", 2025);
            case "AnniversaryKorBundle":
            case "AnniversaryNixBundle":
                return ("anniv", 2025);
            case "MegaBundleHoliday":
            case "HolidayArtemisBundle":
            case "HolidayCrossBundle":
                return ("winter", 2025);
            case "Valentines26MegaBundle":
            case "VdayDuskBundle":
            case "VdayKorBundle":
            case "VdayHattoriBundle":
                return ("love", 2026);
            case "StPatricks26MegaBundle":
            case "StPatricksUlgrimBundle":
            case "StPatricksSkinsBundle":
                return ("march", 2026);
            case "MegaBloomhalla26Bundle":
            case "GardenBloomhallaBundle":
                return ("spring", 2026);
            case "BHFest26HuntersBundle":
            case "BHFest26MegaBundle":
                return ("fest", 2026);
            case "Heatwave26MegaBundle":
            case "Heatwave26RagnirBundle":
            case "Heatwave26SkinBundle":
                return ("summer", 2026);
            case "BackToSchool26MegaBundle":
            case "BackToSchool26SpiritBundle":
            case "BackToSchool26FacultyBundle":
                return ("school", 2026);
            default:
                return ("UNKNOWN", 0);
        }
    }

    public string GetBundleEvent(string bundleName)
    {
        (string @event, int year) = GetBundleEventParams(bundleName);
        return "{{Events|" + @event + "|" + year + "}}";
    }
}