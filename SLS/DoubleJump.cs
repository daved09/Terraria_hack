using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class DoubleJump
    {
        public static void UpdateEquipsPost(Player plr)
        {
            plr.doubleJumpBlizzard = true;
            plr.doubleJumpCloud = true;
            plr.doubleJumpFart = true;
            plr.doubleJumpSail = true;
            plr.doubleJumpSandstorm = true;
            plr.doubleJumpUnicorn = true;
        }
    }
}
