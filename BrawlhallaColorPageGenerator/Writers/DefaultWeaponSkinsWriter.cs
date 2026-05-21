using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class DefaultWeaponSkinsWriter(WriterData data)
{
    public void WriteTo(string path)
    {
        using StreamWriter writer = new(path);
        writer.WriteLine("Each Legend comes with two default weapon skins, which are equipped by default.");
        writer.WriteLine();
        writer.WriteLine("These weapon skins are unique in that they can only be used by the Legend they belong to. For example, [[Bödvar]] can use his default [[Warhammer]] regardless of skin equipped, but all other Hammer Legends are not able to equip it.");

        Dictionary<string, IEnumerable<WeaponSkinType>> defaultSkinsByType = data.WeaponSkinTypes.WeaponSkins.Where((w) =>
        {
            return w.WeaponSkinName != "Template"
                && w.DisplayNameKey is not null
                && w.OwnerHero is not null // default skin
                && !w.WeaponSkinName.EndsWith("Stance");
        }).GroupBy((w) => w.BaseWeapon, (w) => w, (s, w) => (s, w)).ToDictionary(x => x.s, x => x.w);

        foreach (string baseWeapon in (ReadOnlySpan<string>)["Hammer", "Sword", "Pistol", "RocketLance", "Spear", "Katar", "Axe", "Bow", "Fists", "Scythe", "Cannon", "Orb", "Greatsword", "Boots", "Chakram"])
        {
            IEnumerable<WeaponSkinType> weaponSkins = defaultSkinsByType[baseWeapon];
            writer.WriteLine();
            writer.Write("==");
            writer.Write(Utils.BASE_WEAPON_NAME[baseWeapon]);
            writer.WriteLine("==");

            writer.WriteLine("{{itembox/top}}");
            foreach (WeaponSkinType weaponSkin in weaponSkins)
            {
                ItemNameParams weaponSkinNameParams = data.GetWeaponSkinNameParams(weaponSkin, false);
                CostumeType costume = data.GetWeaponSkinSourceCostume(weaponSkin.WeaponSkinName)!;
                ItemNameParams skinNameParams = data.GetSkinNameParams(costume, false);

                writer.Write("{{itembox|width=180|height=170|name=");
                writer.Write(weaponSkinNameParams.Name);
                if (weaponSkinNameParams.Name != weaponSkinNameParams.DisplayName)
                {
                    writer.Write("|displayname=");
                    writer.Write(weaponSkinNameParams.DisplayName);
                }
                writer.Write("|image=");
                writer.Write(weaponSkinNameParams.Image);
                writer.Write('.');
                writer.Write(weaponSkinNameParams.Extension.GetName());
                writer.Write("|desc=[[");
                writer.Write(skinNameParams.Name);
                writer.WriteLine("]]|descheight=55px}}");
            }
            writer.WriteLine("{{itembox/bottom}}");
        }
    }
}