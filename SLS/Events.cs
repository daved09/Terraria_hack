using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

using Terraria;
using Terraria.Localization;
using Terraria.GameContent.Achievements;

using Microsoft.Xna.Framework.Input;

namespace SLS
{
    class Events
    {
        private static bool Multiplayer
        {
            get
            {
                var ret = false;
                if (ret |= (Main.netMode == 1))
                    Globals.Print("Feature disabled in Multiplayer");
                else if (ret |= (Main.netMode == 2))
                    Globals.Print("Feature disabled while hosting");
                return ret;
            }
        }

        private static MethodInfo triggerLunarApocalypse;
        private static FieldInfo spawnMeteor;
        private static MethodInfo dropMeteor;
        private static bool SpawnMeteor
        {
            get { return (bool)spawnMeteor.GetValue(null); }
            set { spawnMeteor.SetValue(null, value); }
        }
        
        public static void InitializePre()
        {
            var worldGen = typeof(Terraria.WorldGen);//Assembly.GetEntryAssembly().GetType("Terraria.WorldGen");
            triggerLunarApocalypse = worldGen.GetMethod("TriggerLunarApocalypse");
            spawnMeteor = worldGen.GetField("spawnMeteor");
            dropMeteor = worldGen.GetMethod("dropMeteor");

            Globals.RegisterHotKey(Keys.NumPad2, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.invasionType > 0)
                    Main.invasionSize = 0;
                else
                    Main.StartInvasion(1);
            });
            Globals.RegisterHotKey(Keys.NumPad3, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.invasionType > 0)
                    Main.invasionSize = 0;
                else
                    Main.StartInvasion(2);
            });
            Globals.RegisterHotKey(Keys.NumPad4, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.invasionType > 0)
                    Main.invasionSize = 0;
                else
                    Main.StartInvasion(3);
            });
            Globals.RegisterHotKey(Keys.NumPad8, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.invasionType > 0)
                    Main.invasionSize = 0;
                else
                    Main.StartInvasion(4);
            });
            Globals.RegisterHotKey(Keys.NumPad6, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.pumpkinMoon)
                    Main.stopMoonEvent();
                else
                    Main.startPumpkinMoon();
            });
            Globals.RegisterHotKey(Keys.NumPad7, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.snowMoon)
                    Main.stopMoonEvent();
                else
                    Main.startSnowMoon();
            });
            Globals.RegisterHotKey(Keys.NumPad9, () =>
            {
                if (Multiplayer)
                    return;

                if (Terraria.NPC.LunarApocalypseIsUp || Terraria.NPC.AnyNPCs(398))
                    StopLunarEvent();
                else
                    TriggerLunarApocalypse();
            });
            Globals.RegisterHotKey(Keys.Add, () =>
            {
                if (Multiplayer)
                    return;

                if (Terraria.NPC.LunarApocalypseIsUp || Terraria.NPC.AnyNPCs(398))
                    StopLunarEvent();
                else
                    SpawnMoonLord();
            });
            Globals.RegisterHotKey(Keys.NumPad1, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.bloodMoon)
                    Main.bloodMoon = false;
                else
                    TriggerBloodMoon();
            });
            Globals.RegisterHotKey(Keys.NumPad5, () =>
            {
                if (Multiplayer)
                    return;

                if (Main.eclipse)
                    Main.eclipse = false;
                else
                    TriggerEclipse();
            });
            Globals.RegisterHotKey(Keys.NumPad0, () =>
            {
                if (Multiplayer)
                    return;

                SpawnMeteor = false;
                DropMeteor();
            });
        }

        private static void DropMeteor()
        {
            dropMeteor.Invoke(null, null);
        }

        private static void TriggerLunarApocalypse()
        {
            triggerLunarApocalypse.Invoke(null, null);
        }

        private static void TriggerEclipse()
        {
            Main.eclipse = true;
            AchievementsHelper.NotifyProgressionEvent(2);
            if (Main.netMode == 0)
            {
                Main.NewText(Language.GetText("LegacyMisc.20").Value, 50, 255, 130, false);
            }
        }

        private static void TriggerBloodMoon()
        {
            Main.bloodMoon = true;
            AchievementsHelper.NotifyProgressionEvent(4);
            if (Main.netMode == 0)
            {
                Main.NewText(Language.GetText("LegacyMisc.8").Value, 50, 255, 130, false);
            }
        }

        private static void SpawnMoonLord()
        {
            Terraria.NPC.MoonLordCountdown = 3600;
            NetMessage.SendData(103, -1, -1, Terraria.Localization.NetworkText.Empty, Terraria.NPC.MoonLordCountdown, 0f, 0f, 0f, 0, 0, 0);
            if (Main.netMode == 0)
            {
                Main.NewText(Language.GetText("LegacyMisc.52").Value, 50, 255, 130, false);
                return;
            }
        }

        private static void StopLunarEvent()
        {
            Main.NewText("Stopped lunar event!", 50, 255, 130, false);
            Terraria.NPC.LunarApocalypseIsUp = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    switch (Main.npc[i].type)
                    {
                        case 398: // Moon Lord
                        case 517: // Tower
                        case 422: // Tower
                        case 507: // Tower
                        case 493: // Tower
                            Main.npc[i].life = 0;
                            break;
                    }
                }
            }
        }
    }
}
