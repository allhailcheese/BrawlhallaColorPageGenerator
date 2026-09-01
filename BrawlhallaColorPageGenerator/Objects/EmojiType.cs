using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class EmojiType
{
    public string EmojiName { get; }
    public string DisplayNameKey { get; }
    public string Category { get; }
    public string AnimRig { get; }

    public EmojiType(XElement element)
    {
        EmojiName = element.Attribute(nameof(EmojiName))!.Value;
        DisplayNameKey = element.Element(nameof(DisplayNameKey))!.Value;
        Category = element.Element(nameof(Category))!.Value;
        AnimRig = element.Element(nameof(AnimRig))!.Value;
    }
}

public sealed class EmojiTypes
{
    public EmojiType[] Emojis { get; }
    public Dictionary<string, EmojiType> EmojisMap { get; }

    public EmojiTypes(string content)
    {
        XElement element = XElement.Parse(content);
        Emojis = [.. element.Elements(nameof(EmojiType)).Select((e) => new EmojiType(e))];
        EmojisMap = Emojis.ToDictionary((t) => t.EmojiName);
    }
}