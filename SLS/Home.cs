using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

using Microsoft.Xna.Framework;

namespace SLS
{
    class Home
    {
        public static void InitializePre()
        {
            if (!File.Exists("homes.dat"))
                File.Create("homes.dat").Close();
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "home" && cmd != "h")
                return false;

            var ply = Globals.GetLocalPlayer();
            var teleportPos = Vector2.Zero;

            var list = File.ReadAllLines("homes.dat").ToList();

            if (args.Length < 1)
            {
                if(ply.SpawnX != -1 && ply.SpawnY != -1)
                    teleportPos = new Vector2(ply.SpawnX * 16, ply.SpawnY * 16);
                else
                {
                    Globals.Print("No spawnpoint set! Teleporting to global spawn...");
                    teleportPos = new Vector2(Main.spawnTileX * 16 + 8 - ply.width / 2, Main.spawnTileY * 16 - ply.height);
                }
            }
            else if (args.Length == 1)
            {
                if (args[0].ToLower() == "show")
                {
                    Globals.Print("Homes in {0}: ", Main.worldName);
                    foreach (var x in list.Where(x => x.Split(new[] { '|' }, 2)[0] == Main.worldName))
                        Globals.Print(x.Replace(Main.worldName + "|", ""));

                    return true;
                }

                var home = list.FirstOrDefault(x =>
                    {
                        var s = x.Split(new[] { '|' });
                        return s[0] == Main.worldName && s[1].ToLower() == args[0].ToLower();
                    });

                if (home == null)
                {
                    Globals.Print("Home not found!");
                    return true;
                }

                var split = home.Split(new[] { '|' });
                teleportPos = new Vector2(float.Parse(split[2]), float.Parse(split[3]));
            }
            else if (args.Length > 1)
            {
                var first = list.FirstOrDefault(x =>
                {
                    var s = x.Split(new[] { '|' });
                    return s[0] == Main.worldName && s[1].ToLower() == args[1].ToLower();
                });

                switch (args[0])
                {
                    case "add":
                    case "a":
                        if (first != null)
                            list.Remove(first);

                        list.Add(string.Join("|", new[]
                            {
                                Main.worldName, args[1], ply.position.X.ToString(), ply.position.Y.ToString()
                            }));
                        Globals.Print("Home {0} added at X: {1}, Y: {2}", args[1], ply.position.X, ply.position.Y);
                        break;

                    case "remove":
                    case "rem":
                    case "r":
                        if (first != null)
                            list.Remove(first);
                        else
                        {
                            Globals.Print("Home not found!");
                            return true;
                        }

                        Globals.Print("Home {0} removed!", args[1]);
                        break;
                }
                File.WriteAllLines("homes.dat", list);
                return true;
            }

            ply.position = teleportPos;
            ply.velocity = Vector2.Zero;

            return true;
        }
    }
}
