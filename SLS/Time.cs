using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class Time
    {
        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "time" && cmd != "t")
                return false;

            if(args.Length < 1)
            {
                Globals.Print("No time specified!");
                return true;
            }

            switch(args[0])
            {
                case "morning:":
                case "m":
                    Main.dayTime = false;
                    Main.time = 32401.0;
                    break;

                case "day":
                case "d":
                    Main.dayTime = true;
                    Main.time = 27000.0;
                    break;

                case "afternoon":
                case "a":
                    Main.dayTime = true;
                    Main.time = 54001.0;
                    break;

                case "night":
                case "n":
                    Main.dayTime = false;
                    Main.time = 16200.0;
                    break;
            }

            return true;
        }
    }
}
