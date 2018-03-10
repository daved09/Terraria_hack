using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLS
{
    class NoPickup
    {
        public static bool Enabled = false;

        public static bool SendChatMessagePre(string cmd)
        {
            if (cmd != "nopickup" && cmd != "np")
                return false;

            Enabled = !Enabled;
            Globals.Print("NoPickup {0}", Enabled ? "Enabled" : "Disabled");
            return true;
        }
    }
}
