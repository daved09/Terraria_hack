using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLS
{
    class NoFall
    {
        public static bool Enabled = true;

        public static void UpdateEquipsPost(Terraria.Player plr)
        {
            if (!Enabled)
                return;

            plr.noFallDmg = true;
        }

        public static bool SendChatMessagePre(string cmd)
        {
            if (cmd != "nofall" && cmd != "nf")
                return false;

            Enabled = !Enabled;
            Globals.Print("NoFall {0}", Enabled ? "Enabled" : "Disabled");
            return true;
        }
    }
}
