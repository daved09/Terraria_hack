using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;

namespace SLS
{
    class ShootProjectiles
    {
        public static bool Enabled = false;
        public static Item Backup;
        public static int Owner = 0;

        public static void InitializePre()
        {
            Globals.RegisterHotKey(Microsoft.Xna.Framework.Input.Keys.V, () =>
                {
                    Enabled = !Enabled;
                    var ply = Globals.GetLocalPlayer();
                    if (Enabled)
                    {
                        Backup = ply.HeldItem.DeepClone();
                        ply.inventory[ply.selectedItem].netDefaults(Terraria.ID.ItemID.RocketLauncher);
                    }
                    else
                        ply.inventory[ply.selectedItem] = Backup;
                    Globals.Print(Enabled ? "Rocket shooting started!" : "Rocket shooting stopped :,(");
                }, alt: true);
        }

        public static void UpdatePlayerPost(Player plr)
        {
            if (!Enabled)
                return;

            var pos = plr.position;
            var pos2 = new Vector2(Main.screenPosition.X + Main.mouseX, Main.screenPosition.Y + Main.mouseY);

            Projectile.NewProjectile(pos, Vector2.Normalize(pos2 - pos) * 50f, Terraria.ID.ProjectileID.StickyDynamite, 0, 0f, Owner);
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "shootprojectiles" && cmd != "sp")
                return false;

            if(args.Length < 1)
            {
                Globals.Print("Invalid arguments count!");
                return true;
            }

            var plr = null as Player;
            if(!PlayerUtil.TryGetPlayer(args[0], out plr))
            {
                Globals.Print("Player not found!");
                return true;
            }

            Owner = plr.whoAmI;
            Globals.Print("Successfully selected {0} as new Projectile owner!", plr.name);

            return true;
        }
    }
}
