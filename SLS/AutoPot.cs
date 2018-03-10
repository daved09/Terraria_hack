using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class AutoPot
    {
        public static bool EnabledLife = true;
        public static bool EnabledMana = true;
        public static bool InfiniteItems = false;

        public static string AutoPotString()
        {
            var arr = new[]
            {
                EnabledLife ? "Life" : "",
                EnabledMana ? "Mana" : "",
                InfiniteItems ? "Infinite" : ""
            };
            return string.Format("[{0}]", string.Join("|", arr.Where(x => x != "")));
        }

        static void UsePotion(Player ply, Item item)
        {
            Main.PlaySound(item.UseSound, ply.position);
            ply.statLife += item.healLife;
            ply.statMana += item.healMana;
            if (ply.statLife > ply.statLifeMax2)
                ply.statLife = ply.statLifeMax2;
            if (ply.statMana > ply.statManaMax2)
                ply.statMana = ply.statManaMax2;

            var str = "Used " + item.Name + " and restored ";

            if (item.healLife > 0)
            {
                ply.HealEffect(item.healLife);
                str += item.healLife + " Life";
            }
            if (item.healMana > 0)
            {
                ply.ManaEffect(item.healMana);
                str += item.healLife > 0 ? " and " + item.healMana + " Mana" : item.healMana + " Mana";
            }

            Globals.Print(str);

            if(!InfiniteItems)
            {
                item.stack--;
                if (item.stack <= 0)
                    item.TurnToAir();

                Recipe.FindRecipes();
            }
        }

        public static void UpdatePlayerPost(Player plr)
        {
            if (!EnabledMana)
                return;

            if (plr.HeldItem.mana < 1)
                return;

            while(plr.statMana < plr.HeldItem.mana && plr.HeldItem.mana <= plr.statManaMax2)
                UsePotion(plr, plr.QuickMana_GetItemToUse());
        }

        public static void HurtMeMid(Player plr, double dmg)
        {
            if (!EnabledLife)
                return;

            var rest = plr.statLife - dmg;
            while(rest < 1)
            {
                var heal = null as Item;
                foreach(var x in plr.inventory)
                {
                    if (!x.potion || x.type == 0)
                        continue;

                    //if (x.healLife + rest < 1)
                        //continue;

                    if (heal == null)
                        heal = x;

                    if (x.healLife + rest > 0 && x.healLife < heal.healLife)
                        heal = x;
                }

                if (heal != null)
                    UsePotion(plr, heal);

                rest += heal.healLife;
            }
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "autopot" && cmd != "ap")
                return false;

            if(args.Length < 1)
            {
                Globals.Print("Missing args!");
                return true;
            }

            switch(args[0])
            {
                case "life":
                case "l":
                    EnabledLife = !EnabledLife;
                    Globals.Print("Autopot health potions {0}", EnabledLife ? "Enabled" : "Disabled");
                    break;

                case "mana":
                case "m":
                    EnabledMana = !EnabledMana;
                    Globals.Print("Autopot mana potions {0}", EnabledMana ? "Enabled" : "Disabled");
                    break;

                case "infinite":
                case "i":
                    InfiniteItems = !InfiniteItems;
                    Globals.Print("Autopot infinite items {0}", InfiniteItems ? "Enabled" : "Disabled");
                    break;

                default:
                    Globals.Print("Invalid argument!");
                    break;
            }

            return true;
        }
    }
}
