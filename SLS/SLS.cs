using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ReLogic.Graphics;

namespace SLS
{
    public class Hooks
    {
        //TypeDefinition(Terraria.Main)
        public static void InitializePre()
        {
            Main.cEd = true; //Collectors edition
            Main.versionNumber = "(" + (Globals.IsCracked ? "Cracked" : "Steam") + ") Terraria " + Main.versionNumber;
            Main.versionNumber += "->" + Globals.Name;

            SkipSplash.InitializePre();
            Teleport.InitializePre();
            Home.InitializePre();
            Spawnrate.InitializePre();
            Events.InitializePre();
            Forcefield.InitializePre();
            ShootProjectiles.InitializePre();
            ItemSpammer.InitializePre();

            Globals.RegisterHotKey(Keys.X, () =>
                {
                    if (LastUsedCommand == string.Empty)
                        return;

                    SendChatMessageFromClientPre(LastUsedCommand);

                    //Globals.Print("Last Command executed! ({0})", LastUsedCommand);
                }, shift: true);

            Globals.RegisterHotKey(Keys.V, () =>
                {
                    //SendChatMessageFromClientPre(".p best");
                    Prefix.SendChatMessagePre("p", new[] { "best" });

                    //Globals.Print("Executed Command \".prefix best\"");
                }, shift: true);

            Globals.RegisterHotKey(Keys.C, () =>
                {
                    ControlUseItem = !ControlUseItem;
                    Globals.Print("Left mouse button {0}", ControlUseItem ? "Down" : "Up");
                }, alt: true);
        }

        public static bool DrawModuleList = true;

        public static void DoDrawPost()
        {
            DrawManager.StartDrawing();

            ESP.DoDrawPost();

            if (DrawModuleList && !(Main.playerInventory || Main.editChest || Main.editSign || Main.drawingPlayerChat))
            {
                if(VampireHeal.Enabled)
                    DrawManager.AddEnabledModule("Vampireheal [" + VampireHeal.HealAmount * 100 + "%] (Silent)", Color.LimeGreen);

                if (Spawnrate.DefaultMaxSpawns != 5)
                    DrawManager.AddEnabledModule("Max Spawns: " + Spawnrate.DefaultMaxSpawns, Color.Yellow);
                if (Spawnrate.DefaultSpawnRate != 100)
                    DrawManager.AddEnabledModule("Spawnrate: " + Spawnrate.DefaultSpawnRate + "%", Color.Yellow);

                if (Buffs.Shine)
                    DrawManager.AddEnabledModule("Shine", Color.Yellow);

                if (Main.netMode == 0)
                    DrawManager.AddEnabledModule("Pickup Range [" + LocalPlayerPickupRange + "]", Color.Yellow);

                DrawManager.AddEnabledModule("No Potion sickness", Color.Yellow);

                if (NoPickup.Enabled)
                    DrawManager.AddEnabledModule("NoPickup", Color.Yellow);

                if (NoFall.Enabled)
                    DrawManager.AddEnabledModule("NoFall", Color.LimeGreen);

                DrawManager.AddEnabledModule("Movement Helper", Color.Cyan);

                if (ItemSpammer.Enabled)
                    DrawManager.AddEnabledModule("ItemSpammer " + ItemSpammer.GetItemSpammerString(), Color.Red);

                DrawManager.AddEnabledModule("Infinite Flight", Color.LimeGreen);

                if (InfiniteAmmo.Enabled)
                    DrawManager.AddEnabledModule("Infinite Ammo", Color.LimeGreen);

                DrawManager.AddEnabledModule("Homing Projectiles" + (Main.netMode != 0 ? " (Async)" : ""), Color.LimeGreen);

                if (God.Enabled)
                    DrawManager.AddEnabledModule("God: " + God.Mode.ToString(), God.Mode == God.EMode.Demi ? Color.Yellow : Color.DarkRed);

                if (Forcefield.Enabled)
                    DrawManager.AddEnabledModule("ForceField " + Forcefield.ForceFieldString(), Forcefield.InstaKill ? Color.Red : Color.LimeGreen);

                if(FixPrefixes)
                    DrawManager.AddEnabledModule("Fixed Prefixes", Color.LimeGreen);

                if(ESP.Enabled)
                    DrawManager.AddEnabledModule("ESP [" + ESP.Range + "]", Color.LimeGreen);

                DrawManager.AddEnabledModule("Buffs " + Buffs.ConstantBuffsString(), Color.Yellow);

                if (ControlUseItem)
                    DrawManager.AddEnabledModule("Autousing current Held item", Color.LimeGreen);

                if (AutoPot.EnabledLife || AutoPot.EnabledMana)
                    DrawManager.AddEnabledModule("Autopot " + AutoPot.AutoPotString(), Color.LimeGreen);

                if (Autodelete.Enabled)
                    DrawManager.AddEnabledModule("Autodelete", Color.Yellow);

                DrawManager.AddEnabledModule("All Accessories", Color.LimeGreen);

                DrawManager.AddEnabledModule("[" + Globals.Name + "]", Color.Cyan);
            }

            DrawManager.EndDrawing();
        }

