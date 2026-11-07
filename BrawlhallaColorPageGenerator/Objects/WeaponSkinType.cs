using System.Collections.Generic;
using System.IO;
using System.Linq;
using nietras.SeparatedValues;

namespace BrawlhallaColorPageGenerator.Objects;

public sealed class WeaponSkinType
{
    private static readonly string[] COLOR_SWAP_COLS = ["InheritCostumeDefines", "HairLt_Define", "Hair_Define", "HairDk_Define", "Body1VL_Define", "Body1Lt_Define", "Body1_Define", "Body1Dk_Define", "Body1VD_Define", "Body1Acc_Define", "Body2VL_Define", "Body2Lt_Define", "Body2_Define", "Body2Dk_Define", "Body2VD_Define", "Body2Acc_Define", "SpecialVL_Define", "SpecialLt_Define", "Special_Define", "SpecialDk_Define", "SpecialVD_Define", "SpecialAcc_Define", "HandsLt_Define", "HandsDk_Define", "HandsSkinLt_Define", "HandsSkinDk_Define", "ClothVL_Define", "ClothLt_Define", "Cloth_Define", "ClothDk_Define", "WeaponVL_Define", "WeaponLt_Define", "Weapon_Define", "WeaponDk_Define", "WeaponAcc_Define"];

    public string WeaponSkinName { get; }
    public uint WeaponSkinID { get; }
    public string BaseWeapon { get; }
    public string? OwnerHero { get; }
    public string? UpgradesTo { get; }
    public string? DisplayNameKey { get; }

    public bool CanColorSwap { get; }

    public WeaponSkinType(SepReader.Row row)
    {
        WeaponSkinName = row[nameof(WeaponSkinName)].ToString();

        WeaponSkinID = row[nameof(WeaponSkinID)].Parse<uint>();

        BaseWeapon = row[nameof(BaseWeapon)].ToString();

        OwnerHero = row[nameof(OwnerHero)].ToString();
        if (string.IsNullOrWhiteSpace(OwnerHero)) OwnerHero = null;

        DisplayNameKey = row[nameof(DisplayNameKey)].ToString();
        if (string.IsNullOrWhiteSpace(DisplayNameKey)) DisplayNameKey = null;

        UpgradesTo = row[nameof(UpgradesTo)].ToString();
        if (string.IsNullOrWhiteSpace(UpgradesTo)) UpgradesTo = null;

        CanColorSwap = false;
        foreach (string colorSwapCol in COLOR_SWAP_COLS)
        {
            if (!string.IsNullOrEmpty(row[colorSwapCol].ToString()))
            {
                CanColorSwap = true;
                break;
            }
        }
    }
}

public sealed class WeaponSkinTypes
{
    public WeaponSkinType[] WeaponSkins { get; }
    public Dictionary<string, WeaponSkinType> WeaponSkinsMap { get; }
    public Dictionary<string, int> UpgradeLevel { get; }

    public WeaponSkinTypes(string content)
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
        WeaponSkins = [.. csvReader.Enumerate((row) => new WeaponSkinType(row))];
        WeaponSkinsMap = new(WeaponSkins.Select((w) => new KeyValuePair<string, WeaponSkinType>(w.WeaponSkinName, w)));

        UpgradeLevel = [];
        Queue<WeaponSkinType> leftover = new(WeaponSkins);
        while (leftover.TryDequeue(out WeaponSkinType? costumeType))
        {
            if (costumeType.WeaponSkinName == "Template")
                continue;

            if (costumeType.UpgradesTo is null)
            {
                UpgradeLevel[costumeType.WeaponSkinName] = 0;
                continue;
            }

            if (UpgradeLevel.TryGetValue(costumeType.UpgradesTo, out int existingLevel))
            {
                if (existingLevel == 0) existingLevel = 1;

                UpgradeLevel[costumeType.WeaponSkinName] = existingLevel++;

                // go up the upgrade chain and update level
                string? upgradedCostumeType = costumeType.UpgradesTo;
                while (
                    upgradedCostumeType is not null &&
                    WeaponSkinsMap.TryGetValue(upgradedCostumeType, out WeaponSkinType? costume)
                )
                {
                    UpgradeLevel[costume.WeaponSkinName] = existingLevel++;
                    upgradedCostumeType = costume.UpgradesTo;
                }
            }
            else
            {
                leftover.Enqueue(costumeType);
            }
        }
    }
}