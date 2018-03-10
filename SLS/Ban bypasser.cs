using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLS
{
    class Ban_bypasser
    {
        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            switch(cmd)
            {
                case "changeuuid":
                    var guid = Guid.NewGuid().ToString();
                    Terraria.Main.clientUUID = guid;
                    Terraria.Main.Configuration.Put("ClientUUID", guid);
                    Terraria.Main.Configuration.Save();

                    Globals.Print("ClientUUID changed to {0}", guid);
                    break;

                case "name":
                    Globals.GetLocalPlayer().name = args[0];
                    Globals.Print("Name changed to {0}", args[0]);
                    break;

                default:
                    return false;
            }

            return true;
        }
    }
}
