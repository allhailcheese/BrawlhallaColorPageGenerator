This repo contains a script for generating various pages in the [Official Brawlhalla Wiki](https://brawlhalla.wiki.gg/wiki/Brawlhalla_Wiki) that include a lot of often-changing data.

#### It generates
* Templates that list every item in a specific color
  * [Template:Color_Skins](https://brawlhalla.wiki.gg/wiki/Template:Color_Skins)
  * [Template:Color_Weapon_Skins](https://brawlhalla.wiki.gg/wiki/Template:Color_Weapon_Skins)
  * [Template:Color_Companions](https://brawlhalla.wiki.gg/wiki/Template:Color_Companions)
* Pages that list every weapon skin, and how to acquire them
  * [Weapon_Skins](https://brawlhalla.wiki.gg/wiki/Weapon_Skins) subpages, for example [Weapon_Skins/Axe](https://brawlhalla.wiki.gg/wiki/Weapon_Skins/Axe)
  * [Weapon_Skins/Default_Weapons](https://brawlhalla.wiki.gg/wiki/Weapon_Skins/Default_Weapons)
* The skin sections for each individual legend, used in the [Skins](https://brawlhalla.wiki.gg/wiki/Skins) page which lists every skin and how to acquire it
* The table rows for [Leveling](https://brawlhalla.wiki.gg/wiki/Leveling)
* The table rows for [Stances](https://brawlhalla.wiki.gg/wiki/Stances)
* The stat sections of [Template:QuestList](https://brawlhalla.wiki.gg/wiki/Template:QuestList)
* [Template:BOTW](https://brawlhalla.wiki.gg/wiki/Template:BOTW)
* Bundle pages
  * [Template:BundleCost](https://brawlhalla.wiki.gg/wiki/Template:BundleCost)
  * Individual bundle pages
* The [Map Set](https://brawlhalla.wiki.gg/wiki/Map_Set) page
* The low/mid/high lists for [Template:QuestList](https://brawlhalla.wiki.gg/wiki/Template:QuestList)

#### And maybe i'll add
* The high/low stat lists for [Stats](https://brawlhalla.wiki.gg/wiki/Stats)
* [Avatars](https://brawlhalla.wiki.gg/wiki/Avatars) (subpages)
* [Emojis](https://brawlhalla.wiki.gg/wiki/Emojis)?
* [Emotes](https://brawlhalla.wiki.gg/wiki/Emotes)?

### Running

Clone the repo: `git clone --recurse-submodules https://github.com/eyalzus12/BrawlhallaColorPageGenerator.git`

Then inside the "BrawlhallaColorPageGenerator" folder: `dotnet run --project BrawlhallaColorPageGenerator`

Requires .NET 9

The program creates an `outputs` folder with the page contents inside.

It assumes brawlhalla is installed into `C:/Program Files (x86)/Steam/steamapps/common/Brawlhalla`.