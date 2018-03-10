using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Terraria;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SLS
{
    class BetterTooltips
    {
        private static FieldInfo _cpItem;

        private static Item cpItem
        {
            get
            {
                return (Item)_cpItem.GetValue(null);
            }
            set
            {
                _cpItem.SetValue(null, value);
            }
        }

        private const int toolTipDistance = 6;

        public static bool MouseText_DrawItemTooltipPre(int rare, byte diff, int x, int y)
        {
            if (_cpItem == null)
                _cpItem = typeof(Main).GetField("cpItem", BindingFlags.Static | BindingFlags.NonPublic);

            MouseText_DrawItemTooltip(rare, diff, x, y);
            return true;
        }

        private static void MouseText_DrawItemTooltip(int rare, byte diff, int X, int Y)
        {
            Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
            int num = -1;
            rare = Main.HoverItem.rare;
            float knockBack = Main.HoverItem.knockBack;
            float num2 = 1f;
            if (Main.HoverItem.melee && Main.player[Main.myPlayer].kbGlove)
            {
                num2 += 1f;
            }
            if (Main.player[Main.myPlayer].kbBuff)
            {
                num2 += 0.5f;
            }
            if (num2 != 1f)
            {
                Main.HoverItem.knockBack *= num2;
            }
            if (Main.HoverItem.ranged && Main.player[Main.myPlayer].shroomiteStealth)
            {
                Main.HoverItem.knockBack *= 1f + (1f - Main.player[Main.myPlayer].stealth) * 0.5f;
            }
            int num3 = 20;
            int num4 = 1;
            string[] array = new string[num3];
            bool[] array2 = new bool[num3];
            bool[] array3 = new bool[num3];
            for (int i = 0; i < num3; i++)
            {
                array2[i] = false;
                array3[i] = false;
            }
            array[0] = "[" + rare + "]" + Main.HoverItem.HoverName;
            if (Main.HoverItem.favorited)
            {
                array[num4++] = Lang.tip[56].Value;
                array[num4++] = Lang.tip[57].Value;
            }
            if (Main.HoverItem.social)
            {
                array[num4] = Lang.tip[0].Value;
                num4++;
                array[num4] = Lang.tip[1].Value;
                num4++;
            }
            else
            {
                if (Main.HoverItem.damage > 0 && (!Main.HoverItem.notAmmo || Main.HoverItem.useStyle > 0) && (Main.HoverItem.type < 71 || Main.HoverItem.type > 74 || Main.player[Main.myPlayer].HasItem(905)))
                {
                    float num5 = 5E-06f;
                    int damage = Main.HoverItem.damage;
                    if (Main.HoverItem.melee)
                    {
                        array[num4] = string.Concat((int)(Main.player[Main.myPlayer].meleeDamage * (float)damage + num5));
                        string[] expr_25C_cp_0 = array;
                        int expr_25C_cp_1 = num4;
                        expr_25C_cp_0[expr_25C_cp_1] += Lang.tip[2].Value;
                    }
                    else if (Main.HoverItem.ranged)
                    {
                        float num6 = (float)damage * Main.player[Main.myPlayer].rangedDamage;
                        if (Main.HoverItem.useAmmo == Terraria.ID.AmmoID.Arrow || Main.HoverItem.useAmmo == Terraria.ID.AmmoID.Stake)
                        {
                            num6 *= Main.player[Main.myPlayer].arrowDamage;
                        }
                        if (Main.HoverItem.useAmmo == Terraria.ID.AmmoID.Arrow && Main.player[Main.myPlayer].archery)
                        {
                            num6 *= 1.2f;
                        }
                        if (Main.HoverItem.useAmmo == Terraria.ID.AmmoID.Bullet || Main.HoverItem.useAmmo == Terraria.ID.AmmoID.CandyCorn)
                        {
                            num6 *= Main.player[Main.myPlayer].bulletDamage;
                        }
                        if (Main.HoverItem.useAmmo == Terraria.ID.AmmoID.Rocket || Main.HoverItem.useAmmo == Terraria.ID.AmmoID.StyngerBolt || Main.HoverItem.useAmmo == Terraria.ID.AmmoID.JackOLantern || Main.HoverItem.useAmmo == Terraria.ID.AmmoID.NailFriendly)
                        {
                            num6 *= Main.player[Main.myPlayer].rocketDamage;
                        }
                        array[num4] = string.Concat((int)(num6 + num5));
                        string[] expr_3AC_cp_0 = array;
                        int expr_3AC_cp_1 = num4;
                        expr_3AC_cp_0[expr_3AC_cp_1] += Lang.tip[3].Value;
                    }
                    else if (Main.HoverItem.magic)
                    {
                        array[num4] = string.Concat((int)(Main.player[Main.myPlayer].magicDamage * (float)damage + num5));
                        string[] expr_401_cp_0 = array;
                        int expr_401_cp_1 = num4;
                        expr_401_cp_0[expr_401_cp_1] += Lang.tip[4].Value;
                    }
                    else if (Main.HoverItem.thrown)
                    {
                        array[num4] = string.Concat((int)(Main.player[Main.myPlayer].thrownDamage * (float)damage + num5));
                        string[] expr_456_cp_0 = array;
                        int expr_456_cp_1 = num4;
                        expr_456_cp_0[expr_456_cp_1] += Lang.tip[58].Value;
                    }
                    else if (Main.HoverItem.summon)
                    {
                        if (Main.HoverItem.type == 3829 || Main.HoverItem.type == 3830 || Main.HoverItem.type == 3831)
                        {
                            array[num4] = string.Concat((int)((Main.player[Main.myPlayer].minionDamage * (float)damage + num5) * 3f));
                        }
                        else
                        {
                            array[num4] = string.Concat((int)(Main.player[Main.myPlayer].minionDamage * (float)damage + num5));
                        }
                        string[] expr_511_cp_0 = array;
                        int expr_511_cp_1 = num4;
                        expr_511_cp_0[expr_511_cp_1] += Lang.tip[53].Value;
                    }
                    else
                    {
                        array[num4] = string.Concat(damage);
                        string[] expr_542_cp_0 = array;
                        int expr_542_cp_1 = num4;
                        expr_542_cp_0[expr_542_cp_1] += Lang.tip[55].Value;
                    }
                    {
                        var split = array[num4].Split(new[] { ' ' });
                        array[num4] = string.Format("Dmg: {0} ({1})", split[0], split[1][0].ToString().ToUpper() + split[1].Remove(0, 1));
                    }
                    num4++;
                    if (Main.HoverItem.melee)
                    {
                        int num7 = Main.player[Main.myPlayer].meleeCrit - Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit + Main.HoverItem.crit;
                        array[num4] = "Crit: (" + num7 + "/100)%";
                        num4++;
                    }
                    else if (Main.HoverItem.ranged)
                    {
                        int num8 = Main.player[Main.myPlayer].rangedCrit - Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit + Main.HoverItem.crit;
                        array[num4] = "Crit: (" + num8 + "/100)%";
                        num4++;
                    }
                    else if (Main.HoverItem.magic)
                    {
                        int num9 = Main.player[Main.myPlayer].magicCrit - Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit + Main.HoverItem.crit;
                        array[num4] = "Crit: (" + num9 + "/100)%";
                        num4++;
                    }
                    else if (Main.HoverItem.thrown)
                    {
                        int num10 = Main.player[Main.myPlayer].thrownCrit - Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].crit + Main.HoverItem.crit;
                        array[num4] = "Crit: (" + num10 + "/100)%";
                        num4++;
                    }
                    if (Main.HoverItem.useStyle > 0 && !Main.HoverItem.summon)
                    {
                        if (Main.HoverItem.useAnimation <= 8)
                        {
                            array[num4] = Lang.tip[6].Value;
                        }
                        else if (Main.HoverItem.useAnimation <= 20)
                        {
                            array[num4] = Lang.tip[7].Value;
                        }
                        else if (Main.HoverItem.useAnimation <= 25)
                        {
                            array[num4] = Lang.tip[8].Value;
                        }
                        else if (Main.HoverItem.useAnimation <= 30)
                        {
                            array[num4] = Lang.tip[9].Value;
                        }
                        else if (Main.HoverItem.useAnimation <= 35)
                        {
                            array[num4] = Lang.tip[10].Value;
                        }
                        else if (Main.HoverItem.useAnimation <= 45)
                        {
                            array[num4] = Lang.tip[11].Value;
                        }
                        else if (Main.HoverItem.useAnimation <= 55)
                        {
                            array[num4] = Lang.tip[12].Value;
                        }
                        else
                        {
                            array[num4] = Lang.tip[13].Value;
                        }
                        array[num4] = string.Format("{0} ({1})", "Speed: " + Main.HoverItem.useAnimation, array[num4]);
                        num4++;
                    }
                    float num11 = Main.HoverItem.knockBack;
                    if (Main.HoverItem.summon)
                    {
                        num11 += Main.player[Main.myPlayer].minionKB;
                    }
                    if ((Main.player[Main.myPlayer].magicQuiver && Main.HoverItem.useAmmo == Terraria.ID.AmmoID.Arrow) || Main.HoverItem.useAmmo == Terraria.ID.AmmoID.Stake)
                    {
                        num11 = (float)((int)(num11 * 1.1f));
                    }
                    if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == 3106 && Main.HoverItem.type == 3106)
                    {
                        num11 += num11 * (1f - Main.player[Main.myPlayer].stealth);
                    }
                    if (num11 == 0f)
                    {
                        array[num4] = Lang.tip[14].Value;
                    }
                    else if ((double)num11 <= 1.5)
                    {
                        array[num4] = Lang.tip[15].Value;
                    }
                    else if (num11 <= 3f)
                    {
                        array[num4] = Lang.tip[16].Value;
                    }
                    else if (num11 <= 4f)
                    {
                        array[num4] = Lang.tip[17].Value;
                    }
                    else if (num11 <= 6f)
                    {
                        array[num4] = Lang.tip[18].Value;
                    }
                    else if (num11 <= 7f)
                    {
                        array[num4] = Lang.tip[19].Value;
                    }
                    else if (num11 <= 9f)
                    {
                        array[num4] = Lang.tip[20].Value;
                    }
                    else if (num11 <= 11f)
                    {
                        array[num4] = Lang.tip[21].Value;
                    }
                    else
                    {
                        array[num4] = Lang.tip[22].Value;
                    }
                    array[num4] = string.Format("{0} ({1})", "KB: " + num11, array[num4]);
                    num4++;
                }
                if (Main.HoverItem.fishingPole > 0)
                {
                    array[num4] = Terraria.Localization.Language.GetTextValue("GameUI.PrecentFishingPower", Main.HoverItem.fishingPole);
                    num4++;
                    array[num4] = Terraria.Localization.Language.GetTextValue("GameUI.BaitRequired");
                    num4++;
                }
                if (Main.HoverItem.bait > 0)
                {
                    array[num4] = Terraria.Localization.Language.GetTextValue("GameUI.BaitPower", Main.HoverItem.bait);
                    num4++;
                }
                if (Main.HoverItem.headSlot > 0 || Main.HoverItem.bodySlot > 0 || Main.HoverItem.legSlot > 0 || Main.HoverItem.accessory || Main.projHook[Main.HoverItem.shoot] || Main.HoverItem.mountType != -1 || (Main.HoverItem.buffType > 0 && (Main.lightPet[Main.HoverItem.buffType] || Main.vanityPet[Main.HoverItem.buffType])))
                {
                    array[num4] = Lang.tip[23].Value;
                    num4++;
                }
                if (Main.HoverItem.tileWand > 0)
                {
                    array[num4] = Lang.tip[52].Value + Lang.GetItemNameValue(Main.HoverItem.tileWand);
                    num4++;
                }
                if (Main.HoverItem.questItem)
                {
                    array[num4] = Lang.inter[65].Value;
                    num4++;
                }
                if (Main.HoverItem.vanity)
                {
                    array[num4] = Lang.tip[24].Value;
                    num4++;
                }
                if (Main.HoverItem.defense > 0)
                {
                    array[num4] = Main.HoverItem.defense + Lang.tip[25].Value;
                    num4++;
                }
                if (Main.HoverItem.pick > 0)
                {
                    array[num4] = Main.HoverItem.pick + Lang.tip[26].Value;
                    num4++;
                }
                if (Main.HoverItem.axe > 0)
                {
                    array[num4] = Main.HoverItem.axe * 5 + Lang.tip[27].Value;
                    num4++;
                }
                if (Main.HoverItem.hammer > 0)
                {
                    array[num4] = Main.HoverItem.hammer + Lang.tip[28].Value;
                    num4++;
                }
                if (Main.HoverItem.tileBoost != 0)
                {
                    int tileBoost = Main.HoverItem.tileBoost;
                    if (tileBoost > 0)
                    {
                        array[num4] = "+" + tileBoost + Lang.tip[54].Value;
                    }
                    else
                    {
                        array[num4] = tileBoost + Lang.tip[54].Value;
                    }
                    num4++;
                }
                if (Main.HoverItem.healLife > 0)
                {
                    array[num4] = Terraria.Localization.Language.GetTextValue("CommonItemTooltip.RestoresLife", Main.HoverItem.healLife);
                    num4++;
                }
                if (Main.HoverItem.healMana > 0)
                {
                    array[num4] = Terraria.Localization.Language.GetTextValue("CommonItemTooltip.RestoresMana", Main.HoverItem.healMana);
                    num4++;
                }
                if (Main.HoverItem.mana > 0 && (Main.HoverItem.type != 127 || !Main.player[Main.myPlayer].spaceGun))
                {
                    array[num4] = Terraria.Localization.Language.GetTextValue("CommonItemTooltip.UsesMana", (int)((float)Main.HoverItem.mana * Main.player[Main.myPlayer].manaCost));
                    num4++;
                }
                if (Main.HoverItem.createWall > 0 || Main.HoverItem.createTile > -1)
                {
                    if (Main.HoverItem.type != 213 && Main.HoverItem.tileWand < 1)
                    {
                        array[num4] = Lang.tip[33].Value;
                        num4++;
                    }
                }
                else if (Main.HoverItem.ammo > 0 && !Main.HoverItem.notAmmo)
                {
                    array[num4] = Lang.tip[34].Value;
                    num4++;
                }
                else if (Main.HoverItem.consumable)
                {
                    array[num4] = Lang.tip[35].Value;
                    num4++;
                }
                if (Main.HoverItem.material)
                {
                    array[num4] = Lang.tip[36].Value;
                    num4++;
                }
                if (Main.HoverItem.ToolTip != null)
                {
                    for (int j = 0; j < Main.HoverItem.ToolTip.Lines; j++)
                    {
                        if (j == 0 && Main.HoverItem.type >= 1533 && Main.HoverItem.type <= 1537 && !NPC.downedPlantBoss)
                        {
                            array[num4] = Lang.tip[59].Value;
                            num4++;
                        }
                        else
                        {
                            array[num4] = Main.HoverItem.ToolTip.GetLine(j);
                            num4++;
                        }
                    }
                }
                if ((Main.HoverItem.type == 3818 || Main.HoverItem.type == 3819 || Main.HoverItem.type == 3820 || Main.HoverItem.type == 3824 || Main.HoverItem.type == 3825 || Main.HoverItem.type == 3826 || Main.HoverItem.type == 3829 || Main.HoverItem.type == 3830 || Main.HoverItem.type == 3831 || Main.HoverItem.type == 3832 || Main.HoverItem.type == 3833 || Main.HoverItem.type == 3834) && !Main.player[Main.myPlayer].downedDD2EventAnyDifficulty)
                {
                    array[num4] = Lang.misc[104].Value;
                    num4++;
                }
                if (Main.HoverItem.buffType == 26 && Main.expertMode)
                {
                    array[num4] = Lang.misc[40].Value;
                    num4++;
                }
                if (Main.HoverItem.buffTime > 0)
                {
                    string textValue;
                    if (Main.HoverItem.buffTime / 60 >= 60)
                    {
                        textValue = Terraria.Localization.Language.GetTextValue("CommonItemTooltip.MinuteDuration", Math.Round((double)(Main.HoverItem.buffTime / 60) / 60.0));
                    }
                    else
                    {
                        textValue = Terraria.Localization.Language.GetTextValue("CommonItemTooltip.SecondDuration", Math.Round((double)Main.HoverItem.buffTime / 60.0));
                    }
                    array[num4] = textValue;
                    num4++;
                }
                if (Main.HoverItem.type == 3262 || Main.HoverItem.type == 3282 || Main.HoverItem.type == 3283 || Main.HoverItem.type == 3284 || Main.HoverItem.type == 3285 || Main.HoverItem.type == 3286 || Main.HoverItem.type == 3316 || Main.HoverItem.type == 3315 || Main.HoverItem.type == 3317 || Main.HoverItem.type == 3291 || Main.HoverItem.type == 3389)
                {
                    array[num4] = " ";
                    num = num4;
                    num4++;
                }
                if (Main.HoverItem.prefix > 0)
                {
                    if (cpItem == null || cpItem.netID != Main.HoverItem.netID)
                    {
                        cpItem = new Item();
                        cpItem.netDefaults(Main.HoverItem.netID);
                    }
                    if (cpItem.damage != Main.HoverItem.damage)
                    {
                        double num12 = (double)((float)Main.HoverItem.damage - (float)cpItem.damage);
                        num12 = num12 / (double)((float)cpItem.damage) * 100.0;
                        num12 = Math.Round(num12);
                        if (num12 > 0.0)
                        {
                            array[num4] = "+" + num12 + Lang.tip[39].Value;
                        }
                        else
                        {
                            array[num4] = num12 + Lang.tip[39].Value;
                        }
                        if (num12 < 0.0)
                        {
                            array3[num4] = true;
                        }
                        array2[num4] = true;
                        num4++;
                    }
                    if (cpItem.useAnimation != Main.HoverItem.useAnimation)
                    {
                        double num13 = (double)((float)Main.HoverItem.useAnimation - (float)cpItem.useAnimation);
                        num13 = num13 / (double)((float)cpItem.useAnimation) * 100.0;
                        num13 = Math.Round(num13);
                        num13 *= -1.0;
                        if (num13 > 0.0)
                        {
                            array[num4] = "+" + num13 + Lang.tip[40].Value;
                        }
                        else
                        {
                            array[num4] = num13 + Lang.tip[40].Value;
                        }
                        if (num13 < 0.0)
                        {
                            array3[num4] = true;
                        }
                        array2[num4] = true;
                        num4++;
                    }
                    if (cpItem.crit != Main.HoverItem.crit)
                    {
                        double num14 = (double)((float)Main.HoverItem.crit - (float)cpItem.crit);
                        if (num14 > 0.0)
                        {
                            array[num4] = "+" + num14 + Lang.tip[41].Value;
                        }
                        else
                        {
                            array[num4] = num14 + Lang.tip[41].Value;
                        }
                        if (num14 < 0.0)
                        {
                            array3[num4] = true;
                        }
                        array2[num4] = true;
                        num4++;
                    }
                    if (cpItem.mana != Main.HoverItem.mana)
                    {
                        double num15 = (double)((float)Main.HoverItem.mana - (float)cpItem.mana);
                        num15 = num15 / (double)((float)cpItem.mana) * 100.0;
                        num15 = Math.Round(num15);
                        if (num15 > 0.0)
                        {
                            array[num4] = "+" + num15 + Lang.tip[42].Value;
                        }
                        else
                        {
                            array[num4] = num15 + Lang.tip[42].Value;
                        }
                        if (num15 > 0.0)
                        {
                            array3[num4] = true;
                        }
                        array2[num4] = true;
                        num4++;
                    }
                    if (cpItem.scale != Main.HoverItem.scale)
                    {
                        double num16 = (double)(Main.HoverItem.scale - cpItem.scale);
                        num16 = num16 / (double)cpItem.scale * 100.0;
                        num16 = Math.Round(num16);
                        if (num16 > 0.0)
                        {
                            array[num4] = "+" + num16 + Lang.tip[43].Value;
                        }
                        else
                        {
                            array[num4] = num16 + Lang.tip[43].Value;
                        }
                        if (num16 < 0.0)
                        {
                            array3[num4] = true;
                        }
                        array2[num4] = true;
                        num4++;
                    }
                    if (cpItem.shootSpeed != Main.HoverItem.shootSpeed)
                    {
                        double num17 = (double)(Main.HoverItem.shootSpeed - cpItem.shootSpeed);
                        num17 = num17 / (double)cpItem.shootSpeed * 100.0;
                        num17 = Math.Round(num17);
                        if (num17 > 0.0)
                        {
                            array[num4] = "+" + num17 + Lang.tip[44].Value;
                        }
                        else
                        {
                            array[num4] = num17 + Lang.tip[44].Value;
                        }
                        if (num17 < 0.0)
                        {
                            array3[num4] = true;
                        }
                        array2[num4] = true;
                        num4++;
                    }
                    if (cpItem.knockBack != knockBack)
                    {
                        double num18 = (double)(knockBack - cpItem.knockBack);
                        num18 = num18 / (double)cpItem.knockBack * 100.0;
                        num18 = Math.Round(num18);
                        if (num18 > 0.0)
                        {
                            array[num4] = "+" + num18 + Lang.tip[45].Value;
                        }
                        else
                        {
                            array[num4] = num18 + Lang.tip[45].Value;
                        }
                        if (num18 < 0.0)
                        {
                            array3[num4] = true;
                        }
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 62)
                    {
                        array[num4] = "+1" + Lang.tip[25].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 63)
                    {
                        array[num4] = "+2" + Lang.tip[25].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 64)
                    {
                        array[num4] = "+3" + Lang.tip[25].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 65)
                    {
                        array[num4] = "+4" + Lang.tip[25].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 66)
                    {
                        array[num4] = "+20 " + Lang.tip[31].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 67)
                    {
                        array[num4] = "+2" + Lang.tip[5].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 68)
                    {
                        array[num4] = "+4" + Lang.tip[5].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 69)
                    {
                        array[num4] = "+1" + Lang.tip[39].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 70)
                    {
                        array[num4] = "+2" + Lang.tip[39].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 71)
                    {
                        array[num4] = "+3" + Lang.tip[39].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 72)
                    {
                        array[num4] = "+4" + Lang.tip[39].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 73)
                    {
                        array[num4] = "+1" + Lang.tip[46].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 74)
                    {
                        array[num4] = "+2" + Lang.tip[46].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 75)
                    {
                        array[num4] = "+3" + Lang.tip[46].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 76)
                    {
                        array[num4] = "+4" + Lang.tip[46].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 77)
                    {
                        array[num4] = "+1" + Lang.tip[47].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 78)
                    {
                        array[num4] = "+2" + Lang.tip[47].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 79)
                    {
                        array[num4] = "+3" + Lang.tip[47].Value;
                        array2[num4] = true;
                        num4++;
                    }
                    if (Main.HoverItem.prefix == 80)
                    {
                        array[num4] = "+4" + Lang.tip[47].Value;
                        array2[num4] = true;
                        num4++;
                    }
                }
                if (Main.HoverItem.wornArmor && Main.player[Main.myPlayer].setBonus != "")
                {
                    array[num4] = Lang.tip[48].Value + " " + Main.player[Main.myPlayer].setBonus;
                    num4++;
                }
            }
            if (Main.HoverItem.expert)
            {
                array[num4] = Terraria.Localization.Language.GetTextValue("GameUI.Expert");
                num4++;
            }
            float num19 = (float)Main.mouseTextColor / 255f;
            int a = (int)Main.mouseTextColor;
            if (Main.npcShop > 0)
            {
                int storeValue = Main.HoverItem.GetStoreValue();
                if (Main.HoverItem.shopSpecialCurrency != -1)
                {
                    Terraria.GameContent.UI.CustomCurrencyManager.GetPriceText(Main.HoverItem.shopSpecialCurrency, array, ref num4, storeValue);
                    color = new Color((int)((byte)(255f * num19)), (int)((byte)(255f * num19)), (int)((byte)(255f * num19)), a);
                }
                else if (Main.HoverItem.GetStoreValue() > 0)
                {
                    string text = "";
                    int num20 = 0;
                    int num21 = 0;
                    int num22 = 0;
                    int num23 = 0;
                    int num24 = storeValue * Main.HoverItem.stack;
                    if (!Main.HoverItem.buy)
                    {
                        num24 = storeValue / 5;
                        if (num24 < 1)
                        {
                            num24 = 1;
                        }
                        num24 *= Main.HoverItem.stack;
                    }
                    if (num24 < 1)
                    {
                        num24 = 1;
                    }
                    if (num24 >= 1000000)
                    {
                        num20 = num24 / 1000000;
                        num24 -= num20 * 1000000;
                    }
                    if (num24 >= 10000)
                    {
                        num21 = num24 / 10000;
                        num24 -= num21 * 10000;
                    }
                    if (num24 >= 100)
                    {
                        num22 = num24 / 100;
                        num24 -= num22 * 100;
                    }
                    if (num24 >= 1)
                    {
                        num23 = num24;
                    }
                    if (num20 > 0)
                    {
                        text = string.Concat(new object[]
				{
					text,
					num20,
					" ",
					Lang.inter[15].Value,
					" "
				});
                    }
                    if (num21 > 0)
                    {
                        text = string.Concat(new object[]
				{
					text,
					num21,
					" ",
					Lang.inter[16].Value,
					" "
				});
                    }
                    if (num22 > 0)
                    {
                        text = string.Concat(new object[]
				{
					text,
					num22,
					" ",
					Lang.inter[17].Value,
					" "
				});
                    }
                    if (num23 > 0)
                    {
                        text = string.Concat(new object[]
				{
					text,
					num23,
					" ",
					Lang.inter[18].Value,
					" "
				});
                    }
                    if (!Main.HoverItem.buy)
                    {
                        array[num4] = Lang.tip[49].Value + " " + text;
                    }
                    else
                    {
                        array[num4] = Lang.tip[50].Value + " " + text;
                    }
                    num4++;
                    if (num20 > 0)
                    {
                        color = new Color((int)((byte)(220f * num19)), (int)((byte)(220f * num19)), (int)((byte)(198f * num19)), a);
                    }
                    else if (num21 > 0)
                    {
                        color = new Color((int)((byte)(224f * num19)), (int)((byte)(201f * num19)), (int)((byte)(92f * num19)), a);
                    }
                    else if (num22 > 0)
                    {
                        color = new Color((int)((byte)(181f * num19)), (int)((byte)(192f * num19)), (int)((byte)(193f * num19)), a);
                    }
                    else if (num23 > 0)
                    {
                        color = new Color((int)((byte)(246f * num19)), (int)((byte)(138f * num19)), (int)((byte)(96f * num19)), a);
                    }
                }
                else if (Main.HoverItem.type != 3817)
                {
                    array[num4] = Lang.tip[51].Value;
                    num4++;
                    color = new Color((int)((byte)(120f * num19)), (int)((byte)(120f * num19)), (int)((byte)(120f * num19)), a);
                }
            }

            if(Main.HoverItem.consumable || Main.HoverItem.melee)
            {
                array[num4] = "Usetime: " + Main.HoverItem.useTime;
                num4++;
            }

            Vector2 zero = Vector2.Zero;
            int num25 = 0;
            for (int k = 0; k < num4; k++)
            {
                Vector2 vector = Main.fontMouseText.MeasureString(array[k]);
                if (vector.X > zero.X)
                {
                    zero.X = vector.X;
                }
                zero.Y += vector.Y + (float)num25;
            }
            X += toolTipDistance;
            Y += toolTipDistance;
            if ((float)X + zero.X + 4f > (float)Main.screenWidth)
            {
                X = (int)((float)Main.screenWidth - zero.X - 4f);
            }
            if ((float)Y + zero.Y + 4f > (float)Main.screenHeight)
            {
                Y = (int)((float)Main.screenHeight - zero.Y - 4f);
            }
            int num26 = 0;
            float arg_203D_0 = (float)Main.mouseTextColor / 255f;
            for (int l = 0; l < num4; l++)
            {
                if (l == num)
                {
                    float num27 = 1f;
                    int num28 = (int)((float)Main.mouseTextColor * num27);
                    Color black = Color.Black;
                    for (int m = 0; m < 5; m++)
                    {
                        int num29 = X;
                        int num30 = Y + num26;
                        if (m == 4)
                        {
                            black = new Color(num28, num28, num28, num28);
                        }
                        if (m == 0)
                        {
                            num29--;
                        }
                        else if (m == 1)
                        {
                            num29++;
                        }
                        else if (m == 2)
                        {
                            num30--;
                        }
                        else if (m == 3)
                        {
                            num30++;
                        }
                        Main.spriteBatch.Draw(Main.oneDropLogo, new Vector2((float)num29, (float)num30), null, black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    }
                }
                else
                {
                    Color baseColor = Color.Black;
                    baseColor = new Color(num19, num19, num19, num19);
                    if (l == 0)
                    {
                        if (rare == -11)
                        {
                            baseColor = new Color((int)((byte)(255f * num19)), (int)((byte)(175f * num19)), (int)((byte)(0f * num19)), a);
                        }
                        if (rare == -1)
                        {
                            baseColor = new Color((int)((byte)(130f * num19)), (int)((byte)(130f * num19)), (int)((byte)(130f * num19)), a);
                        }
                        if (rare == 1)
                        {
                            baseColor = new Color((int)((byte)(150f * num19)), (int)((byte)(150f * num19)), (int)((byte)(255f * num19)), a);
                        }
                        if (rare == 2)
                        {
                            baseColor = new Color((int)((byte)(150f * num19)), (int)((byte)(255f * num19)), (int)((byte)(150f * num19)), a);
                        }
                        if (rare == 3)
                        {
                            baseColor = new Color((int)((byte)(255f * num19)), (int)((byte)(200f * num19)), (int)((byte)(150f * num19)), a);
                        }
                        if (rare == 4)
                        {
                            baseColor = new Color((int)((byte)(255f * num19)), (int)((byte)(150f * num19)), (int)((byte)(150f * num19)), a);
                        }
                        if (rare == 5)
                        {
                            baseColor = new Color((int)((byte)(255f * num19)), (int)((byte)(150f * num19)), (int)((byte)(255f * num19)), a);
                        }
                        if (rare == 6)
                        {
                            baseColor = new Color((int)((byte)(210f * num19)), (int)((byte)(160f * num19)), (int)((byte)(255f * num19)), a);
                        }
                        if (rare == 7)
                        {
                            baseColor = new Color((int)((byte)(150f * num19)), (int)((byte)(255f * num19)), (int)((byte)(10f * num19)), a);
                        }
                        if (rare == 8)
                        {
                            baseColor = new Color((int)((byte)(255f * num19)), (int)((byte)(255f * num19)), (int)((byte)(10f * num19)), a);
                        }
                        if (rare == 9)
                        {
                            baseColor = new Color((int)((byte)(5f * num19)), (int)((byte)(200f * num19)), (int)((byte)(255f * num19)), a);
                        }
                        if (rare == 10)
                        {
                            baseColor = new Color((int)((byte)(255f * num19)), (int)((byte)(40f * num19)), (int)((byte)(100f * num19)), a);
                        }
                        if (rare >= 11)
                        {
                            baseColor = new Color((int)((byte)(180f * num19)), (int)((byte)(40f * num19)), (int)((byte)(255f * num19)), a);
                        }
                        if (diff == 1)
                        {
                            baseColor = new Color((int)((byte)((float)Main.mcColor.R * num19)), (int)((byte)((float)Main.mcColor.G * num19)), (int)((byte)((float)Main.mcColor.B * num19)), a);
                        }
                        if (diff == 2)
                        {
                            baseColor = new Color((int)((byte)((float)Main.hcColor.R * num19)), (int)((byte)((float)Main.hcColor.G * num19)), (int)((byte)((float)Main.hcColor.B * num19)), a);
                        }
                        if (Main.HoverItem.expert || rare == -12)
                        {
                            baseColor = new Color((int)((byte)((float)Main.DiscoR * num19)), (int)((byte)((float)Main.DiscoG * num19)), (int)((byte)((float)Main.DiscoB * num19)), a);
                        }
                    }
                    else if (array2[l])
                    {
                        if (array3[l])
                        {
                            baseColor = new Color((int)((byte)(190f * num19)), (int)((byte)(120f * num19)), (int)((byte)(120f * num19)), a);
                        }
                        else
                        {
                            baseColor = new Color((int)((byte)(120f * num19)), (int)((byte)(190f * num19)), (int)((byte)(120f * num19)), a);
                        }
                    }
                    else if (l == num4 - 1)
                    {
                        baseColor = color;
                    }
                    Terraria.UI.Chat.ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, array[l], new Vector2((float)X, (float)(Y + num26)), baseColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                }
                num26 += (int)(Main.fontMouseText.MeasureString(array[l]).Y + (float)num25);
            }
        }

    }
}
