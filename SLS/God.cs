using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLS
{
    class God
    {
        public enum EMode
        {
            Normal, Demi
        }

        public static EMode Mode = EMode.Normal;
        public static bool Enabled = false;

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "god")
                return false;

            if(args.Length < 1)
            {
                Enabled = !Enabled;
                Globals.Print("God Mode {0}", Enabled ? "Enabled" : "Disabled");
            }
            else if(args.Length > 1 && (args[0] == "mode" || args[0] == "m"))
            {
                var names = Enum.GetNames(typeof(EMode));
                var first = names.FirstOrDefault(x => x.ToLower() == args[1]);
                if(first == null)
                {
                    Globals.Print("Invalid mode!");
                    return true;
                }

                Mode = (EMode)Enum.Parse(typeof(EMode), first);
                Globals.Print("Mode set to {0}", first);
            }
            else
                Globals.Print("Invalid argument count!");

            return true;
        }

        public static void HurtMeMid(Terraria.Player plr, ref double ret)
        {
            if (!Enabled)
                return;

            if (Mode == EMode.Normal)
                ret = 0;
            else if (Mode == EMode.Demi)
                ret = ret < plr.statLife ? ret : plr.statLife - 1;
        }
    }
}
