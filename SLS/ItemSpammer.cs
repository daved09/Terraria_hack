using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;

namespace SLS
{
    class ItemSpammer
    {
        public static bool Enabled = false;
        public static List<string> WhiteList = new List<string>();

        public static string GetItemSpammerString()
        {
            return string.Format("[{0}]", WhiteList.Any() ? string.Join("|", WhiteList) : "None");
        }

        protected static void CreateItem(Vector2 pos, int width, int height)
        {
            var num = Item.NewItem((int)pos.X, (int)pos.Y, width, height, Main.rand.Next(1, ItemID.Count), 1, false, 0, true);
            if (Main.netMode == 1)
                NetMessage.SendData(21, -1, -1, null, num, 1f, 0f, 0f, 0, 0, 0);
        }

        public static void InitializePre()
        {
            Globals.RegisterHotKey(Microsoft.Xna.Framework.Input.Keys.X, () =>
                {
                    Enabled = !Enabled;
                    Globals.Print("Item Spammer {0}", Enabled ? "Enabled" : "Disabled");
                }, alt: true);
        }

        public static void UpdatePre()
        {
            if (!Enabled)
                return;

            var ply = Globals.GetLocalPlayer();

            for (var i = 0; i < Main.player.Length; i++)
            {
                var x = Main.player[i];
                if (Globals.IsLocalPlayer(x) || WhiteList.Any(y => y == x.name))
                    continue;

                if (!x.active || x.dead)
                    continue;

                CreateItem(x.position, x.width, x.height);
            }
        }

        public static void UpdatePlayerPre(Player plr)
        {
            if (!Enabled)
                return;

            var ply = Globals.GetLocalPlayer();
            var pos = new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y);

            CreateItem(pos, ply.width, ply.height);
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "itemspammer" && cmd != "is")
                return false;

            if(args.Length < 2)
            {
                Globals.Print("Invalid arguments count!");
                return true;
            }

            var plr = null as Player;
            if (!PlayerUtil.TryGetPlayer(args[1], out plr))
            {
                Globals.Print("Player not found!");
                return true;
            }

            switch(args[0])
            {
                case "add":
                case "a":
                    if(WhiteList.Any(x => x == plr.name))
                    {
                        Globals.Print("Player {0} already in Whitelist!", plr.name);
                        return true;
                    }
                    WhiteList.Add(plr.name);
                    Globals.Print("Added {0} to the ItemSpammer whitelist", plr.name);
                    break;

                case "delete":
                case "del":
                case "d":

                case "remove":
                case "rem":
                case "r":
                    if(!WhiteList.Any(x => x == plr.name))
                    {
                        Globals.Print("Player {0} not whitelisted!", plr.name);
                        return true;
                    }
                    WhiteList.Remove(plr.name);
                    Globals.Print("Player {0} removed from whitelist", plr.name);
                    break;
            }

            return true;
        }
    }
}
