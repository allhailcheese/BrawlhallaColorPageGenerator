using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator;

public partial class WriterData
{
    private Dictionary<string, CostumeType>? _weaponSkinToCostume = null;

    [MemberNotNull(nameof(_weaponSkinToCostume))]
    private void InitWeaponSkinToCostume()
    {
        if (_weaponSkinToCostume is not null) return;
        _weaponSkinToCostume = [];
        foreach (CostumeType costume in CostumeTypes.Costumes)
        {
            if (costume.WeaponSet is null || costume.DoesNotOwnWeaponSet || !HeroTypes.HeroesMap.TryGetValue(costume.OwnerHero, out HeroType? hero))
                continue;

            if (hero.BaseWeapon1 is not null)
                _weaponSkinToCostume[hero.BaseWeapon1 + costume.WeaponSet] = costume;
            if (hero.BaseWeapon2 is not null)
                _weaponSkinToCostume[hero.BaseWeapon2 + costume.WeaponSet] = costume;
        }
    }

    public CostumeType? GetWeaponSkinSourceCostume(string weaponSkin)
    {
        InitWeaponSkinToCostume();
        return _weaponSkinToCostume.GetValueOrDefault(weaponSkin);
    }
}