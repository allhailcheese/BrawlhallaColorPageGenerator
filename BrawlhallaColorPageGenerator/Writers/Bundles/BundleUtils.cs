using System.Collections.Generic;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Bundles;

public static class BundleUtils
{
    public static IEnumerable<StoreType> Bundles(WriterData data) => data.StoreTypes.Stores.Where((s) => s.Type == "Bundle" && !SKIP_BUNDLES.Contains(s.StoreName) && s.IdolBundleDiscount > 0);

    internal static readonly HashSet<string> SKIP_BUNDLES = [
        "---Template---",
        "BHFest25Bundle",
        "Heatwave25Bundle",
        "BackToSchool25Bundle",
        "BackToSchool26EventCenterBundle",
    ];
}