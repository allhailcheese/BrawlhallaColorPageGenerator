using System.Collections.Generic;
using System.IO;
using System.Linq;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class AvatarType
{
    public string AvatarName { get; }
    public string? DisplayNameKey { get; }

    public AvatarType(SepReader.Row row)
    {
        AvatarName = row[nameof(AvatarName)].ToString();

        DisplayNameKey = row[nameof(DisplayNameKey)].ToString();
        if (string.IsNullOrWhiteSpace(DisplayNameKey)) DisplayNameKey = null;
    }
}

public sealed class AvatarTypes
{
    public AvatarType[] Avatars { get; }
    public Dictionary<string, AvatarType> AvatarsMap { get; }

    public AvatarTypes(string content)
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
        Avatars = [.. csvReader.Enumerate((row) => new AvatarType(row))];
        AvatarsMap = Avatars.ToDictionary((item) => item.AvatarName);
    }
}