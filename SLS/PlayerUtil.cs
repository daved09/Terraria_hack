using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class PlayerUtil
    {
        public static bool TryGetPlayer(string name, out Player plr)
        {
            plr = Main.player.FirstOrDefault(x => x.name.ToLower().Contains(name.ToLower()));
            if(plr == null)
                return false;

            return true;
        }
    }
}
