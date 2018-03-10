using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;

namespace SLS
{
    class Prefix
    {
        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "p" && cmd != "prefix")
                return false;

            if(args.Length < 1)
            {
                Globals.Print("Missing argument!");
                return true;
            }

            var best = args[0] == "best";

            var fldName = Debug.FindFieldName("PrefixName", args[0]);
            var prefix = null as FieldHelper.Field<int>;

            if(!best)
            {
                if (fldName == string.Empty)
                {
                    prefix = ItemSpawner.PrefixID.GetField<int>(args[0]);
                    if(prefix == null)
                    {
                        var id = 0;
                        if(!args[0].TryConvert(out id) || (ItemSpawner.PrefixID.GetFieldByValue(id)) == null)
                        {
                            Globals.Print("Invalid Prefix!");
                            return true;
                        }
                    }
                }
                else
                    prefix = ItemSpawner.PrefixID.GetField<int>(fldName);
            }
            
            var ply = Globals.GetLocalPlayer();
            var item = Main.mouseItem;
            if (item.type == 0)
                item = ply.inventory[ply.selectedItem];

            if (item.type == 0)
                Globals.Print("No item selected!");
            else
            {
                item.netDefaults(item.netID);
                if (!best)
                    Hooks.skipNextPrefix = true;
                item.Prefix(best ? 0 : prefix.Value);

                Globals.Print("Successfully ran Prefix({0}) on {1}", (best ? "Best" : prefix.Name), item.Name);
            }

            return true;
        }
    }
}
