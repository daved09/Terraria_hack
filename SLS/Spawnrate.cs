using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

using Microsoft.Xna.Framework.Input;

namespace SLS
{
    class Spawnrate
    {
        private static bool Multiplayer
        {
            get
            {
                var ret = Terraria.Main.netMode == 1;
                if (ret)
                    Globals.Print("Feature disabled in Multiplayer");
                return ret;
            }
        }

        private static FieldInfo defaultMaxSpawns;
        private static FieldInfo defaultSpawnRate;

        private static int internalSpawnRatePercent = 100;

        private static int oldMaxSpawns;
        private static int oldSpawnRate;

        public static bool Toggle = false;

        public static int DefaultMaxSpawns
        {
            get
            {
                return Convert.ToInt32(defaultMaxSpawns.GetValue(null));
            }
            set
            {
                defaultMaxSpawns.SetValue(null, value);
            }
        }

        public static int DefaultSpawnRate
        {
            get
            {
                return internalSpawnRatePercent;
                /*var rate = (int)defaultSpawnRate.GetValue(null);
                if (rate == 0)
                    return int.MaxValue;

                return 60000 / rate;*/
            }
            set
            {
                internalSpawnRatePercent = value;
                if(value == 0)
                    defaultSpawnRate.SetValue(null, int.MaxValue);
                else
                    defaultSpawnRate.SetValue(null, 60000 / value);
            }
        }

        public static void InitializePre()
        {
            defaultMaxSpawns = typeof(Terraria.NPC).GetField("defaultMaxSpawns", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            defaultSpawnRate = typeof(Terraria.NPC).GetField("defaultSpawnRate", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            Globals.RegisterHotKey(Keys.OemPlus, () =>
            {
                ModifySpawnRate(10);
            });

            Globals.RegisterHotKey(Keys.OemPlus, () =>
                {
                    ModifySpawnRate(50);
                }, shift: true);

            Globals.RegisterHotKey(Keys.OemMinus, () =>
                {
                    ModifySpawnRate(-10);
                });

            Globals.RegisterHotKey(Keys.OemMinus, () =>
                {
                    ModifySpawnRate(-50);
                }, shift: true);

            Globals.RegisterHotKey(Keys.OemPlus, () =>
                {
                    ModifySpawnLimit(5);
                }, alt: true);

            Globals.RegisterHotKey(Keys.OemPlus, () =>
                {
                    ModifySpawnLimit(25);
                }, alt: true, shift: true);

            Globals.RegisterHotKey(Keys.OemMinus, () =>
                {
                    ModifySpawnLimit(-5);
                }, alt: true);

            Globals.RegisterHotKey(Keys.OemMinus, () =>
                {
                    ModifySpawnLimit(-25);
                }, alt: true, shift: true);

            Globals.RegisterHotKey(Keys.N, () =>
                {
                    if(Multiplayer)
                        return;

                    Toggle = !Toggle;
                    if(Toggle)
                    {
                        oldMaxSpawns = DefaultMaxSpawns;
                        oldSpawnRate = DefaultSpawnRate;
                        DefaultMaxSpawns = 0;
                        DefaultSpawnRate = 0;
                        KillAllHostile();
                    }
                    else
                    {
                        DefaultMaxSpawns = oldMaxSpawns;
                        DefaultSpawnRate = oldSpawnRate;
                    }
                    Globals.Print("Spawns {0}", !Toggle ? string.Format("Enabled with SpawnRate: {0}%, MaxSpawns: {1}", DefaultSpawnRate, DefaultMaxSpawns) : "Disabled");
                });
        }

        public static void ModifySpawnRate(int amount)
        {
            if (Multiplayer)
                return;

            var buffer = DefaultSpawnRate + amount;
            if (buffer < 0)
                buffer = 0;
            else if (buffer > 1000)
                buffer = 1000;

            Globals.Print("Changed SpawnRate from {0}% to {1}%", DefaultSpawnRate, buffer);
            DefaultSpawnRate = buffer;
        }

        public static void ModifySpawnLimit(int amount)
        {
            if (Multiplayer)
                return;

            var buffer = DefaultMaxSpawns + amount;

            if (DefaultMaxSpawns == 0)
                KillAllHostile();

            if (buffer > 150)
                buffer = 150;
            else if (buffer < 5)
                buffer = 5;


            Globals.Print("Changed MaxSpawns from {0} to {1}", DefaultMaxSpawns, buffer);
            DefaultMaxSpawns = buffer;
        }

        public static void KillAllHostile()
        {
            foreach(var x in Terraria.Main.npc)
            {
                if (!x.townNPC)
                    x.life = 0;
            }
        }
    }
}
