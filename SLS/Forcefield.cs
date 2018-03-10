using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SLS
{
    class Forcefield
    {
        static DateTime Last = DateTime.Now;

        public static FieldHelper ProjectileID = new FieldHelper(typeof(ProjectileID));
        public static FieldHelper.Field<short> projectile = ProjectileID.GetField<short>("LightDisc");

        public static bool Enabled = false;
        public static bool InstaKill = false;
        public static bool CheckCollision = true;
        public static bool CalcAttackDelay = true;

        public static float Range = 750f;
        public static int Delay = 500;

        public static string ForceFieldString()
        {
            return string.Format("[{0}]", string.Join("|", new[]
            {
                "Range: " + Range,
                InstaKill ? "Instakill" : "Scaled",
                //projectile.GetText("ProjectileName"),
                Delay + "ms" + (CalcAttackDelay ? "(Auto)" : ""),
                "CC: " + (CheckCollision ? "On" : "Off")
            }));
        }

        public static TimeSpan Next
        {
            get
            {
                return DateTime.Now - Last;
            }
        }

        public static void InitializePre()
        {
            Globals.RegisterHotKey(Microsoft.Xna.Framework.Input.Keys.C, () =>
                {
                    SendChatMessagePre("ff", new string[0]);
                }, shift: true);
        }

        public static void UpdatePlayerPost(Player plr)
        {
            if (!Enabled)
                return;

            var maxDamage = 0;
            var maxKnockback = 0f;

            foreach(var x in plr.inventory)
            {
                switch(x.type)
                {
                    case ItemID.CopperCoin:
                    case ItemID.SilverCoin:
                    case ItemID.GoldCoin:
                    case ItemID.PlatinumCoin:
                    case ItemID.Grenade:
                        continue;
                }

                var dmg = plr.GetWeaponDamage(x);
                var kb = plr.GetWeaponKnockback(x, x.knockBack);

                if (dmg > maxDamage)
                    maxDamage = dmg;

                if (kb > maxKnockback)
                    maxKnockback = kb;
            }

            var closest = null as NPC;

            if(CalcAttackDelay)
                Delay = (int)(500 * plr.meleeSpeed);

            foreach(var x in Main.npc.Where(y => plr.Distance(y.position) <= Range).OrderBy(z => plr.Distance(z.position)))
            {
                if (Next < TimeSpan.FromMilliseconds(Delay))
                    break;

                if (!x.active || x.townNPC || x.friendly || x.dontTakeDamage || x.immortal || x.type == 0 || x.life < 1)
                    continue;

                if (CheckCollision && /*!Collision.CanHit(plr.position, plr.width, plr.height, x.position, x.width, x.height)*/ !plr.CanHit(x))
                    continue;

                closest = x;
                break;
            }

            if(closest != null)
            {
                var dmg = (int)(InstaKill ? closest.defense * 0.5 + closest.life : maxDamage);
                var num = Projectile.NewProjectile(plr.position, closest.DirectionFrom(plr.position) * 50f, projectile.Value, dmg, maxKnockback, plr.whoAmI);
                var proj = Main.projectile[num];
                proj.tileCollide = false;
                //plr.addDPS(dmg);

                Last = DateTime.Now;
            }
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "forcefield" && cmd != "ff")
                return false;

            if (args.Length < 1)
            {
                Enabled = !Enabled;
                Globals.Print("Forcefield {0}", Enabled ? "Enabled" : "Disabled");
            }
            else if (args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "range":
                    case "r":
                        if(args.Length < 2)
                        {
                            Globals.Print("Missing argument: Range");
                            return true;
                        }

                        float range;
                        if (!float.TryParse(args[1], out range))
                            Globals.Print("Invalid range specified!");
                        else
                        {
                            Globals.Print("Range changed from {0} to {1}", Range, range);
                            Range = range;
                        }
                        break;

                    case "instakill":
                    case "ik":
                        InstaKill = !InstaKill;
                        Globals.Print("Instakill {0}", InstaKill ? "Enabled" : "Disabled");
                        break;

                    case "calcattackdelay":
                    case "cad":
                        CalcAttackDelay = !CalcAttackDelay;
                        Globals.Print("Calulating attack delay {0}", CalcAttackDelay ? "Enabled" : "Disabled");
                        break;

                    case "checkcollision":
                    case "cc":
                        CheckCollision = !CheckCollision;
                        Globals.Print("Collision checking {0}", CheckCollision ? "Enabled" : "Disabled");
                        break;

                    case "delay":
                    case "d":
                        if(args.Length < 2)
                        {
                            Globals.Print("Missing argument: Delay");
                            return true;
                        }
                        int delay;
                        if(!args[1].TryConvert(out delay))
                        {
                            Globals.Print("Invalid delay specified!");
                            return true;
                        }
                        Delay = delay;
                        break;
                }
            }

            return true;
        }
    }
}
