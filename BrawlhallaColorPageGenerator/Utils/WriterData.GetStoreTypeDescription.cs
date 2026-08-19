using System.Collections.Generic;
using System.Text;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public string GetStoreTypeDescription(StoreType storeType, bool smallItemTag)
    {
        string FormatItemTag(string tag, int year = 0) => "{{ItemTag|" + tag + (smallItemTag ? "|small" : "") + (year > 0 ? "|" + year : "") + "}}";

        int i = 0;
        StringBuilder sb = new("{{Coin|");
        foreach ((string coin, int cost) in GetStoreTypeCost(storeType))
        {
            if (i > 0) sb.Append(" or {{Coin|");
            sb.Append(coin);
            sb.Append('|');
            sb.Append(cost);
            sb.Append("}}");
            ++i;
        }

        if (storeType.SpecialCurrencyType is not null)
        {
            bool useSmallElement = SPECIAL_CURRENCY_WITH_LONG_NAME.Contains(storeType.SpecialCurrencyType);

            sb.Append("<br>");
            if (useSmallElement) sb.Append("<small>");
            sb.Append(storeType.SpecialCurrencyType switch
            {
                "BHFest25" => FormatItemTag("fest", 2025),
                "Heatwave25" => FormatItemTag("summer", 2025),
                "BackToSchool25" => FormatItemTag("school", 2025),
                "Halloween25" => FormatItemTag("halloween", 2025),
                "Anniversary25" => FormatItemTag("anniv", 2025),
                "Christmas25" => FormatItemTag("xmas", 2025),
                "VDay25" => FormatItemTag("love", 2026),
                "StPatricks26" => FormatItemTag("march", 2026),
                "Bloomhalla26" => FormatItemTag("spring", 2026),
                "BHFest26" => FormatItemTag("fest", 2026),
                "Heatwave26" => FormatItemTag("summer", 2026),
                "BackToSchool26" => FormatItemTag("school", 2026),
                _ => " ERROR",
            });
            if (useSmallElement) sb.Append("</small>");
        }
        else if (storeType.EndDateKey is not null)
        {
            sb.Append("<br>");
            sb.Append(storeType.EndDateKey switch
            {
                "StoreType_EndDate_RequiresSkyforged" => "+ Skyforged Variant",
                "StoreType_EndDate_RequiresGoldforged" => "+ Goldforged Variant",
                "StoreType_EndDate_LimitedTime" => storeType.TimedPromotion switch
                {
                    "Valhallentines" => FormatItemTag("valentines"),
                    "StPatricks" => FormatItemTag("march"),
                    "SpringEvent" => FormatItemTag("spring"),
                    "Heatwave" => FormatItemTag("summer"),
                    "Halloween" => FormatItemTag("halloween"),
                    "BackToSchool" => FormatItemTag("school"),
                    "Anniversary" => FormatItemTag("anniversary"),
                    "Christmas" => FormatItemTag("winter"),

                    _ => "Limited time purchase",
                },
                "StoreType_EndDate_Unavailable" => "Limited time purchase",
                _ => "ERROR",
            });
        }

        return sb.ToString();
    }

    public (string, int)[] GetStoreTypeCost(StoreType storeType)
    {
        List<(string, int)> result = [];

        // costs gold
        if (storeType.GoldCost > 0)
        {
            result.Add(("gold", storeType.GoldCost));
        }

        // costs mammoth coins
        if (storeType.IdolCost > 0)
        {
            result.Add(("mammoth", storeType.StoreName switch
            {
                // Purchased as a bundle
                "PaleRider" => 300,
                "MythicWuShang" => 900,
                "MythicNix" => 900,
                "MythicWerewolf" => 900,
                // Normal
                _ => storeType.IdolCost
            }));
        }

        // costs guild gems
        if (storeType.GuildGemsCost > 0)
        {
            result.Add(("goin", storeType.GuildGemsCost));
        }

        // costs glory
        if (storeType.RankedPointsCost > 0)
        {
            result.Add(("glory", storeType.RankedPointsCost));
        }

        // costs tickets
        if (storeType.SpecialCurrencyType is not null)
        {
            string coinStr = "ticket " + storeType.SpecialCurrencyType switch
            {
                "BHFest25" => "fest",
                "Heatwave25" or "Heatwave26" => "heatwave",
                "BackToSchool25" or "BackToSchool26" => "school",
                "Halloween25" => "halloween",
                "Anniversary25" => "anniv",
                "Christmas25" => "xmas",
                "VDay25" => "love",
                "StPatricks26" => "march",
                "Bloomhalla26" => "spring",
                "BHFest26" => "fest26",
                _ => "ERROR",
            };

            int cost = storeType.SpecialCurrencyCost > 0
                ? storeType.SpecialCurrencyCost
                // event-finish (only works with skins. bhfest2026 podium has different value)
                : 1850;

            result.Add((coinStr, cost));
        }

        return [.. result];
    }

    private static readonly HashSet<string> SPECIAL_CURRENCY_WITH_LONG_NAME = [
        "BackToSchool25",
        "BackToSchool26",
        "Halloween25",
        "StPatricks26",
    ];
}