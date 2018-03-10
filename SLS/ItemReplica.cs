using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Microsoft.Xna.Framework.Input;

namespace SLS
{
    class ItemReplica
    {
        enum EContext
        {
            InventoryItem,
            InventoryCoin,
            InventoryAmmo,
            ChestItem,
            BankItem,
            TrashItem,
            EquipArmor,
            EquipArmorVanity,
            EquipAccessory,
            EquipAccessoryVanity,
            EquipDye,
            EquipGrapple,
            EquipMount,
            EquipMinecart,
            EquipPet,
            EquipLight,
            Length
        }

        public static Keys ToggleKey = Keys.R;

        public static bool RightClickItemPre(Terraria.Item[] inv, int context, int slot)
        {
            if(!Main.keyState.IsKeyDown(ToggleKey))
                return false;

            var item = inv[slot];
            item.newAndShiny = false;

            if(Main.stackSplit <= 1 && Main.mouseRight && context < (int)EContext.Length)
            {
                if (Main.mouseItem.type == 0)
                {
                    Main.mouseItem = item.Clone();
                    Main.mouseItem.stack = 0;
                }
                else if (!Main.mouseItem.IsTheSameAs(item))
                    return false;

                if(Main.mouseItem.stack < Main.mouseItem.maxStack)
                    Main.mouseItem.stack++;
                Recipe.FindRecipes();

                if((EContext)context == EContext.ChestItem && Main.netMode == 1)
                    NetMessage.SendData(32, -1, -1, Terraria.Localization.NetworkText.Empty, Main.player[Main.myPlayer].chest, (float)slot, 0f, 0f, 0, 0, 0);

                return true;
            }

            return false;
        }
    }
}
