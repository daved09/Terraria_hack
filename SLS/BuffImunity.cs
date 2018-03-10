using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;

namespace SLS
{
    class BuffImunity
    {
        static int[] Buffs = new[]
            {
                BuffID.Blackout,
                BuffID.Bleeding,
                BuffID.BrokenArmor,
                BuffID.Burning,
                BuffID.ChaosState,
                BuffID.Confused,
                BuffID.Cursed,
                BuffID.CursedInferno,
                BuffID.Electrified,
                BuffID.Frozen,
                BuffID.Horrified,
                BuffID.ManaSickness,
                BuffID.MoonLeech,
                BuffID.NoBuilding,
                BuffID.Obstructed,
                BuffID.Oiled,
                BuffID.OnFire,
                BuffID.Poisoned,
                BuffID.PotionSickness,
                BuffID.Slow,
                BuffID.Stinky,
                BuffID.Stoned,
                BuffID.Suffocation,
                BuffID.TheTongue,
                BuffID.Weak,
                BuffID.Webbed,
            };

        public static void UpdateBuffsPost(Player plr)
        {
            return;
            plr.buffImmune[BuffID.PotionSickness] = true;
            plr.buffImmune[BuffID.ManaSickness] = true;
            OldUpdateBuffs(plr);
        }

        protected static void OldUpdateBuffs(Player plr)
        {
            foreach (var x in Buffs)
                plr.buffImmune[x] = true;

            plr.potionDelay = 0;
        }
    }
}
