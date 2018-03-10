using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace SLS
{
    class Inventory
    {
        static List<Tuple<int, Item>> _Inventory = new List<Tuple<int, Item>>(); //Whole inventory
        static List<Tuple<int, Item>> _Equip = new List<Tuple<int, Item>>(); //Equip
        static List<Tuple<int, Item>> _Armor = new List<Tuple<int, Item>>(); //Armor and Accessory
        static List<Tuple<int, Item>> _MiscDyes = new List<Tuple<int, Item>>(); //Equip
        static List<Tuple<int, Item>> _Dye = new List<Tuple<int, Item>>(); //Armor and Accessorys
        static int statLifeMax = 0;
        static int statLifeMax2 = 0;
        static int statLifeMana = 0;
        static int statLifeMana2 = 0;

        public static bool SendchatMessagePre(string cmd, string[] args)
        {
            var ply = Globals.GetLocalPlayer();

            switch(cmd)
            {
                case "saveinventory":
                case "si":
                    _Inventory = new List<Tuple<int, Item>>();
                    _Equip = new List<Tuple<int, Item>>();
                    _Armor = new List<Tuple<int, Item>>();
                    _MiscDyes = new List<Tuple<int, Item>>();
                    _Dye = new List<Tuple<int, Item>>();
                    for (var i = 0; i < ply.inventory.Length; i++)
                        _Inventory.Add(new Tuple<int, Item>(i, ply.inventory[i].DeepClone()));
                    for (var i = 0; i < ply.miscEquips.Length; i++)
                        _Equip.Add(new Tuple<int, Item>(i, ply.miscEquips[i].DeepClone()));
                    for (var i = 0; i < ply.armor.Length; i++)
                        _Armor.Add(new Tuple<int, Item>(i, ply.armor[i].DeepClone()));
                    for (var i = 0; i < ply.miscDyes.Length; i++)
                        _MiscDyes.Add(new Tuple<int, Item>(i, ply.miscDyes[i].DeepClone()));
                    for (var i = 0; i < ply.dye.Length; i++)
                        _Dye.Add(new Tuple<int, Item>(i, ply.dye[i].DeepClone()));
                    statLifeMax = ply.statLife;
                    statLifeMax2 = ply.statLifeMax2;
                    statLifeMana = ply.statManaMax;
                    statLifeMana2 = ply.statManaMax2;

                    Globals.Print("Inventory saved!");
                    break;

                case "restoreinventory":
                case "ri":
                    Hooks.FixPrefixes = true;
                    foreach(var x in _Inventory)
                        ply.inventory[x.Item1] = x.Item2.DeepClone();
                    foreach (var x in _Equip)
                        ply.miscEquips[x.Item1] = x.Item2.DeepClone();
                    foreach (var x in _Armor)
                        ply.armor[x.Item1] = x.Item2.DeepClone();
                    foreach (var x in _MiscDyes)
                        ply.miscDyes[x.Item1] = x.Item2.DeepClone();
                    foreach (var x in _Dye)
                        ply.dye[x.Item1] = x.Item2.DeepClone();
                    ply.statLifeMax = statLifeMax;
                    ply.statLifeMax2 = statLifeMax2;
                    ply.statManaMax = statLifeMana;
                    ply.statManaMax2 = statLifeMana2;

                    Globals.Print("Inventory restored!");
                    break;

                case "copyinventory":
                case "ci":
                    if(args.Length < 1)
                    {
                        Globals.Print("Missing argument: Player name");
                        return true;
                    }
                    Player plr;
                    if(!PlayerUtil.TryGetPlayer(args[0], out plr))
                    {
                        Globals.Print("Player not found!");
                        return true;
                    }
                    Hooks.FixPrefixes = false;
                    ply.statLifeMax = plr.statLifeMax;
                    ply.statLifeMax2 = plr.statLifeMax2;
                    ply.statManaMax = plr.statManaMax;
                    ply.statManaMax2 = plr.statManaMax2;

                    for (var i = 0; i < plr.inventory.Length; i++)
                        ply.inventory[i] = plr.inventory[i].DeepClone();
                    for (var i = 0; i < plr.miscEquips.Length; i++)
                        ply.miscEquips[i] = plr.miscEquips[i].DeepClone();
                    for (var i = 0; i < plr.armor.Length; i++)
                        ply.armor[i] = plr.armor[i].DeepClone();
                    for (var i = 0; i < plr.miscDyes.Length; i++)
                        ply.miscDyes[i] = plr.miscDyes[i].DeepClone();
                    for (var i = 0; i < plr.dye.Length; i++)
                        ply.dye[i] = plr.dye[i].DeepClone();

                    Globals.Print("Successfully cloned {0}'s inventory", plr.name);

                    break;

                default:
                    return false;
            }

            return true;
        }
    }
}
