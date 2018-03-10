using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class WorkingSocialSlots
    {
        // Terraria.Player
        public static void UpdateEquipsPre(Player plr, int i)
        {
            for (int k = 10; k < 18 + plr.extraAccessorySlots; k++)
            {
                if (!plr.armor[k].expertOnly || Main.expertMode)
                {
                    int type2 = plr.armor[k].type;
                    if ((type2 == 15 || type2 == 707) && plr.accWatch < 1)
                    {
                        plr.accWatch = 1;
                    }
                    if ((type2 == 16 || type2 == 708) && plr.accWatch < 2)
                    {
                        plr.accWatch = 2;
                    }
                    if ((type2 == 17 || type2 == 709) && plr.accWatch < 3)
                    {
                        plr.accWatch = 3;
                    }
                    if (type2 == 393)
                    {
                        plr.accCompass = 1;
                    }
                    if (type2 == 18)
                    {
                        plr.accDepthMeter = 1;
                    }
                    if (type2 == 395 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accWatch = 3;
                        plr.accDepthMeter = 1;
                        plr.accCompass = 1;
                    }
                    if (type2 == 3120 || type2 == 3036 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accFishFinder = true;
                    }
                    if (type2 == 3037 || type2 == 3036 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accWeatherRadio = true;
                    }
                    if (type2 == 3096 || type2 == 3036 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accCalendar = true;
                    }
                    if (type2 == 3084 || type2 == 3122 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accThirdEye = true;
                    }
                    if (type2 == 3095 || type2 == 3122 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accJarOfSouls = true;
                    }
                    if (type2 == 3118 || type2 == 3122 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accCritterGuide = true;
                    }
                    if (type2 == 3099 || type2 == 3121 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accStopwatch = true;
                    }
                    if (type2 == 3102 || type2 == 3121 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accOreFinder = true;
                    }
                    if (type2 == 3119 || type2 == 3121 || type2 == 3123 || type2 == 3124)
                    {
                        plr.accDreamCatcher = true;
                    }
                    if (type2 == 3619)
                    {
                        plr.InfoAccMechShowWires = true;
                    }
                    if (plr.armor[k].type == 3017 && plr.whoAmI == Main.myPlayer && plr.velocity.Y == 0f && plr.grappling[0] == -1)
                    {
                        int num = (int)plr.Center.X / 16;
                        int num2 = (int)(plr.position.Y + (float)plr.height - 1f) / 16;
                        if (Main.tile[num, num2] == null)
                        {
                            Main.tile[num, num2] = new Tile();
                        }
                        if (!Main.tile[num, num2].active() && Main.tile[num, num2].liquid == 0 && Main.tile[num, num2 + 1] != null && WorldGen.SolidTile(num, num2 + 1))
                        {
                            Main.tile[num, num2].frameY = 0;
                            Main.tile[num, num2].slope(0);
                            Main.tile[num, num2].halfBrick(false);
                            if (Main.tile[num, num2 + 1].type == 2)
                            {
                                if (Main.rand.Next(2) == 0)
                                {
                                    Main.tile[num, num2].active(true);
                                    Main.tile[num, num2].type = 3;
                                    Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(6, 11));
                                    while (Main.tile[num, num2].frameX == 144)
                                    {
                                        Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(6, 11));
                                    }
                                }
                                else
                                {
                                    Main.tile[num, num2].active(true);
                                    Main.tile[num, num2].type = 73;
                                    Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(6, 21));
                                    while (Main.tile[num, num2].frameX == 144)
                                    {
                                        Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(6, 21));
                                    }
                                }
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendTileSquare(-1, num, num2, 1, Terraria.ID.TileChangeType.None);
                                }
                            }
                            else if (Main.tile[num, num2 + 1].type == 109)
                            {
                                if (Main.rand.Next(2) == 0)
                                {
                                    Main.tile[num, num2].active(true);
                                    Main.tile[num, num2].type = 110;
                                    Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(4, 7));
                                    while (Main.tile[num, num2].frameX == 90)
                                    {
                                        Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(4, 7));
                                    }
                                }
                                else
                                {
                                    Main.tile[num, num2].active(true);
                                    Main.tile[num, num2].type = 113;
                                    Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(2, 8));
                                    while (Main.tile[num, num2].frameX == 90)
                                    {
                                        Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(2, 8));
                                    }
                                }
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendTileSquare(-1, num, num2, 1, Terraria.ID.TileChangeType.None);
                                }
                            }
                            else if (Main.tile[num, num2 + 1].type == 60)
                            {
                                Main.tile[num, num2].active(true);
                                Main.tile[num, num2].type = 74;
                                Main.tile[num, num2].frameX = (short)(18 * Main.rand.Next(9, 17));
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendTileSquare(-1, num, num2, 1, Terraria.ID.TileChangeType.None);
                                }
                            }
                        }
                    }
                    plr.statDefense += plr.armor[k].defense;
                    plr.lifeRegen += plr.armor[k].lifeRegen;
                    if (plr.armor[k].shieldSlot > 0)
                    {
                        plr.hasRaisableShield = true;
                    }
                    int type3 = plr.armor[k].type;
                    switch (type3)
                    {
                        case 3797:
                            plr.maxTurrets++;
                            plr.manaCost -= 0.1f;
                            break;
                        case 3798:
                            plr.magicDamage += 0.1f;
                            plr.minionDamage += 0.2f;
                            break;
                        case 3799:
                            plr.minionDamage += 0.1f;
                            plr.magicCrit += 20;
                            break;
                        case 3800:
                            plr.maxTurrets++;
                            plr.lifeRegen += 8;
                            break;
                        case 3801:
                            plr.meleeDamage += 0.15f;
                            plr.minionDamage += 0.15f;
                            break;
                        case 3802:
                            plr.minionDamage += 0.15f;
                            plr.meleeCrit += 20;
                            plr.moveSpeed += 0.2f;
                            break;
                        case 3803:
                            plr.maxTurrets++;
                            plr.rangedCrit += 10;
                            break;
                        case 3804:
                            plr.rangedDamage += 0.2f;
                            plr.minionDamage += 0.2f;
                            break;
                        case 3805:
                            plr.minionDamage += 0.1f;
                            plr.moveSpeed += 0.2f;
                            break;
                        case 3806:
                            plr.maxTurrets++;
                            plr.meleeSpeed += 0.2f;
                            break;
                        case 3807:
                            plr.meleeDamage += 0.2f;
                            plr.minionDamage += 0.2f;
                            break;
                        case 3808:
                            plr.minionDamage += 0.1f;
                            plr.meleeCrit += 10;
                            plr.moveSpeed += 0.2f;
                            break;
                        default:
                            switch (type3)
                            {
                                case 3871:
                                    plr.maxTurrets += 2;
                                    plr.minionDamage += 0.1f;
                                    break;
                                case 3872:
                                    plr.minionDamage += 0.3f;
                                    plr.lifeRegen += 16;
                                    break;
                                case 3873:
                                    plr.minionDamage += 0.2f;
                                    plr.meleeCrit += 20;
                                    plr.moveSpeed += 0.3f;
                                    break;
                                case 3874:
                                    plr.maxTurrets += 2;
                                    plr.magicDamage += 0.1f;
                                    plr.minionDamage += 0.1f;
                                    break;
                                case 3875:
                                    plr.minionDamage += 0.3f;
                                    plr.magicDamage += 0.15f;
                                    break;
                                case 3876:
                                    plr.minionDamage += 0.2f;
                                    plr.magicCrit += 25;
                                    break;
                                case 3877:
                                    plr.maxTurrets += 2;
                                    plr.minionDamage += 0.1f;
                                    plr.rangedCrit += 10;
                                    break;
                                case 3878:
                                    plr.minionDamage += 0.25f;
                                    plr.rangedDamage += 0.25f;
                                    break;
                                case 3879:
                                    plr.minionDamage += 0.25f;
                                    plr.moveSpeed += 0.2f;
                                    break;
                                case 3880:
                                    plr.maxTurrets += 2;
                                    plr.minionDamage += 0.2f;
                                    plr.meleeDamage += 0.2f;
                                    break;
                                case 3881:
                                    plr.meleeSpeed += 0.2f;
                                    plr.minionDamage += 0.2f;
                                    break;
                                case 3882:
                                    plr.minionDamage += 0.2f;
                                    plr.meleeCrit += 20;
                                    plr.moveSpeed += 0.2f;
                                    break;
                            }
                            break;
                    }
                    if (plr.armor[k].type == 268)
                    {
                        plr.accDivingHelm = true;
                    }
                    if (plr.armor[k].type == 238)
                    {
                        plr.magicDamage += 0.15f;
                    }
                    if (plr.armor[k].type == 3770)
                    {
                        plr.slowFall = true;
                    }
                    if (plr.armor[k].type == 3776)
                    {
                        plr.magicDamage += 0.15f;
                        plr.minionDamage += 0.15f;
                    }
                    if (plr.armor[k].type == 3777)
                    {
                        plr.statManaMax2 += 80;
                    }
                    if (plr.armor[k].type == 3778)
                    {
                        plr.maxMinions += 2;
                    }
                    if (plr.armor[k].type == 3212)
                    {
                        plr.armorPenetration += 5;
                    }
                    if (plr.armor[k].type == 2277)
                    {
                        plr.magicDamage += 0.05f;
                        plr.meleeDamage += 0.05f;
                        plr.rangedDamage += 0.05f;
                        plr.thrownDamage += 0.05f;
                        plr.magicCrit += 5;
                        plr.rangedCrit += 5;
                        plr.meleeCrit += 5;
                        plr.thrownCrit += 5;
                        plr.meleeSpeed += 0.1f;
                        plr.moveSpeed += 0.1f;
                    }
                    if (plr.armor[k].type == 2279)
                    {
                        plr.magicDamage += 0.06f;
                        plr.magicCrit += 6;
                        plr.manaCost -= 0.1f;
                    }
                    if (plr.armor[k].type == 3109)
                    {
                        plr.nightVision = true;
                    }
                    if (plr.armor[k].type == 256)
                    {
                        plr.thrownVelocity += 0.15f;
                    }
                    if (plr.armor[k].type == 257)
                    {
                        plr.thrownDamage += 0.15f;
                    }
                    if (plr.armor[k].type == 258)
                    {
                        plr.thrownCrit += 10;
                    }
                    if (plr.armor[k].type == 3374)
                    {
                        plr.thrownVelocity += 0.2f;
                    }
                    if (plr.armor[k].type == 3375)
                    {
                        plr.thrownDamage += 0.2f;
                    }
                    if (plr.armor[k].type == 3376)
                    {
                        plr.thrownCrit += 15;
                    }
                    if (plr.armor[k].type == 2275)
                    {
                        plr.magicDamage += 0.07f;
                        plr.magicCrit += 7;
                    }
                    if (plr.armor[k].type == 123 || plr.armor[k].type == 124 || plr.armor[k].type == 125)
                    {
                        plr.magicDamage += 0.07f;
                    }
                    if (plr.armor[k].type == 151 || plr.armor[k].type == 152 || plr.armor[k].type == 153 || plr.armor[k].type == 959)
                    {
                        plr.rangedDamage += 0.05f;
                    }
                    if (plr.armor[k].type == 111 || plr.armor[k].type == 228 || plr.armor[k].type == 229 || plr.armor[k].type == 230 || plr.armor[k].type == 960 || plr.armor[k].type == 961 || plr.armor[k].type == 962)
                    {
                        plr.statManaMax2 += 20;
                    }
                    if (plr.armor[k].type == 228 || plr.armor[k].type == 960)
                    {
                        plr.statManaMax2 += 20;
                    }
                    if (plr.armor[k].type == 228 || plr.armor[k].type == 229 || plr.armor[k].type == 230 || plr.armor[k].type == 960 || plr.armor[k].type == 961 || plr.armor[k].type == 962)
                    {
                        plr.magicCrit += 4;
                    }
                    if (plr.armor[k].type == 100 || plr.armor[k].type == 101 || plr.armor[k].type == 102)
                    {
                        plr.meleeSpeed += 0.07f;
                    }
                    if (plr.armor[k].type == 956 || plr.armor[k].type == 957 || plr.armor[k].type == 958)
                    {
                        plr.meleeSpeed += 0.07f;
                    }
                    if (plr.armor[k].type == 792 || plr.armor[k].type == 793 || plr.armor[k].type == 794)
                    {
                        plr.meleeDamage += 0.02f;
                        plr.rangedDamage += 0.02f;
                        plr.magicDamage += 0.02f;
                        plr.thrownDamage += 0.02f;
                    }
                    if (plr.armor[k].type == 371)
                    {
                        plr.magicCrit += 9;
                        plr.statManaMax2 += 40;
                    }
                    if (plr.armor[k].type == 372)
                    {
                        plr.moveSpeed += 0.07f;
                        plr.meleeSpeed += 0.12f;
                    }
                    if (plr.armor[k].type == 373)
                    {
                        plr.rangedDamage += 0.1f;
                        plr.rangedCrit += 6;
                    }
                    if (plr.armor[k].type == 374)
                    {
                        plr.magicCrit += 3;
                        plr.meleeCrit += 3;
                        plr.rangedCrit += 3;
                    }
                    if (plr.armor[k].type == 375)
                    {
                        plr.moveSpeed += 0.1f;
                    }
                    if (plr.armor[k].type == 376)
                    {
                        plr.magicDamage += 0.15f;
                        plr.statManaMax2 += 60;
                    }
                    if (plr.armor[k].type == 377)
                    {
                        plr.meleeCrit += 5;
                        plr.meleeDamage += 0.1f;
                    }
                    if (plr.armor[k].type == 378)
                    {
                        plr.rangedDamage += 0.12f;
                        plr.rangedCrit += 7;
                    }
                    if (plr.armor[k].type == 379)
                    {
                        plr.rangedDamage += 0.05f;
                        plr.meleeDamage += 0.05f;
                        plr.magicDamage += 0.05f;
                    }
                    if (plr.armor[k].type == 380)
                    {
                        plr.magicCrit += 3;
                        plr.meleeCrit += 3;
                        plr.rangedCrit += 3;
                    }
                    if (plr.armor[k].type >= 2367 && plr.armor[k].type <= 2369)
                    {
                        plr.fishingSkill += 5;
                    }
                    if (plr.armor[k].type == 400)
                    {
                        plr.magicDamage += 0.11f;
                        plr.magicCrit += 11;
                        plr.statManaMax2 += 80;
                    }
                    if (plr.armor[k].type == 401)
                    {
                        plr.meleeCrit += 7;
                        plr.meleeDamage += 0.14f;
                    }
                    if (plr.armor[k].type == 402)
                    {
                        plr.rangedDamage += 0.14f;
                        plr.rangedCrit += 8;
                    }
                    if (plr.armor[k].type == 403)
                    {
                        plr.rangedDamage += 0.06f;
                        plr.meleeDamage += 0.06f;
                        plr.magicDamage += 0.06f;
                    }
                    if (plr.armor[k].type == 404)
                    {
                        plr.magicCrit += 4;
                        plr.meleeCrit += 4;
                        plr.rangedCrit += 4;
                        plr.moveSpeed += 0.05f;
                    }
                    if (plr.armor[k].type == 1205)
                    {
                        plr.meleeDamage += 0.08f;
                        plr.meleeSpeed += 0.12f;
                    }
                    if (plr.armor[k].type == 1206)
                    {
                        plr.rangedDamage += 0.09f;
                        plr.rangedCrit += 9;
                    }
                    if (plr.armor[k].type == 1207)
                    {
                        plr.magicDamage += 0.07f;
                        plr.magicCrit += 7;
                        plr.statManaMax2 += 60;
                    }
                    if (plr.armor[k].type == 1208)
                    {
                        plr.meleeDamage += 0.03f;
                        plr.rangedDamage += 0.03f;
                        plr.magicDamage += 0.03f;
                        plr.magicCrit += 2;
                        plr.meleeCrit += 2;
                        plr.rangedCrit += 2;
                    }
                    if (plr.armor[k].type == 1209)
                    {
                        plr.meleeDamage += 0.02f;
                        plr.rangedDamage += 0.02f;
                        plr.magicDamage += 0.02f;
                        plr.magicCrit++;
                        plr.meleeCrit++;
                        plr.rangedCrit++;
                    }
                    if (plr.armor[k].type == 1210)
                    {
                        plr.meleeDamage += 0.07f;
                        plr.meleeSpeed += 0.07f;
                        plr.moveSpeed += 0.07f;
                    }
                    if (plr.armor[k].type == 1211)
                    {
                        plr.rangedCrit += 15;
                        plr.moveSpeed += 0.08f;
                    }
                    if (plr.armor[k].type == 1212)
                    {
                        plr.magicCrit += 18;
                        plr.statManaMax2 += 80;
                    }
                    if (plr.armor[k].type == 1213)
                    {
                        plr.magicCrit += 6;
                        plr.meleeCrit += 6;
                        plr.rangedCrit += 6;
                    }
                    if (plr.armor[k].type == 1214)
                    {
                        plr.moveSpeed += 0.11f;
                    }
                    if (plr.armor[k].type == 1215)
                    {
                        plr.meleeDamage += 0.08f;
                        plr.meleeCrit += 8;
                        plr.meleeSpeed += 0.08f;
                    }
                    if (plr.armor[k].type == 1216)
                    {
                        plr.rangedDamage += 0.16f;
                        plr.rangedCrit += 7;
                    }
                    if (plr.armor[k].type == 1217)
                    {
                        plr.magicDamage += 0.16f;
                        plr.magicCrit += 7;
                        plr.statManaMax2 += 100;
                    }
                    if (plr.armor[k].type == 1218)
                    {
                        plr.meleeDamage += 0.04f;
                        plr.rangedDamage += 0.04f;
                        plr.magicDamage += 0.04f;
                        plr.magicCrit += 3;
                        plr.meleeCrit += 3;
                        plr.rangedCrit += 3;
                    }
                    if (plr.armor[k].type == 1219)
                    {
                        plr.meleeDamage += 0.03f;
                        plr.rangedDamage += 0.03f;
                        plr.magicDamage += 0.03f;
                        plr.magicCrit += 3;
                        plr.meleeCrit += 3;
                        plr.rangedCrit += 3;
                        plr.moveSpeed += 0.06f;
                    }
                    if (plr.armor[k].type == 558)
                    {
                        plr.magicDamage += 0.12f;
                        plr.magicCrit += 12;
                        plr.statManaMax2 += 100;
                    }
                    if (plr.armor[k].type == 559)
                    {
                        plr.meleeCrit += 10;
                        plr.meleeDamage += 0.1f;
                        plr.meleeSpeed += 0.1f;
                    }
                    if (plr.armor[k].type == 553)
                    {
                        plr.rangedDamage += 0.15f;
                        plr.rangedCrit += 8;
                    }
                    if (plr.armor[k].type == 551)
                    {
                        plr.magicCrit += 7;
                        plr.meleeCrit += 7;
                        plr.rangedCrit += 7;
                    }
                    if (plr.armor[k].type == 552)
                    {
                        plr.rangedDamage += 0.07f;
                        plr.meleeDamage += 0.07f;
                        plr.magicDamage += 0.07f;
                        plr.moveSpeed += 0.08f;
                    }
                    if (plr.armor[k].type == 1001)
                    {
                        plr.meleeDamage += 0.16f;
                        plr.meleeCrit += 6;
                    }
                    if (plr.armor[k].type == 1002)
                    {
                        plr.rangedDamage += 0.16f;
                        plr.ammoCost80 = true;
                    }
                    if (plr.armor[k].type == 1003)
                    {
                        plr.statManaMax2 += 80;
                        plr.manaCost -= 0.17f;
                        plr.magicDamage += 0.16f;
                    }
                    if (plr.armor[k].type == 1004)
                    {
                        plr.meleeDamage += 0.05f;
                        plr.magicDamage += 0.05f;
                        plr.rangedDamage += 0.05f;
                        plr.magicCrit += 7;
                        plr.meleeCrit += 7;
                        plr.rangedCrit += 7;
                    }
                    if (plr.armor[k].type == 1005)
                    {
                        plr.magicCrit += 8;
                        plr.meleeCrit += 8;
                        plr.rangedCrit += 8;
                        plr.moveSpeed += 0.05f;
                    }
                    if (plr.armor[k].type == 2189)
                    {
                        plr.statManaMax2 += 60;
                        plr.manaCost -= 0.13f;
                        plr.magicDamage += 0.05f;
                        plr.magicCrit += 5;
                    }
                    if (plr.armor[k].type == 1503)
                    {
                        plr.magicDamage -= 0.4f;
                    }
                    if (plr.armor[k].type == 1504)
                    {
                        plr.magicDamage += 0.07f;
                        plr.magicCrit += 7;
                    }
                    if (plr.armor[k].type == 1505)
                    {
                        plr.magicDamage += 0.08f;
                        plr.moveSpeed += 0.08f;
                    }
                    if (plr.armor[k].type == 1546)
                    {
                        plr.rangedCrit += 5;
                        plr.arrowDamage += 0.15f;
                    }
                    if (plr.armor[k].type == 1547)
                    {
                        plr.rangedCrit += 5;
                        plr.bulletDamage += 0.15f;
                    }
                    if (plr.armor[k].type == 1548)
                    {
                        plr.rangedCrit += 5;
                        plr.rocketDamage += 0.15f;
                    }
                    if (plr.armor[k].type == 1549)
                    {
                        plr.rangedCrit += 13;
                        plr.rangedDamage += 0.13f;
                        plr.ammoCost80 = true;
                    }
                    if (plr.armor[k].type == 1550)
                    {
                        plr.rangedCrit += 7;
                        plr.moveSpeed += 0.12f;
                    }
                    if (plr.armor[k].type == 1282)
                    {
                        plr.statManaMax2 += 20;
                        plr.manaCost -= 0.05f;
                    }
                    if (plr.armor[k].type == 1283)
                    {
                        plr.statManaMax2 += 40;
                        plr.manaCost -= 0.07f;
                    }
                    if (plr.armor[k].type == 1284)
                    {
                        plr.statManaMax2 += 40;
                        plr.manaCost -= 0.09f;
                    }
                    if (plr.armor[k].type == 1285)
                    {
                        plr.statManaMax2 += 60;
                        plr.manaCost -= 0.11f;
                    }
                    if (plr.armor[k].type == 1286)
                    {
                        plr.statManaMax2 += 60;
                        plr.manaCost -= 0.13f;
                    }
                    if (plr.armor[k].type == 1287)
                    {
                        plr.statManaMax2 += 80;
                        plr.manaCost -= 0.15f;
                    }
                    if (plr.armor[k].type == 1316 || plr.armor[k].type == 1317 || plr.armor[k].type == 1318)
                    {
                        plr.aggro += 250;
                    }
                    if (plr.armor[k].type == 1316)
                    {
                        plr.meleeDamage += 0.06f;
                    }
                    if (plr.armor[k].type == 1317)
                    {
                        plr.meleeDamage += 0.08f;
                        plr.meleeCrit += 8;
                    }
                    if (plr.armor[k].type == 1318)
                    {
                        plr.meleeCrit += 4;
                    }
                    if (plr.armor[k].type == 2199 || plr.armor[k].type == 2202)
                    {
                        plr.aggro += 250;
                    }
                    if (plr.armor[k].type == 2201)
                    {
                        plr.aggro += 400;
                    }
                    if (plr.armor[k].type == 2199)
                    {
                        plr.meleeDamage += 0.06f;
                    }
                    if (plr.armor[k].type == 2200)
                    {
                        plr.meleeDamage += 0.08f;
                        plr.meleeCrit += 8;
                        plr.meleeSpeed += 0.06f;
                        plr.moveSpeed += 0.06f;
                    }
                    if (plr.armor[k].type == 2201)
                    {
                        plr.meleeDamage += 0.05f;
                        plr.meleeCrit += 5;
                    }
                    if (plr.armor[k].type == 2202)
                    {
                        plr.meleeSpeed += 0.06f;
                        plr.moveSpeed += 0.06f;
                    }
                    if (plr.armor[k].type == 684)
                    {
                        plr.rangedDamage += 0.16f;
                        plr.meleeDamage += 0.16f;
                    }
                    if (plr.armor[k].type == 685)
                    {
                        plr.meleeCrit += 11;
                        plr.rangedCrit += 11;
                    }
                    if (plr.armor[k].type == 686)
                    {
                        plr.moveSpeed += 0.08f;
                        plr.meleeSpeed += 0.07f;
                    }
                    if (plr.armor[k].type == 2361)
                    {
                        plr.maxMinions++;
                        plr.minionDamage += 0.04f;
                    }
                    if (plr.armor[k].type == 2362)
                    {
                        plr.maxMinions++;
                        plr.minionDamage += 0.04f;
                    }
                    if (plr.armor[k].type == 2363)
                    {
                        plr.minionDamage += 0.05f;
                    }
                    if (plr.armor[k].type >= 1158 && plr.armor[k].type <= 1161)
                    {
                        plr.maxMinions++;
                    }
                    if (plr.armor[k].type >= 1159 && plr.armor[k].type <= 1161)
                    {
                        plr.minionDamage += 0.1f;
                    }
                    if (plr.armor[k].type >= 2370 && plr.armor[k].type <= 2371)
                    {
                        plr.minionDamage += 0.05f;
                        plr.maxMinions++;
                    }
                    if (plr.armor[k].type == 2372)
                    {
                        plr.minionDamage += 0.06f;
                        plr.maxMinions++;
                    }
                    if (plr.armor[k].type == 3381 || plr.armor[k].type == 3382 || plr.armor[k].type == 3383)
                    {
                        if (plr.armor[k].type != 3381)
                        {
                            plr.maxMinions++;
                        }
                        plr.maxMinions++;
                        plr.minionDamage += 0.22f;
                    }
                    if (plr.armor[k].type == 2763)
                    {
                        plr.aggro += 300;
                        plr.meleeCrit += 17;
                    }
                    if (plr.armor[k].type == 2764)
                    {
                        plr.aggro += 300;
                        plr.meleeDamage += 0.22f;
                    }
                    if (plr.armor[k].type == 2765)
                    {
                        plr.aggro += 300;
                        plr.meleeSpeed += 0.15f;
                        plr.moveSpeed += 0.15f;
                    }
                    if (plr.armor[k].type == 2757)
                    {
                        plr.rangedCrit += 7;
                        plr.rangedDamage += 0.16f;
                    }
                    if (plr.armor[k].type == 2758)
                    {
                        plr.ammoCost75 = true;
                        plr.rangedCrit += 12;
                        plr.rangedDamage += 0.12f;
                    }
                    if (plr.armor[k].type == 2759)
                    {
                        plr.rangedCrit += 8;
                        plr.rangedDamage += 0.08f;
                        plr.moveSpeed += 0.1f;
                    }
                    if (plr.armor[k].type == 2760)
                    {
                        plr.statManaMax2 += 60;
                        plr.manaCost -= 0.15f;
                        plr.magicCrit += 7;
                        plr.magicDamage += 0.07f;
                    }
                    if (plr.armor[k].type == 2761)
                    {
                        plr.magicDamage += 0.09f;
                        plr.magicCrit += 9;
                    }
                    if (plr.armor[k].type == 2762)
                    {
                        plr.moveSpeed += 0.1f;
                        plr.magicDamage += 0.1f;
                    }
                    if (plr.armor[k].type >= 1832 && plr.armor[k].type <= 1834)
                    {
                        plr.maxMinions++;
                    }
                    if (plr.armor[k].type >= 1832 && plr.armor[k].type <= 1834)
                    {
                        plr.minionDamage += 0.11f;
                    }
                    if (plr.armor[k].prefix == 62)
                    {
                        plr.statDefense++;
                    }
                    if (plr.armor[k].prefix == 63)
                    {
                        plr.statDefense += 2;
                    }
                    if (plr.armor[k].prefix == 64)
                    {
                        plr.statDefense += 3;
                    }
                    if (plr.armor[k].prefix == 65)
                    {
                        plr.statDefense += 4;
                    }
                    if (plr.armor[k].prefix == 66)
                    {
                        plr.statManaMax2 += 20;
                    }
                    if (plr.armor[k].prefix == 67)
                    {
                        plr.meleeCrit += 2;
                        plr.rangedCrit += 2;
                        plr.magicCrit += 2;
                        plr.thrownCrit += 2;
                    }
                    if (plr.armor[k].prefix == 68)
                    {
                        plr.meleeCrit += 4;
                        plr.rangedCrit += 4;
                        plr.magicCrit += 4;
                        plr.thrownCrit += 4;
                    }
                    if (plr.armor[k].prefix == 69)
                    {
                        plr.meleeDamage += 0.01f;
                        plr.rangedDamage += 0.01f;
                        plr.magicDamage += 0.01f;
                        plr.minionDamage += 0.01f;
                        plr.thrownDamage += 0.01f;
                    }
                    if (plr.armor[k].prefix == 70)
                    {
                        plr.meleeDamage += 0.02f;
                        plr.rangedDamage += 0.02f;
                        plr.magicDamage += 0.02f;
                        plr.minionDamage += 0.02f;
                        plr.thrownDamage += 0.02f;
                    }
                    if (plr.armor[k].prefix == 71)
                    {
                        plr.meleeDamage += 0.03f;
                        plr.rangedDamage += 0.03f;
                        plr.magicDamage += 0.03f;
                        plr.minionDamage += 0.03f;
                        plr.thrownDamage += 0.03f;
                    }
                    if (plr.armor[k].prefix == 72)
                    {
                        plr.meleeDamage += 0.04f;
                        plr.rangedDamage += 0.04f;
                        plr.magicDamage += 0.04f;
                        plr.minionDamage += 0.04f;
                        plr.thrownDamage += 0.04f;
                    }
                    if (plr.armor[k].prefix == 73)
                    {
                        plr.moveSpeed += 0.01f;
                    }
                    if (plr.armor[k].prefix == 74)
                    {
                        plr.moveSpeed += 0.02f;
                    }
                    if (plr.armor[k].prefix == 75)
                    {
                        plr.moveSpeed += 0.03f;
                    }
                    if (plr.armor[k].prefix == 76)
                    {
                        plr.moveSpeed += 0.04f;
                    }
                    if (plr.armor[k].prefix == 77)
                    {
                        plr.meleeSpeed += 0.01f;
                    }
                    if (plr.armor[k].prefix == 78)
                    {
                        plr.meleeSpeed += 0.02f;
                    }
                    if (plr.armor[k].prefix == 79)
                    {
                        plr.meleeSpeed += 0.03f;
                    }
                    if (plr.armor[k].prefix == 80)
                    {
                        plr.meleeSpeed += 0.04f;
                    }
                }
            }
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            for (int l = 13; l < 18 + plr.extraAccessorySlots; l++)
            {
                if (!plr.armor[l].expertOnly || Main.expertMode)
                {
                    if (plr.armor[l].type == 3810 || plr.armor[l].type == 3809 || plr.armor[l].type == 3812 || plr.armor[l].type == 3811)
                    {
                        plr.dd2Accessory = true;
                    }
                    if (plr.armor[l].type == 3015)
                    {
                        plr.aggro -= 400;
                        plr.meleeCrit += 5;
                        plr.magicCrit += 5;
                        plr.rangedCrit += 5;
                        plr.thrownCrit += 5;
                        plr.meleeDamage += 0.05f;
                        plr.magicDamage += 0.05f;
                        plr.rangedDamage += 0.05f;
                        plr.thrownDamage += 0.05f;
                        plr.minionDamage += 0.05f;
                    }
                    if (plr.armor[l].type == 3016)
                    {
                        plr.aggro += 400;
                    }
                    if (plr.armor[l].type == 2373)
                    {
                        plr.accFishingLine = true;
                    }
                    if (plr.armor[l].type == 2374)
                    {
                        plr.fishingSkill += 10;
                    }
                    if (plr.armor[l].type == 2375)
                    {
                        plr.accTackleBox = true;
                    }
                    if (plr.armor[l].type == 3721)
                    {
                        plr.accFishingLine = true;
                        plr.accTackleBox = true;
                        plr.fishingSkill += 10;
                    }
                    if (plr.armor[l].type == 3090)
                    {
                        plr.npcTypeNoAggro[1] = true;
                        plr.npcTypeNoAggro[16] = true;
                        plr.npcTypeNoAggro[59] = true;
                        plr.npcTypeNoAggro[71] = true;
                        plr.npcTypeNoAggro[81] = true;
                        plr.npcTypeNoAggro[138] = true;
                        plr.npcTypeNoAggro[121] = true;
                        plr.npcTypeNoAggro[122] = true;
                        plr.npcTypeNoAggro[141] = true;
                        plr.npcTypeNoAggro[147] = true;
                        plr.npcTypeNoAggro[183] = true;
                        plr.npcTypeNoAggro[184] = true;
                        plr.npcTypeNoAggro[204] = true;
                        plr.npcTypeNoAggro[225] = true;
                        plr.npcTypeNoAggro[244] = true;
                        plr.npcTypeNoAggro[302] = true;
                        plr.npcTypeNoAggro[333] = true;
                        plr.npcTypeNoAggro[335] = true;
                        plr.npcTypeNoAggro[334] = true;
                        plr.npcTypeNoAggro[336] = true;
                        plr.npcTypeNoAggro[537] = true;
                    }
                    if (plr.armor[l].stringColor > 0)
                    {
                        plr.yoyoString = true;
                    }
                    if (plr.armor[l].type == 3366)
                    {
                        plr.counterWeight = 556 + Main.rand.Next(6);
                        plr.yoyoGlove = true;
                        plr.yoyoString = true;
                    }
                    if (plr.armor[l].type >= 3309 && plr.armor[l].type <= 3314)
                    {
                        plr.counterWeight = 556 + plr.armor[l].type - 3309;
                    }
                    if (plr.armor[l].type == 3334)
                    {
                        plr.yoyoGlove = true;
                    }
                    if (plr.armor[l].type == 3337)
                    {
                        plr.shinyStone = true;
                    }
                    if (plr.armor[l].type == 3336)
                    {
                        plr.SporeSac();
                        plr.sporeSac = true;
                    }
                    if (plr.armor[l].type == 2423)
                    {
                        plr.autoJump = true;
                        plr.jumpSpeedBoost += 2.4f;
                        plr.extraFall += 15;
                    }
                    if (plr.armor[l].type == 857)
                    {
                        plr.doubleJumpSandstorm = true;
                    }
                    if (plr.armor[l].type == 983)
                    {
                        plr.doubleJumpSandstorm = true;
                        plr.jumpBoost = true;
                    }
                    if (plr.armor[l].type == 987)
                    {
                        plr.doubleJumpBlizzard = true;
                    }
                    if (plr.armor[l].type == 1163)
                    {
                        plr.doubleJumpBlizzard = true;
                        plr.jumpBoost = true;
                    }
                    if (plr.armor[l].type == 1724)
                    {
                        plr.doubleJumpFart = true;
                    }
                    if (plr.armor[l].type == 1863)
                    {
                        plr.doubleJumpFart = true;
                        plr.jumpBoost = true;
                    }
                    if (plr.armor[l].type == 1164)
                    {
                        plr.doubleJumpCloud = true;
                        plr.doubleJumpSandstorm = true;
                        plr.doubleJumpBlizzard = true;
                        plr.jumpBoost = true;
                    }
                    if (plr.armor[l].type == 1250)
                    {
                        plr.jumpBoost = true;
                        plr.doubleJumpCloud = true;
                        plr.noFallDmg = true;
                    }
                    if (plr.armor[l].type == 1252)
                    {
                        plr.doubleJumpSandstorm = true;
                        plr.jumpBoost = true;
                        plr.noFallDmg = true;
                    }
                    if (plr.armor[l].type == 1251)
                    {
                        plr.doubleJumpBlizzard = true;
                        plr.jumpBoost = true;
                        plr.noFallDmg = true;
                    }
                    if (plr.armor[l].type == 3250)
                    {
                        plr.doubleJumpFart = true;
                        plr.jumpBoost = true;
                        plr.noFallDmg = true;
                    }
                    if (plr.armor[l].type == 3252)
                    {
                        plr.doubleJumpSail = true;
                        plr.jumpBoost = true;
                        plr.noFallDmg = true;
                    }
                    if (plr.armor[l].type == 3251)
                    {
                        plr.jumpBoost = true;
                        plr.bee = true;
                        plr.noFallDmg = true;
                    }
                    if (plr.armor[l].type == 1249)
                    {
                        plr.jumpBoost = true;
                        plr.bee = true;
                    }
                    if (plr.armor[l].type == 3241)
                    {
                        plr.jumpBoost = true;
                        plr.doubleJumpSail = true;
                    }
                    if (plr.armor[l].type == 1253 && (double)plr.statLife <= (double)plr.statLifeMax2 * 0.5)
                    {
                        plr.AddBuff(62, 5, true);
                    }
                    if (plr.armor[l].type == 1290)
                    {
                        plr.panic = true;
                    }
                    if ((plr.armor[l].type == 1300 || plr.armor[l].type == 1858) && (plr.inventory[plr.selectedItem].useAmmo == Terraria.ID.AmmoID.Bullet || plr.inventory[plr.selectedItem].useAmmo == Terraria.ID.AmmoID.CandyCorn || plr.inventory[plr.selectedItem].useAmmo == Terraria.ID.AmmoID.Stake || plr.inventory[plr.selectedItem].useAmmo == 23))
                    {
                        plr.scope = true;
                    }
                    if (plr.armor[l].type == 1858)
                    {
                        plr.rangedCrit += 10;
                        plr.rangedDamage += 0.1f;
                    }
                    if (plr.armor[l].type == 1303 && plr.wet)
                    {
                        Lighting.AddLight((int)plr.Center.X / 16, (int)plr.Center.Y / 16, 0.9f, 0.2f, 0.6f);
                    }
                    if (plr.armor[l].type == 1301)
                    {
                        plr.meleeCrit += 8;
                        plr.rangedCrit += 8;
                        plr.magicCrit += 8;
                        plr.thrownCrit += 8;
                        plr.meleeDamage += 0.1f;
                        plr.rangedDamage += 0.1f;
                        plr.magicDamage += 0.1f;
                        plr.minionDamage += 0.1f;
                        plr.thrownDamage += 0.1f;
                    }
                    if (plr.armor[l].type == 982)
                    {
                        plr.statManaMax2 += 20;
                        plr.manaRegenDelayBonus++;
                        plr.manaRegenBonus += 25;
                    }
                    if (plr.armor[l].type == 1595)
                    {
                        plr.statManaMax2 += 20;
                        plr.magicCuffs = true;
                    }
                    if (plr.armor[l].type == 2219)
                    {
                        plr.manaMagnet = true;
                    }
                    if (plr.armor[l].type == 2220)
                    {
                        plr.manaMagnet = true;
                        plr.magicDamage += 0.15f;
                    }
                    if (plr.armor[l].type == 2221)
                    {
                        plr.manaMagnet = true;
                        plr.magicCuffs = true;
                    }
                    if (plr.whoAmI == Main.myPlayer && plr.armor[l].type == 1923)
                    {
                        Player.tileRangeX++;
                        Player.tileRangeY++;
                    }
                    if (plr.armor[l].type == 1247)
                    {
                        plr.starCloak = true;
                        plr.bee = true;
                    }
                    if (plr.armor[l].type == 1248)
                    {
                        plr.meleeCrit += 10;
                        plr.rangedCrit += 10;
                        plr.magicCrit += 10;
                        plr.thrownCrit += 10;
                    }
                    if (plr.armor[l].type == 854)
                    {
                        plr.discount = true;
                    }
                    if (plr.armor[l].type == 855)
                    {
                        plr.coins = true;
                    }
                    if (plr.armor[l].type == 3033)
                    {
                        plr.goldRing = true;
                    }
                    if (plr.armor[l].type == 3034)
                    {
                        plr.goldRing = true;
                        plr.coins = true;
                    }
                    if (plr.armor[l].type == 3035)
                    {
                        plr.goldRing = true;
                        plr.coins = true;
                        plr.discount = true;
                    }
                    if (plr.armor[l].type == 53)
                    {
                        plr.doubleJumpCloud = true;
                    }
                    if (plr.armor[l].type == 3201)
                    {
                        plr.doubleJumpSail = true;
                    }
                    if (plr.armor[l].type == 54)
                    {
                        plr.accRunSpeed = 6f;
                    }
                    if (plr.armor[l].type == 3068)
                    {
                        plr.cordage = true;
                    }
                    if (plr.armor[l].type == 1579)
                    {
                        plr.accRunSpeed = 6f;
                        plr.coldDash = true;
                    }
                    if (plr.armor[l].type == 3200)
                    {
                        plr.accRunSpeed = 6f;
                        plr.sailDash = true;
                    }
                    if (plr.armor[l].type == 128)
                    {
                        plr.rocketBoots = 1;
                    }
                    if (plr.armor[l].type == 156)
                    {
                        plr.noKnockback = true;
                    }
                    if (plr.armor[l].type == 158)
                    {
                        plr.noFallDmg = true;
                    }
                    if (plr.armor[l].type == 934)
                    {
                        plr.carpet = true;
                    }
                    if (plr.armor[l].type == 953)
                    {
                        plr.spikedBoots++;
                    }
                    if (plr.armor[l].type == 975)
                    {
                        plr.spikedBoots++;
                    }
                    if (plr.armor[l].type == 976)
                    {
                        plr.spikedBoots += 2;
                    }
                    if (plr.armor[l].type == 977)
                    {
                        plr.dash = 1;
                    }
                    if (plr.armor[l].type == 3097)
                    {
                        plr.dash = 2;
                    }
                    if (plr.armor[l].type == 963)
                    {
                        plr.blackBelt = true;
                    }
                    if (plr.armor[l].type == 984)
                    {
                        plr.blackBelt = true;
                        plr.dash = 1;
                        plr.spikedBoots = 2;
                    }
                    if (plr.armor[l].type == 1131)
                    {
                        plr.gravControl2 = true;
                    }
                    if (plr.armor[l].type == 1132)
                    {
                        plr.bee = true;
                    }
                    if (plr.armor[l].type == 1578)
                    {
                        plr.bee = true;
                        plr.panic = true;
                    }
                    if (plr.armor[l].type == 3224)
                    {
                        plr.endurance += 0.17f;
                    }
                    if (plr.armor[l].type == 3223)
                    {
                        plr.brainOfConfusion = true;
                    }
                    if (plr.armor[l].type == 950)
                    {
                        plr.iceSkate = true;
                    }
                    if (plr.armor[l].type == 159)
                    {
                        plr.jumpBoost = true;
                    }
                    if (plr.armor[l].type == 3225)
                    {
                        plr.jumpBoost = true;
                    }
                    if (plr.armor[l].type == 187)
                    {
                        plr.accFlipper = true;
                    }
                    if (plr.armor[l].type == 211)
                    {
                        plr.meleeSpeed += 0.12f;
                    }
                    if (plr.armor[l].type == 223)
                    {
                        plr.manaCost -= 0.06f;
                    }
                    if (plr.armor[l].type == 285)
                    {
                        plr.moveSpeed += 0.05f;
                    }
                    if (plr.armor[l].type == 212)
                    {
                        plr.moveSpeed += 0.1f;
                    }
                    if (plr.armor[l].type == 267)
                    {
                        plr.killGuide = true;
                    }
                    if (plr.armor[l].type == 1307)
                    {
                        plr.killClothier = true;
                    }
                    if (plr.armor[l].type == 193)
                    {
                        plr.fireWalk = true;
                    }
                    if (plr.armor[l].type == 861)
                    {
                        plr.accMerman = true;
                        plr.wolfAcc = true;
                        if (plr.hideVisual[l])
                        {
                            plr.hideMerman = true;
                            plr.hideWolf = true;
                        }
                    }
                    if (plr.armor[l].type == 862)
                    {
                        plr.starCloak = true;
                        plr.longInvince = true;
                    }
                    if (plr.armor[l].type == 860)
                    {
                        plr.pStone = true;
                    }
                    if (plr.armor[l].type == 863)
                    {
                        plr.waterWalk2 = true;
                    }
                    if (plr.armor[l].type == 907)
                    {
                        plr.waterWalk2 = true;
                        plr.fireWalk = true;
                    }
                    if (plr.armor[l].type == 908)
                    {
                        plr.waterWalk = true;
                        plr.fireWalk = true;
                        plr.lavaMax += 420;
                    }
                    if (plr.armor[l].type == 906)
                    {
                        plr.lavaMax += 420;
                    }
                    if (plr.armor[l].type == 485)
                    {
                        plr.wolfAcc = true;
                        if (plr.hideVisual[l])
                        {
                            plr.hideWolf = true;
                        }
                    }
                    if (plr.armor[l].type == 486)
                    {
                        plr.rulerLine = true;
                    }
                    if (plr.armor[l].type == 2799)
                    {
                        plr.rulerGrid = true;
                    }
                    if (plr.armor[l].type == 394)
                    {
                        plr.accFlipper = true;
                        plr.accDivingHelm = true;
                    }
                    if (plr.armor[l].type == 396)
                    {
                        plr.noFallDmg = true;
                        plr.fireWalk = true;
                    }
                    if (plr.armor[l].type == 397)
                    {
                        plr.noKnockback = true;
                        plr.fireWalk = true;
                    }
                    if (plr.armor[l].type == 399)
                    {
                        plr.jumpBoost = true;
                        plr.doubleJumpCloud = true;
                    }
                    if (plr.armor[l].type == 405)
                    {
                        plr.accRunSpeed = 6f;
                        plr.rocketBoots = 2;
                    }
                    if (plr.armor[l].type == 1860)
                    {
                        plr.accFlipper = true;
                        plr.accDivingHelm = true;
                        if (plr.wet)
                        {
                            Lighting.AddLight((int)plr.Center.X / 16, (int)plr.Center.Y / 16, 0.9f, 0.2f, 0.6f);
                        }
                    }
                    if (plr.armor[l].type == 1861)
                    {
                        plr.arcticDivingGear = true;
                        plr.accFlipper = true;
                        plr.accDivingHelm = true;
                        plr.iceSkate = true;
                        if (plr.wet)
                        {
                            Lighting.AddLight((int)plr.Center.X / 16, (int)plr.Center.Y / 16, 0.2f, 0.8f, 0.9f);
                        }
                    }
                    if (plr.armor[l].type == 2214)
                    {
                        flag2 = true;
                    }
                    if (plr.armor[l].type == 2215)
                    {
                        flag3 = true;
                    }
                    if (plr.armor[l].type == 2216)
                    {
                        plr.autoPaint = true;
                    }
                    if (plr.armor[l].type == 2217)
                    {
                        flag = true;
                    }
                    if (plr.armor[l].type == 3061)
                    {
                        flag = true;
                        flag2 = true;
                        plr.autoPaint = true;
                        flag3 = true;
                    }
                    if (plr.armor[l].type == 3624)
                    {
                        plr.autoActuator = true;
                    }
                    if (plr.armor[l].type == 897)
                    {
                        plr.kbGlove = true;
                        plr.meleeSpeed += 0.12f;
                    }
                    if (plr.armor[l].type == 1343)
                    {
                        plr.kbGlove = true;
                        plr.meleeSpeed += 0.1f;
                        plr.meleeDamage += 0.1f;
                        plr.magmaStone = true;
                    }
                    if (plr.armor[l].type == 1167)
                    {
                        plr.minionKB += 2f;
                        plr.minionDamage += 0.15f;
                    }
                    if (plr.armor[l].type == 1864)
                    {
                        plr.minionKB += 2f;
                        plr.minionDamage += 0.15f;
                        plr.maxMinions++;
                    }
                    if (plr.armor[l].type == 1845)
                    {
                        plr.minionDamage += 0.1f;
                        plr.maxMinions++;
                    }
                    if (plr.armor[l].type == 1321)
                    {
                        plr.magicQuiver = true;
                        plr.arrowDamage += 0.1f;
                    }
                    if (plr.armor[l].type == 1322)
                    {
                        plr.magmaStone = true;
                    }
                    if (plr.armor[l].type == 1323)
                    {
                        plr.lavaRose = true;
                    }
                    if (plr.armor[l].type == 3333)
                    {
                        plr.strongBees = true;
                    }
                    if (plr.armor[l].type == 938)
                    {
                        plr.noKnockback = true;
                        if ((float)plr.statLife > (float)plr.statLifeMax2 * 0.25f)
                        {
                            plr.hasPaladinShield = true;
                            if (i != Main.myPlayer && plr.miscCounter % 10 == 0)
                            {
                                int myPlayer = Main.myPlayer;
                                if (Main.player[myPlayer].team == plr.team && plr.team != 0)
                                {
                                    float arg_4364_0 = plr.position.X - Main.player[myPlayer].position.X;
                                    float num3 = plr.position.Y - Main.player[myPlayer].position.Y;
                                    if ((float)Math.Sqrt((double)(arg_4364_0 * arg_4364_0 + num3 * num3)) < 800f)
                                    {
                                        Main.player[myPlayer].AddBuff(43, 20, true);
                                    }
                                }
                            }
                        }
                    }
                    if (plr.armor[l].type == 936)
                    {
                        plr.kbGlove = true;
                        plr.meleeSpeed += 0.12f;
                        plr.meleeDamage += 0.12f;
                    }
                    if (plr.armor[l].type == 898)
                    {
                        plr.accRunSpeed = 6.75f;
                        plr.rocketBoots = 2;
                        plr.moveSpeed += 0.08f;
                    }
                    if (plr.armor[l].type == 1862)
                    {
                        plr.accRunSpeed = 6.75f;
                        plr.rocketBoots = 3;
                        plr.moveSpeed += 0.08f;
                        plr.iceSkate = true;
                    }
                    if (plr.armor[l].type == 3110)
                    {
                        plr.accMerman = true;
                        plr.wolfAcc = true;
                        if (plr.hideVisual[l])
                        {
                            plr.hideMerman = true;
                            plr.hideWolf = true;
                        }
                    }
                    if (plr.armor[l].type == 1865 || plr.armor[l].type == 3110)
                    {
                        plr.lifeRegen += 2;
                        plr.statDefense += 4;
                        plr.meleeSpeed += 0.1f;
                        plr.meleeDamage += 0.1f;
                        plr.meleeCrit += 2;
                        plr.rangedDamage += 0.1f;
                        plr.rangedCrit += 2;
                        plr.magicDamage += 0.1f;
                        plr.magicCrit += 2;
                        plr.pickSpeed -= 0.15f;
                        plr.minionDamage += 0.1f;
                        plr.minionKB += 0.5f;
                        plr.thrownDamage += 0.1f;
                        plr.thrownCrit += 2;
                    }
                    if (plr.armor[l].type == 899 && Main.dayTime)
                    {
                        plr.lifeRegen += 2;
                        plr.statDefense += 4;
                        plr.meleeSpeed += 0.1f;
                        plr.meleeDamage += 0.1f;
                        plr.meleeCrit += 2;
                        plr.rangedDamage += 0.1f;
                        plr.rangedCrit += 2;
                        plr.magicDamage += 0.1f;
                        plr.magicCrit += 2;
                        plr.pickSpeed -= 0.15f;
                        plr.minionDamage += 0.1f;
                        plr.minionKB += 0.5f;
                        plr.thrownDamage += 0.1f;
                        plr.thrownCrit += 2;
                    }
                    if (plr.armor[l].type == 900 && (!Main.dayTime || Main.eclipse))
                    {
                        plr.lifeRegen += 2;
                        plr.statDefense += 4;
                        plr.meleeSpeed += 0.1f;
                        plr.meleeDamage += 0.1f;
                        plr.meleeCrit += 2;
                        plr.rangedDamage += 0.1f;
                        plr.rangedCrit += 2;
                        plr.magicDamage += 0.1f;
                        plr.magicCrit += 2;
                        plr.pickSpeed -= 0.15f;
                        plr.minionDamage += 0.1f;
                        plr.minionKB += 0.5f;
                        plr.thrownDamage += 0.1f;
                        plr.thrownCrit += 2;
                    }
                    if (plr.armor[l].type == 407)
                    {
                        plr.blockRange = 1;
                    }
                    if (plr.armor[l].type == 489)
                    {
                        plr.magicDamage += 0.15f;
                    }
                    if (plr.armor[l].type == 490)
                    {
                        plr.meleeDamage += 0.15f;
                    }
                    if (plr.armor[l].type == 491)
                    {
                        plr.rangedDamage += 0.15f;
                    }
                    if (plr.armor[l].type == 2998)
                    {
                        plr.minionDamage += 0.15f;
                    }
                    if (plr.armor[l].type == 935)
                    {
                        plr.magicDamage += 0.12f;
                        plr.meleeDamage += 0.12f;
                        plr.rangedDamage += 0.12f;
                        plr.minionDamage += 0.12f;
                        plr.thrownDamage += 0.12f;
                    }
                    if (plr.armor[l].type == 492)
                    {
                        plr.wingTimeMax = 100;
                    }
                    if (plr.armor[l].type == 493)
                    {
                        plr.wingTimeMax = 100;
                    }
                    if (plr.armor[l].type == 748)
                    {
                        plr.wingTimeMax = 115;
                    }
                    if (plr.armor[l].type == 749)
                    {
                        plr.wingTimeMax = 130;
                    }
                    if (plr.armor[l].type == 761)
                    {
                        plr.wingTimeMax = 130;
                    }
                    if (plr.armor[l].type == 785)
                    {
                        plr.wingTimeMax = 140;
                    }
                    if (plr.armor[l].type == 786)
                    {
                        plr.wingTimeMax = 140;
                    }
                    if (plr.armor[l].type == 821)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 822)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 823)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 2280)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 2494)
                    {
                        plr.wingTimeMax = 100;
                    }
                    if (plr.armor[l].type == 2609)
                    {
                        plr.wingTimeMax = 180;
                        plr.ignoreWater = true;
                    }
                    if (plr.armor[l].type == 948)
                    {
                        plr.wingTimeMax = 180;
                    }
                    if (plr.armor[l].type == 1162)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 1165)
                    {
                        plr.wingTimeMax = 140;
                    }
                    if (plr.armor[l].type == 1515)
                    {
                        plr.wingTimeMax = 130;
                    }
                    if (plr.armor[l].type == 665)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 1583)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 1584)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 1585)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 1586)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 3228)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 3580)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 3582)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 3588)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 3592)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 3924)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 3928)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 1797)
                    {
                        plr.wingTimeMax = 180;
                    }
                    if (plr.armor[l].type == 1830)
                    {
                        plr.wingTimeMax = 180;
                    }
                    if (plr.armor[l].type == 1866)
                    {
                        plr.wingTimeMax = 170;
                    }
                    if (plr.armor[l].type == 1871)
                    {
                        plr.wingTimeMax = 170;
                    }
                    if (plr.armor[l].type == 2770)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 3468)
                    {
                        plr.wingTimeMax = 180;
                    }
                    if (plr.armor[l].type == 3469)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 3470)
                    {
                        plr.wingTimeMax = 160;
                    }
                    if (plr.armor[l].type == 3471)
                    {
                        plr.wingTimeMax = 180;
                    }
                    if (plr.armor[l].type == 3883)
                    {
                        plr.wingTimeMax = 150;
                    }
                    if (plr.armor[l].type == 885)
                    {
                        plr.buffImmune[30] = true;
                    }
                    if (plr.armor[l].type == 886)
                    {
                        plr.buffImmune[36] = true;
                    }
                    if (plr.armor[l].type == 887)
                    {
                        plr.buffImmune[20] = true;
                    }
                    if (plr.armor[l].type == 888)
                    {
                        plr.buffImmune[22] = true;
                    }
                    if (plr.armor[l].type == 889)
                    {
                        plr.buffImmune[32] = true;
                    }
                    if (plr.armor[l].type == 890)
                    {
                        plr.buffImmune[35] = true;
                    }
                    if (plr.armor[l].type == 891)
                    {
                        plr.buffImmune[23] = true;
                    }
                    if (plr.armor[l].type == 892)
                    {
                        plr.buffImmune[33] = true;
                    }
                    if (plr.armor[l].type == 893)
                    {
                        plr.buffImmune[31] = true;
                    }
                    if (plr.armor[l].type == 3781)
                    {
                        plr.buffImmune[156] = true;
                    }
                    if (plr.armor[l].type == 901)
                    {
                        plr.buffImmune[33] = true;
                        plr.buffImmune[36] = true;
                    }
                    if (plr.armor[l].type == 902)
                    {
                        plr.buffImmune[30] = true;
                        plr.buffImmune[20] = true;
                    }
                    if (plr.armor[l].type == 903)
                    {
                        plr.buffImmune[32] = true;
                        plr.buffImmune[31] = true;
                    }
                    if (plr.armor[l].type == 904)
                    {
                        plr.buffImmune[35] = true;
                        plr.buffImmune[23] = true;
                    }
                    if (plr.armor[l].type == 1921)
                    {
                        plr.buffImmune[46] = true;
                        plr.buffImmune[47] = true;
                    }
                    if (plr.armor[l].type == 1612)
                    {
                        plr.buffImmune[33] = true;
                        plr.buffImmune[36] = true;
                        plr.buffImmune[30] = true;
                        plr.buffImmune[20] = true;
                        plr.buffImmune[32] = true;
                        plr.buffImmune[31] = true;
                        plr.buffImmune[35] = true;
                        plr.buffImmune[23] = true;
                        plr.buffImmune[22] = true;
                    }
                    if (plr.armor[l].type == 1613)
                    {
                        plr.buffImmune[46] = true;
                        plr.noKnockback = true;
                        plr.fireWalk = true;
                        plr.buffImmune[33] = true;
                        plr.buffImmune[36] = true;
                        plr.buffImmune[30] = true;
                        plr.buffImmune[20] = true;
                        plr.buffImmune[32] = true;
                        plr.buffImmune[31] = true;
                        plr.buffImmune[35] = true;
                        plr.buffImmune[23] = true;
                        plr.buffImmune[22] = true;
                    }
                    if (plr.armor[l].type == 497)
                    {
                        plr.accMerman = true;
                        if (plr.hideVisual[l])
                        {
                            plr.hideMerman = true;
                        }
                    }
                    if (plr.armor[l].type == 535)
                    {
                        plr.pStone = true;
                    }
                    if (plr.armor[l].type == 536)
                    {
                        plr.kbGlove = true;
                    }
                    if (plr.armor[l].type == 532)
                    {
                        plr.starCloak = true;
                    }
                    if (plr.armor[l].type == 554)
                    {
                        plr.longInvince = true;
                    }
                    if (plr.armor[l].type == 555)
                    {
                        plr.manaFlower = true;
                        plr.manaCost -= 0.08f;
                    }
                    if (Main.myPlayer == plr.whoAmI)
                    {
                        if (plr.armor[l].type == 576 && Main.rand.Next(10800) == 0 && Main.curMusic > 0 && Main.curMusic <= 41)
                        {
                            int num4 = 0;
                            if (Main.curMusic == 1)
                            {
                                num4 = 0;
                            }
                            if (Main.curMusic == 2)
                            {
                                num4 = 1;
                            }
                            if (Main.curMusic == 3)
                            {
                                num4 = 2;
                            }
                            if (Main.curMusic == 4)
                            {
                                num4 = 4;
                            }
                            if (Main.curMusic == 5)
                            {
                                num4 = 5;
                            }
                            if (Main.curMusic == 6)
                            {
                                num4 = 3;
                            }
                            if (Main.curMusic == 7)
                            {
                                num4 = 6;
                            }
                            if (Main.curMusic == 8)
                            {
                                num4 = 7;
                            }
                            if (Main.curMusic == 9)
                            {
                                num4 = 9;
                            }
                            if (Main.curMusic == 10)
                            {
                                num4 = 8;
                            }
                            if (Main.curMusic == 11)
                            {
                                num4 = 11;
                            }
                            if (Main.curMusic == 12)
                            {
                                num4 = 10;
                            }
                            if (Main.curMusic == 13)
                            {
                                num4 = 12;
                            }
                            if (Main.curMusic == 28)
                            {
                                plr.armor[l].SetDefaults(1963, false);
                            }
                            else if (Main.curMusic == 29)
                            {
                                plr.armor[l].SetDefaults(1610, false);
                            }
                            else if (Main.curMusic == 30)
                            {
                                plr.armor[l].SetDefaults(1963, false);
                            }
                            else if (Main.curMusic == 31)
                            {
                                plr.armor[l].SetDefaults(1964, false);
                            }
                            else if (Main.curMusic == 32)
                            {
                                plr.armor[l].SetDefaults(1965, false);
                            }
                            else if (Main.curMusic == 33)
                            {
                                plr.armor[l].SetDefaults(2742, false);
                            }
                            else if (Main.curMusic == 34)
                            {
                                plr.armor[l].SetDefaults(3370, false);
                            }
                            else if (Main.curMusic == 35)
                            {
                                plr.armor[l].SetDefaults(3236, false);
                            }
                            else if (Main.curMusic == 36)
                            {
                                plr.armor[l].SetDefaults(3237, false);
                            }
                            else if (Main.curMusic == 37)
                            {
                                plr.armor[l].SetDefaults(3235, false);
                            }
                            else if (Main.curMusic == 38)
                            {
                                plr.armor[l].SetDefaults(3044, false);
                            }
                            else if (Main.curMusic == 39)
                            {
                                plr.armor[l].SetDefaults(3371, false);
                            }
                            else if (Main.curMusic == 40)
                            {
                                plr.armor[l].SetDefaults(3796, false);
                            }
                            else if (Main.curMusic == 41)
                            {
                                plr.armor[l].SetDefaults(3869, false);
                            }
                            else if (Main.curMusic > 13)
                            {
                                plr.armor[l].SetDefaults(1596 + Main.curMusic - 14, false);
                            }
                            else
                            {
                                plr.armor[l].SetDefaults(num4 + 562, false);
                            }
                        }
                        if (plr.armor[l].type >= 562 && plr.armor[l].type <= 574)
                        {
                            Main.musicBox2 = plr.armor[l].type - 562;
                        }
                        if (plr.armor[l].type >= 1596 && plr.armor[l].type <= 1609)
                        {
                            Main.musicBox2 = plr.armor[l].type - 1596 + 13;
                        }
                        if (plr.armor[l].type == 1610)
                        {
                            Main.musicBox2 = 27;
                        }
                        if (plr.armor[l].type == 1963)
                        {
                            Main.musicBox2 = 28;
                        }
                        if (plr.armor[l].type == 1964)
                        {
                            Main.musicBox2 = 29;
                        }
                        if (plr.armor[l].type == 1965)
                        {
                            Main.musicBox2 = 30;
                        }
                        if (plr.armor[l].type == 2742)
                        {
                            Main.musicBox2 = 31;
                        }
                        if (plr.armor[l].type == 3044)
                        {
                            Main.musicBox2 = 32;
                        }
                        if (plr.armor[l].type == 3235)
                        {
                            Main.musicBox2 = 33;
                        }
                        if (plr.armor[l].type == 3236)
                        {
                            Main.musicBox2 = 34;
                        }
                        if (plr.armor[l].type == 3237)
                        {
                            Main.musicBox2 = 35;
                        }
                        if (plr.armor[l].type == 3370)
                        {
                            Main.musicBox2 = 36;
                        }
                        if (plr.armor[l].type == 3371)
                        {
                            Main.musicBox2 = 37;
                        }
                        if (plr.armor[l].type == 3796)
                        {
                            Main.musicBox2 = 38;
                        }
                        if (plr.armor[l].type == 3869)
                        {
                            Main.musicBox2 = 39;
                        }
                    }
                }
            }
        }

    }
}
