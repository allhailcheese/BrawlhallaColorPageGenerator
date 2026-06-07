using System.Collections.Generic;
using System.IO;
using System.Linq;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class ItemType
{
    public string ItemName { get; }
    public string? DisplayNameKey { get; }

    public ItemType(SepReader.Row row)
    {
        ItemName = row[nameof(ItemName)].ToString();

        DisplayNameKey = row[nameof(DisplayNameKey)].ToString();
        if (string.IsNullOrWhiteSpace(DisplayNameKey)) DisplayNameKey = null;
    }
}

public sealed class ItemTypes
{
    public ItemType[] Items { get; }
    public Dictionary<string, ItemType> ItemsMap { get; }

    public ItemTypes(string content)
    {
        using StringReader textReader = new(content);
        textReader.ReadLine(); // skip first line bullshit
        SepReaderOptions sepReaderOptions = Sep.New(',').Reader((opts) =>
        {
            return opts with
            {
                DisableColCountCheck = true,
            };
        });
        using SepReader csvReader = sepReaderOptions.From(textReader);
        Items = [.. csvReader.Enumerate((row) => new ItemType(row))];
        ItemsMap = Items.ToDictionary((item) => item.ItemName);
    }
}