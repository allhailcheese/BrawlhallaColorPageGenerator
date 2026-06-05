using System.Collections.Generic;
using System.IO;
using BrawlhallaColorPageGenerator.Objects;

namespace BrawlhallaColorPageGenerator.Writers.Colors;

public sealed class WeaponSkinWriter(WriterData data)
{
    public void WriteTo(string path, string baseWeapon)
    {
        string weaponName = Utils.BASE_WEAPON_NAME[baseWeapon];
        List<WeaponSkinType> defaults = [];

        using StreamWriter writer = new(path) { NewLine = "\n" };
        writer.Write("The following is a list of all [[");
        writer.Write(weaponName);
        writer.WriteLine("]] skins in [[Brawlhalla]].");
        writer.WriteLine();

        writer.Write("==");
        writer.Write(weaponName);
        writer.WriteLine(" Skins==");

        writer.WriteLine("{{itembox/top}}");
        foreach (WeaponSkinType weaponSkin in data.WeaponSkinTypes.WeaponSkins)
        {
            if (
                weaponSkin.WeaponSkinName == "Template" ||
                weaponSkin.BaseWeapon != baseWeapon ||
                weaponSkin.DisplayNameKey is null ||
                weaponSkin.WeaponSkinName.EndsWith("Stub") ||
                weaponSkin.WeaponSkinName.EndsWith("EivorMale") ||
                weaponSkin.WeaponSkinName.EndsWith("Stance") ||
                // only show level 3
                weaponSkin.UpgradesTo is not null
            ) continue;

            if (weaponSkin.OwnerHero is not null)
            {
                defaults.Add(weaponSkin);
                continue;
            }

            ProcessWeaponSkinType(weaponSkin, writer);
        }
        writer.WriteLine("{{itembox/bottom}}");
        writer.WriteLine();

        writer.Write("==Default ");
        writer.Write(weaponName);
        writer.WriteLine(" Skins==");
        writer.Write("These ");
        writer.Write(weaponName);
        writer.WriteLine(" skins belong to default Legend skins. Because of this, they can only be equipped by the Legend they belong to.");
        writer.WriteLine();

        writer.WriteLine("{{itembox/top}}");
        foreach (WeaponSkinType weaponSkin in defaults)
        {
            ProcessWeaponSkinType(weaponSkin, writer);
        }
        writer.WriteLine("{{itembox/bottom}}");
    }

    private void ProcessWeaponSkinType(WeaponSkinType weaponSkin, StreamWriter writer)
    {
        ItemNameParams nameParams = data.GetWeaponSkinNameParams(weaponSkin, false);
        writer.Write("{{itembox|width=");
        // width
        writer.Write(weaponSkin.BaseWeapon switch
        {
            "Axe" => 200,
            "Boots" => 200,
            "Bow" => 180,
            "Cannon" => 180,
            "Chakram" => 200,
            "Fists" => 190,
            "Greatsword" => 190,
            "Hammer" => 205,
            "Katar" => 180,
            "Orb" => 180,
            "Pistol" => 180,
            "RocketLance" => 205,
            "Scythe" => 180,
            "Spear" => 210,
            "Sword" => 190,
            _ => 0,
        });
        writer.Write("|height=");
        // height
        writer.Write(weaponSkin.BaseWeapon switch
        {
            "Axe" => 170,
            "Boots" => 170,
            "Bow" => 170,
            "Cannon" => 170,
            "Chakram" => 170,
            "Fists" => 170,
            "Greatsword" => 170,
            "Hammer" => 170,
            "Katar" => 190,
            "Orb" => 170,
            "Pistol" => 170,
            "RocketLance" => 170,
            "Scythe" => 170,
            "Spear" => 220,
            "Sword" => 200,
            _ => 0,
        });
        writer.Write("|name=");
        writer.Write(nameParams.Name);
        if (nameParams.Name != nameParams.DisplayName)
        {
            writer.Write("|displayname=");
            writer.Write(nameParams.DisplayName);
        }
        writer.Write("|image=");
        writer.Write(nameParams.Image);
        writer.Write('.');
        writer.Write(nameParams.Extension.GetName());

        ItemDescription description = data.GetItemDescription(weaponSkin.WeaponSkinName, ItemTypeEnum.WeaponSkin);

        writer.Write('|');
        writer.Write(description.DescriptionType.GetName());
        writer.Write('=');
        writer.Write(description.Description);

        writer.Write("|");
        writer.Write(description.DescriptionType.GetName());
        writer.Write("height=");
        // desc/cost height
        writer.Write(weaponSkin.BaseWeapon switch
        {
            "Axe" => 55,
            "Boots" => 55,
            "Bow" => 55,
            "Cannon" => 55,
            "Chakram" => 55,
            "Fists" => 70,
            "Greatsword" => 70,
            "Hammer" => 55,
            "Katar" => 55,
            "Orb" => 55,
            "Pistol" => 55,
            "RocketLance" => 55,
            "Scythe" => 55,
            "Spear" => 55,
            "Sword" => 55,
            _ => 0,
        });
        writer.Write("px");

        if (description.Rarity != RarityEnum.None)
        {
            writer.Write('|');
            writer.Write(description.Rarity.GetName());
            writer.Write("=true");
        }

        writer.WriteLine("}}");
    }
}