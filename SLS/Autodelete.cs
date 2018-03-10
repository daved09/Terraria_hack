using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;

namespace SLS
{
    class Autodelete
    {
        public static bool Enabled = false;

        public static bool GetItemPre(Player plr, Item item, out Item result, ref bool noText)
        {
            result = item.Clone();
            result.TurnToAir();

            if (!Enabled)
                return false;

            if (plr.inventory.Any(x => x.type == item.type))
                return false;

            switch(item.type)
            {
                case ItemID.CopperCoin:
                case ItemID.SilverCoin:
                case ItemID.GoldCoin:
                case ItemID.PlatinumCoin:
                    return false;
            }

            if (item.ammo != 0)
                return false;

            //Globals.Print("Deleted {0} {1}", item.stack, item.Name);
            noText = true;

            return true;
        }

        public static bool SendChatMessagePre(string cmd)
        {
            if (cmd != "autodelete" && cmd != "ad")
                return false;

            Enabled = !Enabled;
            Globals.Print("Autodelete collected items {0}", Enabled ? "Enabled" : "Disabled");
            return true;
        }
    }
}
