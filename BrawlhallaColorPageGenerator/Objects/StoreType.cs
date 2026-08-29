using System.Collections.Generic;
using System.IO;
using System.Linq;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class StoreType
{
    public string StoreName { get; }
    public string? DisplayNameKey { get; }

    public int IdolCost { get; }
    public double IdolBundleDiscount { get; }
    public int GoldCost { get; }
    public int RankedPointsCost { get; }
    public int GuildGemsCost { get; }
    public string? SpecialCurrencyType { get; }
    public int SpecialCurrencyCost { get; }

    public string Type { get; }
    public string? Item { get; }
    public string[] ItemList { get; } = [];
    public string? EndDateKey { get; }
    public string? Rarity { get; }
    public string? TimedPromotion { get; }

    public StoreType(SepReader.Row row)
    {
        StoreName = row[nameof(StoreName)].ToString();

        DisplayNameKey = row[nameof(DisplayNameKey)].ToString();
        if (string.IsNullOrWhiteSpace(DisplayNameKey)) DisplayNameKey = null;

        IdolCost = row[nameof(IdolCost)].TryParse<int>() ?? 0;
        IdolBundleDiscount = row[nameof(IdolBundleDiscount)].TryParse<double>() ?? 0;
        GoldCost = row[nameof(GoldCost)].TryParse<int>() ?? 0;
        RankedPointsCost = row[nameof(RankedPointsCost)].TryParse<int>() ?? 0;
        GuildGemsCost = row[nameof(GuildGemsCost)].TryParse<int>() ?? 0;

        SpecialCurrencyType = row[nameof(SpecialCurrencyType)].ToString();
        if (string.IsNullOrWhiteSpace(SpecialCurrencyType)) SpecialCurrencyType = null;
        SpecialCurrencyCost = row[nameof(SpecialCurrencyCost)].TryParse<int>() ?? 0;

        Type = row[nameof(Type)].ToString();

        Item = row[nameof(Item)].ToString();
        if (string.IsNullOrWhiteSpace(Item)) Item = null;
        ItemList = row[nameof(ItemList)].ToString().Split(',');

        EndDateKey = row[nameof(EndDateKey)].ToString();
        if (string.IsNullOrWhiteSpace(EndDateKey)) EndDateKey = null;

        Rarity = row[nameof(Rarity)].ToString();
        if (string.IsNullOrWhiteSpace(Rarity)) Rarity = null;

        TimedPromotion = row[nameof(TimedPromotion)].ToString();
        if (string.IsNullOrWhiteSpace(TimedPromotion)) TimedPromotion = null;
    }
}

public sealed class StoreTypes
{
    public StoreType[] Stores { get; }
    public Dictionary<string, StoreType> StoresMap { get; }
    public Dictionary<(string ItemType, string Item), StoreType> ItemToStoreType { get; }

    public StoreTypes(string content)
    {
        using StringReader textReader = new(content);
        textReader.ReadLine(); // skip first line bullshit
        SepReaderOptions sepReaderOptions = Sep.New(',').Reader((opts) =>
        {
            return opts with
            {
                DisableColCountCheck = true,
                Unescape = true,
            };
        });
        using SepReader csvReader = sepReaderOptions.From(textReader);
        Stores = [.. csvReader.Enumerate((row) => new StoreType(row))];
        StoresMap = Stores.ToDictionary((s) => s.StoreName);
        ItemToStoreType = Stores.Where((s) => s.Item is not null).ToDictionary((s) => (s.Type, s.Item!));
    }
}