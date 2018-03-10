using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class NPCSpawner
    {
        public static FieldHelper NPCID = new FieldHelper(typeof(Terraria.ID.NPCID));

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "npc" && cmd != "n")
                return false;

            if(args.Length < 1)
            {
                Globals.Print("Invalid argument count!");
                return true;
            }

            var fldName = Debug.FindFieldName("NPCName.", args[0]);
            var npc = null as FieldHelper.Field<short>;
            if (fldName == string.Empty)
                if (!NPCID.Contains(args[0]))
                {
                    short id = 0;
                    if (!args[0].TryConvert(out id) || (npc = NPCID.GetFieldByValue(id)) == null)
                    {
                        Globals.Print("Invalid npc name!");
                        return true;
                    }
                }
                else
                    npc = NPCID.GetField<short>(args[0]);
            else
                npc = NPCID.GetField<short>(fldName);

            var count = 1;
            if(args.Length > 1 && !args[1].TryConvert(out count))
            {
                Globals.Print("Invalid count!");
                return true;
            }

            var ply = Globals.GetLocalPlayer();
            try
            {
                for (var i = 0; i < count; i++)
                    NPC.NewNPC((int)ply.position.X, (int)ply.position.Y + 10, npc.Value);
            }
            catch(Exception e)
            {
                Globals.Print("An error occured: {0}", e);
            }
            Globals.Print("Successfully created {0} {1}", count + (Main.netMode == 1 ? " clientsided" : ""), Debug.FindFieldLangname("NPCName." + npc.Name) + (count > 1 ? "s" : ""));
            return true;
        }
    }
}
