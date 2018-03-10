using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;

namespace SLS
{
    class ItemSpawner
    {
        public static FieldHelper ItemID = new FieldHelper(typeof(ItemID));
        public static FieldHelper PrefixID = new FieldHelper(typeof(PrefixID));

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            Player plr = null;

            if (cmd == "item" || cmd == "i")
                plr = Globals.GetLocalPlayer();
            else if (cmd == "give" || cmd == "g")
            {
                if(!PlayerUtil.TryGetPlayer(args[0], out plr))
                {
                    Globals.Print("Player {0} not found!", args[0]);
                    return true;
                }
                args = args.Where((x, y) => y != 0).ToArray();
            }
            else
                return false;

            if (args.Length < 1)
            {
                Globals.Print("Missing arguments!");
                return true;
            }

            var fldName = Debug.FindFieldName("ItemName.", args[0]);
            var item = null as FieldHelper.Field<short>;

            if(fldName == string.Empty)
                if (!ItemID.Contains(args[0]))
                {
                    short id = 0;
                    if (!args[0].TryConvert(out id) || (item = ItemID.GetFieldByValue(id)) == null)
                    {
                        Globals.Print("Item not found!");
                        return true;
                    }
                
                }
                else
                    item = ItemID.GetField<short>(args[0]);
            else
                item = ItemID.GetField<short>(fldName);

            var count = 1;
            if(args.Length > 1 && !args[1].TryConvert(out count))
            {
                Globals.Print("Invalid item count!");
                return true;
            }

            var prefix = null as FieldHelper.Field<int>;
            if(args.Length > 2)
            {
                if (!PrefixID.Contains(args[2]))
                {
                    var id = 0;
                    if (!args[2].TryConvert(out id) || (prefix = PrefixID.GetFieldByValue(id)) == null)
                    {
                        Globals.Print("Invalid prefix!");
                        return true;
                    }
                }
                else
                    prefix = PrefixID.GetField<int>(args[2]);
            }

            if(Globals.IsLocalPlayer(plr))
            {
                Hooks.skipNextPrefix = true;
                Hooks.skipNextGetItem = true;

                foreach(var x in plr.inventory)
                {
                    if(x.netID == 0)
                    {
                        x.netDefaults(item.Value);
                        if (prefix != null)
                            x.Prefix(prefix.Value);
                        x.stack = count;
                        break;
                    }
                }
            }
            else
            {
                var num = Item.NewItem((int)plr.position.X, (int)plr.position.Y, plr.width, plr.height, item.Value, count, false, (prefix == null ? 0 : prefix.Value), true);
                if (Main.netMode == 1)
                    NetMessage.SendData(21, -1, -1, null, num, 1f, 0f, 0f, 0, 0, 0);
            }

            Globals.Print("Gave {0} {1} to {2}", count, (prefix == null ? "" : prefix.Name + " ") + Debug.FindFieldLangname("ItemName." + item.Name) + (count > 1 ? "s" : ""), plr.name);

            return true;
        }
    }
}