        public static void DrawMenuPost()
        {
            DrawManager.StartDrawing();

            DrawManager.DrawString("Hello " + Globals.Username + "!" + "\nI hope you enjoy using SLSHook :)\nDon't worry about longer initializing times,\nit only seems to take this long because the splash is skipped", Color.Cyan, Vector2.Zero);

            ShootProjectiles.Enabled = false;
            ItemSpammer.Enabled = false;

            DrawManager.EndDrawing();
        }

        public static void UpdatePre()
        {
            ItemSpammer.UpdatePre();
        }

        public static bool MouseText_DrawItemTooltipPre(int rare, byte diff, int x, int y)
        {
            return BetterTooltips.MouseText_DrawItemTooltipPre(rare, diff, x, y);
        }

        //TypeDefinition(Terraria.Player)
        public static void ItemCheckPre(Player plr)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            if (ControlUseItem)
            {
                plr.controlUseItem = true;
                if (!ControlUsedItem)
                    ControlUsedItem = true;
            }
            else if (ControlUsedItem)
            {
                ControlUsedItem = false;
                plr.controlUseItem = false;
            }
        }

        public static bool ControlUseItem = false;
        public static bool ControlUsedItem = false;

        public static void UpdatePlayerPre(Player plr)
        {
            //plr.hasBanner = true;
            //for (var i = 0; i < plr.NPCBannerBuff.Length; i++)
                //plr.NPCBannerBuff[i] = true;
        }

        public static void UpdatePlayerPost(Player plr)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            Globals.ExecuteHotKeys();

