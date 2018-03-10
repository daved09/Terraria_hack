using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class Buffs
    {
        public static bool Shine = false;
        public static bool Nebula = false;
        public static FieldHelper BuffID = new FieldHelper(typeof(Terraria.ID.BuffID));

        public static FieldHelper.Field<int>[] ConstantBuffs = new[]
            {
                BuffID.GetField<int>("Endurance"),
                BuffID.GetField<int>("Rage"),
                BuffID.GetField<int>("Swiftness"),
                BuffID.GetField<int>("WellFed"),
                BuffID.GetField<int>("Wrath")
            };

        public static string ConstantBuffsString()
        {
            return string.Format("[{0}]", string.Join("|", ConstantBuffs.Select(x => x.GetText("BuffName")).OrderBy(x => x)) + (Nebula ? "|" + "Nebula3" : ""));
        }

        public static void UpdateBuffsPost(Player plr)
        {
            plr.extraAccessory = true;
            plr.extraAccessorySlots = 2;

            //plr.maxMinions = 100;
            /*if (Globals.IsPrivate)
                plr.maxMinions *= 10;
            else
                plr.maxMinions *= 5;
            */
            if(Shine)
                Lighting.AddLight((int)(plr.position.X + (float)(plr.width / 2)) / 16, (int)(plr.position.Y + (float)(plr.height / 2)) / 16, 0.8f, 0.95f, 1f); //Constant shine buff

            if(Nebula)
            {
                if(Globals.IsPrivate)
                {
                    //Emulate Nebula Life 3
                    plr.lifeRegen += 10 * 3; //10 * nebulaLevelLife

                    //Emulate Nebula Mana 3
                    int num = 6;
                    plr.nebulaManaCounter += 3;
                    if (plr.nebulaManaCounter >= num)
                    {
                        plr.nebulaManaCounter -= num;
                        plr.statMana++;
                        if (plr.statMana > plr.statManaMax2)
                            plr.statMana = plr.statManaMax2;
                    }

                    //Emulate Nebula Damage 3
                    float nebulaLevelDamage = 0.15f * 3f; //0.15f * (float)nebulaLevelDamage
                    plr.meleeDamage += nebulaLevelDamage;
                    plr.rangedDamage += nebulaLevelDamage;
                    plr.magicDamage += nebulaLevelDamage;
                    plr.minionDamage += nebulaLevelDamage;
                    plr.thrownDamage += nebulaLevelDamage;
                }
                else
                {
                    plr.nebulaLevelLife = 3;
                    plr.nebulaLevelDamage = 3;
                    plr.nebulaLevelMana = 3;
                }
            }

            foreach(var x in ConstantBuffs)
                plr.AddBuff(x.Value, 1, false);
        }

        public static bool SendChatMessagePre(string cmd, string[] args)
        {
            switch(cmd)
            {
                case "heal":
                    var ply = Globals.GetLocalPlayer();
                    ply.statLife = ply.statLifeMax2;
                    Globals.Print("You have been healed! Greetz by [pCoder]n4m3nl0s lel");
                    break;
                case "shine":
                    Shine = !Shine;
                    Globals.Print("Permanent Shine buff {0}", Shine ? "Enabled" : "Disabled");
                    break;
                case "nebula":
                    Nebula = !Nebula;
                    Globals.Print("Nebula buffs {0}", Nebula ? "Enabled" : "Disabled");
                    break;
                case "buff":
                case "b":
                    if(args.Length < 2)
                    {
                        Globals.Print("Invalid args count!");
                        break;
                    }

                    var fldName = Debug.FindFieldName("BuffName.", args[0]);
                    var buff = null as FieldHelper.Field<int>;
                    if (fldName == string.Empty)
                        if (!BuffID.Contains(args[0]))
                        {
                            var id = 0;
                            if (!args[0].TryConvert(out id) || (buff = BuffID.GetFieldByValue(id)) == null)
                            {
                                Globals.Print("Invalid Buff name/id!");
                                break;
                            }
                        }
                        else
                            buff = BuffID.GetField<int>(args[0]);
                    else
                        buff = BuffID.GetField<int>(fldName);

                    var time = 0;
                    if(!args[1].TryConvert(out time))
                    {
                        Globals.Print("Invalid time!");
                        break;
                    }

                    Globals.GetLocalPlayer().AddBuff(buff.Value, time * 60, false); //Time * Frames per Second

                    Globals.Print("Added {0} for {1}", Debug.FindFieldLangname("BuffName." + buff.Name), TimeSpan.FromSeconds(time).ToString());

                    break;
                default:
                    return false;
            }

            return true;
        }

        protected static void OldUpdateBuffs(Player plr)
        {
            //Emulate Nebula Life 3
            plr.lifeRegen += 10 * 3; //10 * nebulaLevelLife

            //Emulate Nebula Mana 3
            //plr.nebulaLevelMana = 3;
            plr.statMana += 3;

            //Emulate Nebula Damage 3
            float nebulaLevelDamage = 0.15f * 3f; //0.15f * (float)nebulaLevelDamage
            plr.meleeDamage += nebulaLevelDamage;
            plr.rangedDamage += nebulaLevelDamage;
            plr.magicDamage += nebulaLevelDamage;
            plr.minionDamage += nebulaLevelDamage;
            plr.thrownDamage += nebulaLevelDamage;

            //Gravitation
            plr.gravControl = true;

            //Featherfall
            plr.slowFall = true;

            //Spelunker
            plr.findTreasure = true;

            //Thorns
            plr.thorns += 0.333333343f;

            //Waterwalking
            plr.waterWalk = true;

            //Random enchantment
            plr.meleeEnchant = (byte)Main.rand.Next(1, 9);

            //Ammo box
            plr.ammoBox = true;

            //Danger Sense
            plr.dangerSense = true;

            //AmmoReservation
            plr.ammoPotion = true;

            plr.moveSpeed += 0.5f; //0.25f;

            //Heartreach
            plr.lifeMagnet = true;

            //Endurance
            plr.endurance += 0.1f;

            //Rage
            plr.meleeCrit += 10;
            plr.rangedCrit += 10;
            plr.magicCrit += 10;
            plr.thrownCrit += 10;

            //Wrath (Candidate to disable bc it's to op with Nebula DMG)
            plr.thrownDamage += 0.1f;
            plr.meleeDamage += 0.1f;
            plr.rangedDamage += 0.1f;
            plr.magicDamage += 0.1f;
            plr.minionDamage += 0.1f;
        }
    }
}
