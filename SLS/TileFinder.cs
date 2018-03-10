using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;

using Microsoft.Xna.Framework;

namespace SLS
{
    class TileFinder
    {
        public static FieldHelper TileID = new FieldHelper(typeof(TileID));

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            if (cmd != "findtile" && cmd != "ft" && cmd != "find")
                return false;

            if(args.Length < 1)
            {
                Globals.Print("Missing arguments!");
                return true;
            }

            var fldName = Debug.FindFieldName("MapObject.", args[0]);
            var tile = null as FieldHelper.Field<ushort>;
            if (fldName == string.Empty)
                if (!TileID.Contains(args[0]))
                {
                    ushort id = 0;
                    if (!args[0].TryConvert(out id) || (tile = TileID.GetFieldByValue(id)) == null)
                    {
                        Globals.Print("TileID not found!");
                        return true;
                    }
                }
                else
                    tile = TileID.GetField<ushort>(args[0]);
            else
                tile = TileID.GetField<ushort>(fldName);

            var option = 0;

            if(args.Length > 1 && !args[1].TryConvert(out option))
            {
                Globals.Print("Invalid sub-id!");
                return true;
            }

            var tileLookup = Terraria.Map.MapHelper.TileToLookup(tile.Value, option);

            var found = false;

            for (var x = 0; x < Main.maxTilesX; x++)
            {
                for (var y = 0; y < Main.maxTilesY; y++)
                {
                    if (Terraria.Map.MapHelper.CreateMapTile(x, y, (byte)111).Type == tileLookup)
                    {
                        found = true;
                        var ply = Globals.GetLocalPlayer();
                        var newPos = new Vector2(x * 16, y * 16);
                        ply.position = new Vector2(x * 16, y * 16);
                        ply.velocity = Vector2.Zero;
                        ply.fallStart = (int)(ply.position.Y / 16f);
                        break;
                    }
                }
                if (found)
                    break;
            }

            if (!found)
                Globals.Print("Tile {0} not on Map!", Debug.FindFieldLangname("MapObject." + tile.Name));

            return true;
        }
    }
}
