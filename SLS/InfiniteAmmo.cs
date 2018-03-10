using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLS
{
    class InfiniteAmmo
    {
        public static bool Enabled = true;

        public static void PickAmmoPre(ref bool dontConsume)
        {
            if (Enabled)
                dontConsume = true;
        }

        public static bool SendChatMessagePre(string cmd)
        {
            switch(cmd)
            {
                case "infammo":
                case "infiniteammo":
                case "ia":
                    Enabled = !Enabled;
                    Globals.Print("Infinite ammo {0}", Enabled ? "Enabled" : "Disabled");
                    return true;

                default:
                    return false;
            }
        }
    }
}
