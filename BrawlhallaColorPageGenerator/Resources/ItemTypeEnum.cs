using System;

namespace BrawlhallaColorPageGenerator;

public enum ItemTypeEnum
{
    Costume,
    WeaponSkin,
    Taunt,
    SpawnBot,
    Hero,
    KOEffect,
    ChanceBox, // chests
    Avatar,
    ColorScheme,
    UniversalColor, // used for RGB, CMYK
    RandomColor, // used for RGB
    PeekableColor, // used for CMYK
    Podium,
    Bundle,
    PlayerTheme, // insignias
    Entitlement, // all legends pack
    Emoji,
    EmitterGroup, // trail effects
    Companion,
    Moniker,
    Guild, // creating a guild
}

public static class ItemTypeEnumExtensions
{
    public static string GetName(this ItemTypeEnum itemType) => itemType switch
    {
        ItemTypeEnum.Costume => "Costume",
        ItemTypeEnum.WeaponSkin => "WeaponSkin",
        ItemTypeEnum.Taunt => "Taunt",
        ItemTypeEnum.SpawnBot => "SpawnBot",
        ItemTypeEnum.Hero => "Hero",
        ItemTypeEnum.KOEffect => "KOEffect",
        ItemTypeEnum.ChanceBox => "ChanceBox",
        ItemTypeEnum.Avatar => "Avatar",
        ItemTypeEnum.ColorScheme => "ColorScheme",
        ItemTypeEnum.UniversalColor => "UniversalColor",
        ItemTypeEnum.RandomColor => "RandomColor",
        ItemTypeEnum.PeekableColor => "PeekableColor",
        ItemTypeEnum.Podium => "Podium",
        ItemTypeEnum.Bundle => "Bundle",
        ItemTypeEnum.PlayerTheme => "PlayerTheme",
        ItemTypeEnum.Entitlement => "Entitlement",
        ItemTypeEnum.Emoji => "Emoji",
        ItemTypeEnum.EmitterGroup => "EmitterGroup",
        ItemTypeEnum.Companion => "Companion",
        ItemTypeEnum.Moniker => "Moniker",
        ItemTypeEnum.Guild => "Guild",
        _ => throw new IndexOutOfRangeException(),
    };
}