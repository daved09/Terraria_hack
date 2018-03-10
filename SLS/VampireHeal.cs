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
    class VampireHeal
    {
        public static bool Enabled = true;

        static List<Tuple<DateTime, int>> Last = new List<Tuple<DateTime, int>>();

        public static float HealAmount = 0.15f;

        public static void UpdatePlayerPost(Player plr)
        {
            plr.lifeSteal = 1000000;
        }

        public static void Projectile_DamagePost(Projectile projectile, Player owner)
        {
            if (!Enabled)
                return;

            if (projectile.type == 0)
                return;

            var tpl = Last.FirstOrDefault(x => x.Item2 == projectile.type);
            var ind = Last.IndexOf(tpl);
            if(tpl == null)
            {
                tpl = new Tuple<DateTime, int>(new DateTime(0), projectile.type);
                Last.Add(tpl);
            }

            foreach(var x in Main.npc)
            {
                if(DateTime.Now - tpl.Item1 < TimeSpan.FromMilliseconds(500))
                    break;

                if (owner.statLife >= owner.statLifeMax2)
                    break;

                if (projectile.Colliding(projectile.Hitbox, x.Hitbox))
                {
                    var heal = projectile.damage * HealAmount; //0.075f;
                    //if (heal < 1)
                        //continue;

                    var maxAdd = Math.Min(heal, owner.statLifeMax2 - owner.statLife);
                    if (maxAdd < 1)
                        continue;
 
                    owner.statLife += (int)maxAdd;
                    owner.HealEffect((int)maxAdd, true);
                    //projectile.vampireHeal(projectile.damage, x.position);
                    //Projectile.NewProjectile(x.position, Vector2.Zero, ProjectileID.VampireHeal, 0, 0f, owner.whoAmI, (float)owner.whoAmI, heal);
                    Last[ind] = new Tuple<DateTime, int>(DateTime.Now, tpl.Item2);
                    break;
                }
            }
        }
    }
}