            /*plr.runAcceleration = 0.16f;
            plr.wingTime = 1f;
            plr.carpetTime = 1;
            plr.rocketTime = 1;*/
            AutoPot.UpdatePlayerPost(plr);
            Forcefield.UpdatePlayerPost(plr);
            VampireHeal.UpdatePlayerPost(plr);
            Teleport.UpdatePlayerPost(plr);
            ShootProjectiles.UpdatePlayerPost(plr);
            ItemSpammer.UpdatePlayerPre(plr);
        }

        public static void UpdateBuffsPost(Player plr)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            //AllAccessories.UpdateBuffsPost(plr);
            Buffs.UpdateBuffsPost(plr);
            //BuffImunity.UpdateBuffsPost(plr);
        }

        public static void UpdateEquipsPre(Player plr, int i)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            WorkingSocialSlots.UpdateEquipsPre(plr, i);
        }

        public static void UpdateEquipsPost(Player plr)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            NoFall.UpdateEquipsPost(plr);
            //DoubleJump.UpdateEquipsPost(plr);
        }

        public static void HurtMeMid(Player plr, ref double damage)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            AutoPot.HurtMeMid(plr, damage);
            God.HurtMeMid(plr, ref damage);
        }

        public static void HurtMePost(Player plr)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            plr.immuneTime *= 2;
        }

        public static int LocalPlayerPickupRange = 0;

        public static bool GrabItemsPre(Player plr)
        {
            Player.defaultItemGrabRange = 38;

            if (!Globals.IsLocalPlayer(plr))
                return false;

            if(Main.netMode == 0)
            {
                //Player.defaultItemGrabRange = Forcefield.Enabled ? (int)Forcefield.Range : 38 * 5;
                //LocalPlayerPickupRange = Player.defaultItemGrabRange;
            }

            return NoPickup.Enabled;
        }

        public static void HorizontalMovementPost(Player plr)
        {
            if (!Globals.IsLocalPlayer(plr))
                return;

            if (!Main.mouseLeft ||
                plr.HeldItem.mana > plr.statMana ||
                (plr.HeldItem.useAmmo != 0 && !plr.inventory.Any(x => plr.HeldItem.useAmmo == x.type)) ||
                plr.HeldItem.createTile > 0 || plr.HeldItem.createWall > 0 ||
                plr.HeldItem.axe > 0 || plr.HeldItem.pick > 0 || plr.HeldItem.hammer > 0)
                return;

            if (Main.screenPosition.X + Main.mouseX > plr.position.X)
                plr.direction = 1;
            else
                plr.direction = -1;
        }

        public static bool skipNextGetItem = false;
        public static bool GetItemPre(Player plr, Item newItem, out Item result, ref bool noText)
        {
            result = new Item();

            if(skipNextGetItem)
            {
                skipNextGetItem = false;
                return false;
            }

            if (!Globals.IsLocalPlayer(plr) || newItem.type == 0)
                return false;

            return Autodelete.GetItemPre(plr, newItem, out result, ref noText);
        }


        public static void PickAmmoPre(ref bool dontConsume)
        {
            InfiniteAmmo.PickAmmoPre(ref dontConsume);
        }

        //TypeDefinition(Terraria.Item)
        public static void ItemSetDefaultsPost(Item item)
        {
            if (item.pick > 0 ||
                item.axe > 0 ||
                item.hammer > 0 ||
                item.melee || item.magic || item.ranged)
                item.autoReuse = true;

            /*if (item.createTile > 0 ||
                item.createWall > 0 ||
                item.summon)
                item.useTime = 1;

            item.pick = (int)(item.pick * 1.5f);
            item.axe = (int)(item.axe * 1.5f);
            item.hammer = (int)(item.hammer * 1.5f);*/

            switch(item.type)
            {
                case ItemID.CopperCoin:
                case ItemID.SilverCoin:
                case ItemID.GoldCoin:
                case ItemID.PlatinumCoin:
                    item.shoot = ProjectileID.ChlorophyteBullet;
                    item.damage *= 2;
                    break;

                default:
                    //if (item.maxStack > 1)
                        //item.maxStack = int.MaxValue;
                    break;
            }

            if (Globals.IsPrivate)
            {
                if ((item.ranged || item.magic || item.melee) && (item.pick < 1 && item.axe < 1 && item.hammer < 1))
                {
                    //item.useTime = (int)((float)item.useTime * 0.75f);
                    //item.shootSpeed *= 1.5f;
                }
            }
        }

        public static bool skipNextPrefix = false;
        public static bool FixPrefixes = true;

        public static void PrefixPre(Item item, ref int prefix)
        {
            if (!FixPrefixes)
                return;

            if(skipNextPrefix)
            {
                skipNextPrefix = false;
                return;
            }

            if (item.Name.ToLower().Contains("coin"))
                return;

            if (item.accessory)
                prefix = PrefixID.Menacing;
            else if (item.melee)
                prefix = PrefixID.Legendary;
            else if (item.ranged)
                prefix = PrefixID.Unreal;
            else if (item.magic)
                if (item.mana == 0)
                    prefix = PrefixID.Godly;
                else
                    prefix = PrefixID.Mythical;
            else if (item.summon)
                prefix = PrefixID.Ruthless;

            if (!item.accessory)
            {
                if (item.knockBack == 0f)
                    prefix = PrefixID.Demonic;

                if (item.damage == 0)
                    prefix = PrefixID.Rapid;
            }

            if (item.defense > 0)
                prefix = PrefixID.Menacing;
        }

        //TypeDefinition(Terraria.Projectile)
        public static void Projectile_AI001(Projectile projectile)
        {
            if (projectile.owner != Globals.GetLocalPlayer().whoAmI)
                return;

            ProjectileAI.ProjectTile_AI001Post(projectile);
        }

        public static void Projectile_DamagePost(Projectile projectile)
        {
            var ply = Globals.GetLocalPlayer();

            if (projectile.owner != Main.myPlayer)
                return;

            VampireHeal.Projectile_DamagePost(projectile, ply);
        }

        static string LastUsedCommand = string.Empty;
        public static int ChatPatch = -1;
        //TypeDefinition(Terraria.NetMessage)
        public static bool SendChatMessageFromClientPre(string text)
        {
            if (text[0] != '.')
            {
                ChatPatch = Main.netMode;
                return false;
            }

            ChatPatch = -1;
            Main.chatText = string.Empty;

            var split = text.Split(new[] { ' ' }, 2);
            var cmd = split[0].Remove(0, 1);
            if(cmd == "help")
            {
                var help = new[]
                    {
                        "help",
                        "clear",
                        "modules"
                    };

                    var help2 = new[]
                    {
                        "itemspammer(is): add(a) || delete(del)(d) || remove(rem)(r)",
                        "esp(e): range (r)",
                        "item(i): ItemID opt:Count opt:Prefix",
                        "give(g): PlayerName ItemID opt:Count opt:Prefix",
                        "god(g): opt:mode(m) opt:(Normal, Demi)",
                        "prefix(p): PrefixName || best",
                        "findtile(find, ft): TileName",
                        "time(t): morning(m) || day(d) || afternoon(a) || night(n)",
                        "forcefield(ff): opt:range(r) || opt:instakill (ik) || opt:checkcollsion (cc) || opt:delay (d) || opt:calculateattackdelay(cad)",
                        "nopickup(np)",
                        "buff(b): buffname time",
                        "autodelete(ad)",
                        "name: newName",
                        "changeuuid",
                        "autopot(ap): life(l) || mana(m) || infinite(i)",
                        "nofall(nf)",
                        "saveinventory(si)",
                        "restoreinventory(ri)",
                        "copyinventory(ci): playername",
                        "infiniteammo(infammo)(ia)"
                    };

                var help3 = new[]
                    {
                        "Shift+X: Reexecute last successfully ran command",
                        "Shift+V: Execute \".prefix best\"",
                        "F: Teleport to mouse cursor",

                        "N: Toggle Spawns",
                        "OemPlus: Increase Spawnrate by 10%",
                        "OemMinus: Decrease Spawnrate by 10%",
                        "Shit+OemPlus: Increase Spawnrate by 50%",
                        "Shift+OemMinus: Decrease Spawnrate by 50%",
                        "Alt+OemPlus: Increase max Spawns by 1",
                        "Alt+OemMinus: Decrease max Spawns by 1",
                        "Alt+Shift+OemPlus: Increase max Spawns by 5",
                        "Alt+Shift+OemMinus: Decrease max Spawns by 5",

                        "Shift+C: Toggle Forcefield"
                    };

                if (Main.chatLine[0].parsedText == null || Main.chatLine[0].text == string.Empty)
                    Globals.Print("Congrats! You've spotted a bug, please contact Ace! _SL/S");

                Globals.Print("Core commands:");
                foreach (var x in help)
                    Globals.Print(x);
                Globals.Print("Module commands:");
                foreach (var x in help2.OrderBy(x => x))
                    Globals.Print(x);
                Globals.Print("Hotkeys: ");
                foreach (var x in help3)
                    Globals.Print(x);
                return true;
            }
            else if(cmd == "clear")
            {
                foreach (var x in Main.chatLine)
                {
                    x.text = "";
                    x.parsedText = null;
                }

                Globals.Print("Chat cleared!");

                return true;
            }
            else if(cmd == "modules")
            {
                DrawModuleList = !DrawModuleList;
                Globals.Print("Module list turned {0}!", DrawModuleList ? "on" : "off");
                return true;
            }

            var argList = new List<string>();

            if (split.Length > 1)
            {
                var quotafound = false;
                var start = 0;
                var skipNext = false;

                if (split[1].Count(x => x == '"') % 2 != 0)
                {
                    Globals.Print("Invalid quota count!");
                    return true;
                }

                for (var i = 0; i < split[1].Length; i++)
                {
                    if (skipNext)
                    {
                        skipNext = false;
                        continue;
                    }

                    var valid = i + 1 == split[1].Length;
                    var cur = split[1][i];

                    if (cur == '"')
                    {
                        if (!quotafound)
                            quotafound = true;
                        else
                        {
                            valid = true;
                            quotafound = false;
                            skipNext = true;
                        }
                    }

                    if (cur == ' ' && !quotafound)
                        valid = true;

                    if (valid)
                    {
                        var arg = split[1].Substring(start, i - start + 1).Replace("\"", "");
                        while (arg.Length > 1)
                        {
                            if (arg[0] == ' ')
                                arg = arg.Remove(0, 1);

                            if (arg[arg.Length - 1] == ' ')
                                arg = arg.Remove(arg.Length - 1, 1);

                            if (arg[0] != ' ' && arg[arg.Length - 1] != ' ')
                                break;
                        }
                        argList.Add(arg);
                        start = i + 1;
                    }
                }
            }

            var args = argList.ToArray();
            var success = ItemSpawner.SendChatMessagePre(cmd, args) ||
                Prefix.SendChatMessagePre(cmd, args) ||
                Time.SendChatMessagePre(cmd, args) ||
                TileFinder.SendChatMessagePre(cmd, args) ||
                Home.SendChatMessagePre(cmd, args) ||
                God.SendChatMessagePre(cmd, args) ||
                Forcefield.SendChatMessagePre(cmd, args) ||
                Buffs.SendChatMessagePre(cmd, args) ||
                NoPickup.SendChatMessagePre(cmd) ||
                NPCSpawner.SendChatMessagePre(cmd, args) ||
                Autodelete.SendChatMessagePre(cmd) ||
                Ban_bypasser.SendChatMessagePre(cmd, args) ||
                AutoPot.SendChatMessagePre(cmd, args) ||
                NoFall.SendChatMessagePre(cmd) ||
                Inventory.SendchatMessagePre(cmd, args) ||
                ShootProjectiles.SendChatMessagePre(cmd, args) ||
                Teleport.SendChatMessagePre(cmd, args) ||
                Exploits.SendChatMessagePre(cmd, args) ||
                ESP.SendChatMessagePre(cmd, args) ||
                ItemSpammer.SendChatMessagePre(cmd, args) ||
                InfiniteAmmo.SendChatMessagePre(cmd) ||

                Debug.SendChatMessagePre(cmd, args);

            if (!success)
                Globals.Print("Command not found!");
            else
                LastUsedCommand = text;

            return true;
        }

        //TypeDefinition(Terraria.UI)
        public static bool RightClickItemPre(Item[] inv, int context = 0, int slot = 0)
        {
            return ItemReplica.RightClickItemPre(inv, context, slot);
        }

        //TypeDefinition(Terraria.Social.SocialAPI)
        public static bool LoadSteamPre()
        {
            Globals.IsCracked = System.Windows.Forms.MessageBox.Show("Do you want to play cracked?", Globals.Name, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes;
            return Globals.IsCracked;
        }
    }
}