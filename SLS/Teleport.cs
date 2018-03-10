using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Terraria;

namespace SLS
{
    class Teleport
    {
        public static Keys Key = Keys.F;

        public static void InitializePre()
        {
            Globals.RegisterHotKey(Key, () =>
                {
                    var ply = Globals.GetLocalPlayer();
                    ply.position = new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y);
                    ply.velocity = Vector2.Zero;
                });
        }

        public static void UpdatePlayerPost(Player plr)
        {
            if(Main.mapFullscreen && Main.mouseRight)
            {
                int num = Main.maxTilesX * 16;
                int num2 = Main.maxTilesY * 16;
                Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY);
                vector.X -= (float)(Main.screenWidth / 2);
                vector.Y -= (float)(Main.screenHeight / 2);
                Vector2 mapFullscreenPos = Main.mapFullscreenPos;
                Vector2 vector2 = mapFullscreenPos;
                vector /= 16f;
                vector *= 16f / Main.mapFullscreenScale;
                vector2 += vector;
                vector2 *= 16f;
                vector2.Y -= (float)plr.height;
                if (vector2.X < 0f)
                {
                    vector2.X = 0f;
                }
                else if (vector2.X + (float)plr.width > (float)num)
                {
                    vector2.X = (float)(num - plr.width);
                }
                if (vector2.Y < 0f)
                {
                    vector2.Y = 0f;
                }
                else if (vector2.Y + (float)plr.height > (float)num2)
                {
                    vector2.Y = (float)(num2 - plr.height);
                }
                plr.position = vector2;
                plr.velocity = Vector2.Zero;
                plr.fallStart = (int)(plr.position.Y / 16f);
            }
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "tp")
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

            var ply = Globals.GetLocalPlayer();
            ply.position = plr.position;
            ply.velocity = Vector2.Zero;

            Globals.Print("Successfully teleported to {0}", plr.name);

            return true;
        }
    }
}
