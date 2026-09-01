using System;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    public ItemNameParams GetEmojiNameParams(EmojiType emojiType)
    {
        string displayNameKey = emojiType.DisplayNameKey;

        string emojiName = LangFile.Entries[displayNameKey].Replace('’', '\'');
        string imageName = emojiName;
        ImageExtensionEnum extension = emojiType.AnimRig switch
        {
            "a__AnimationEmoji1" => ImageExtensionEnum.Png,
            "a__AnimationEmoji8" => emojiType.EmojiName switch
            {
                "OneMoreBaoBao" => ImageExtensionEnum.Webp,
                _ => ImageExtensionEnum.Gif,
            },
            _ => throw new ArgumentException($"Unhandled emoji AnimRig {emojiType.AnimRig}"),
        };
        string displayName = emojiName;

        switch (emojiType.EmojiName)
        {
            case "BloomhallaXullThumbsDown":
                emojiName = displayName = "Elvenhollow Thumbs Down";
                imageName = "Elvenhollow";
                break;
            case "OneMoreBaoBao":
                break;
            default:
                string[] expectedSuffixes = emojiType.Category switch
                {
                    "OneMore" => ["One More", "1 More"],
                    "LookingGood" => ["Lookin' Good"],
                    "ThumbsUp" => ["Thumbs Up"],
                    "ThumbsDown" => ["Thumbs Down"],
                    "Laugh" => ["Laugh", "Cackle"],
                    "GG" => ["GG", "GG OR AM I LYING?"],
                    "Think" => ["Think", "Thinking"],
                    _ => [emojiType.Category],
                };
                string suffix = expectedSuffixes.FirstOrDefault((suff) => imageName.EndsWith(suff)) ?? throw new ArgumentException($"Unhandled irregular emoji name {imageName}");
                imageName = imageName[..^suffix.Length].TrimEnd();
                break;
        }

        imageName = "Emoji " + emojiType.Category switch
        {
            "OneMore" => "1More",
            "LookingGood" => "LookinGood",
            _ => emojiType.Category,
        } + " " + imageName;

        return new()
        {
            Name = emojiName,
            Image = imageName,
            Extension = extension,
            DisplayName = displayName,
        };
    }
}