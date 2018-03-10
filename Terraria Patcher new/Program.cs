using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Mono.Cecil.Cil;
using Mono.Cecil;

namespace Terraria_Patcher_new
{
    class Program
    {
        static void Main(string[] args)
        {
            var fiCur = new FileInfo("Terraria.exe");
            var fiBack = new FileInfo(fiCur.FullName + ".bak");

            if (!fiBack.Exists)
                fiCur.CopyTo(fiBack.FullName);
            else
                fiBack.CopyTo(fiCur.FullName, true);

            var asmDef = AssemblyDefinition.ReadAssembly(fiCur.FullName);
            var modDef = asmDef.MainModule;

            var slsHooks = modDef.Import(typeof(SLS.Hooks)).Resolve();

            {
                var main = IL.GetTypeDefinition(modDef, "Main");
                var initialize = IL.GetMethodDefinition(main, "Initialize");
                var update = IL.GetMethodDefinition(main, "Update");
                var toggleChat = IL.GetMethodDefinition(main, "DoUpdate_Enter_ToggleChat");
                var handleChat = IL.GetMethodDefinition(main, "DoUpdate_HandleChat");
                var drawMenu = IL.GetMethodDefinition(main, "DrawMenu");
                var doDraw = IL.GetMethodDefinition(main, "DoDraw");
                var doUpdate = IL.GetMethodDefinition(main, "DoUpdate");
                var mouseText_DrawItemTooltip = IL.GetMethodDefinition(main, "MouseText_DrawItemTooltip");
                var damageVar = IL.GetMethodDefinition(main, "DamageVar");

                var slsInitializePre = modDef.Import(IL.GetMethodDefinition(slsHooks, "InitializePre"));
                var slsUpdatePre = modDef.Import(IL.GetMethodDefinition(slsHooks, "UpdatePre"));
                var slsChatPatch = modDef.Import(IL.GetFieldDefinition(slsHooks, "ChatPatch"));
                var slsDrawMenuPost = modDef.Import(IL.GetMethodDefinition(slsHooks, "DrawMenuPost"));
                var slsDoDrawPost = modDef.Import(IL.GetMethodDefinition(slsHooks, "DoDrawPost"));
                var slsMouseText_DrawItemTooltip = modDef.Import(IL.GetMethodDefinition(slsHooks, "MouseText_DrawItemTooltipPre"));

                IL.MethodPrepend(initialize, new[]
                    {
                        Instruction.Create(OpCodes.Call, slsInitializePre)
                    });
                Console.WriteLine("Main::Initialize() pre Hook added!");

                IL.MethodPrepend(update, new[]
                {
                    Instruction.Create(OpCodes.Call, slsUpdatePre)
                });
                Console.WriteLine("Main::Update() pre Hook added!");

                int spot = IL.ScanForOpcodePattern(toggleChat, (i, instruction) =>
                    {
                        var f0 = instruction.Operand as FieldReference;
                        return f0 != null && f0.Name == "netMode";
                    }, OpCodes.Ldsfld,
                    OpCodes.Ldc_I4_1);

                toggleChat.Body.Instructions[spot].OpCode = OpCodes.Nop;
                toggleChat.Body.Instructions[spot + 1].OpCode = OpCodes.Nop;
                toggleChat.Body.Instructions[spot + 2].OpCode = OpCodes.Nop;
                Console.WriteLine("Main::DoUpdate_Enter_ToggleChat() Patched!");

                spot = IL.ScanForOpcodePattern(handleChat, (i, instruction) =>
                    {
                        var f0 = instruction.Operand as FieldReference;
                        return f0 != null && f0.Name == "netMode";
                    }, OpCodes.Ldsfld,
                    OpCodes.Brtrue_S);

                handleChat.Body.Instructions[spot].Operand = slsChatPatch;
                Console.WriteLine("Main::DoUpdate_HandleChat Patched!");

                IL.MethodAppend(drawMenu, drawMenu.Body.Instructions.Count - 1, 1, new[]
                {
                    Instruction.Create(OpCodes.Call, slsDrawMenuPost),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Main::DrawMenu(GameTime) post Hook added!");

                IL.MethodAppend(doDraw, doDraw.Body.Instructions.Count - 1, 1, new[]
                {
                    Instruction.Create(OpCodes.Call, slsDoDrawPost),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Main::DoDraw(GameTime) post Hook added!");
                
                /*var doUpdateSpot = IL.ScanForOpcodePattern(doUpdate, (i, instruction) =>
                    {
                        var f0 = instruction.Operand as FieldReference;

                        return f0 != null && f0.Name == "UpdateTimeAccumulator";
                    },
                    OpCodes.Stsfld,
                    OpCodes.Ldsfld,
                    OpCodes.Ldc_R8);


                Console.WriteLine("Main::DoUpdate(GameTime) patched! (Max fps set to 74)");

                doUpdate.Body.Instructions[doUpdateSpot + 2] = Instruction.Create(OpCodes.Ldc_R8, (double)(1d / 74d)); //1 Second / 74 Pictures
                doUpdate.Body.Instructions[doUpdateSpot + 14] = Instruction.Create(OpCodes.Ldc_R8, (double)(1000d / 74d));
                doUpdate.Body.Instructions[doUpdateSpot + 20] = Instruction.Create(OpCodes.Ldc_R8, (double)(1d / 74d));
                doUpdate.Body.Instructions[doUpdateSpot + 24] = Instruction.Create(OpCodes.Ldc_R8, (double)(1d / 74d));*/

                var drawTooltipFirst = mouseText_DrawItemTooltip.Body.Instructions.First();
                IL.MethodPrepend(mouseText_DrawItemTooltip, new[]
                {
                    Instruction.Create(OpCodes.Ldarg, mouseText_DrawItemTooltip.Parameters[0]),
                    Instruction.Create(OpCodes.Ldarg, mouseText_DrawItemTooltip.Parameters[1]),
                    Instruction.Create(OpCodes.Ldarg, mouseText_DrawItemTooltip.Parameters[2]),
                    Instruction.Create(OpCodes.Ldarg, mouseText_DrawItemTooltip.Parameters[3]),
                    Instruction.Create(OpCodes.Call, slsMouseText_DrawItemTooltip),
                    Instruction.Create(OpCodes.Brfalse, drawTooltipFirst),
                    Instruction.Create(OpCodes.Ret)
                });

                damageVar.Body.Instructions.Clear();
                IL.MethodAppend(damageVar, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Conv_I4),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Main::DamageVar(float) patched ;)");
            }

            {
                var player = IL.GetTypeDefinition(modDef, "Player");
                var itemCheck = IL.GetMethodDefinition(player, "ItemCheck");
                var update = IL.GetMethodDefinition(player, "Update");
                var updateBuffs = IL.GetMethodDefinition(player, "UpdateBuffs");
                var updateEquips = IL.GetMethodDefinition(player, "UpdateEquips");
                var hurtMe = IL.GetMethodDefinition(player, "Hurt");
                var grabItems = IL.GetMethodDefinition(player, "GrabItems");
                var horizontalMovement = IL.GetMethodDefinition(player, "HorizontalMovement");
                var getItem = IL.GetMethodDefinition(player, "GetItem");
                var pickAmmo = IL.GetMethodDefinition(player, "PickAmmo");

                var ctor = IL.GetMethodDefinition(player, ".ctor");
                var slsItemCheckPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "ItemCheckPre"));
                var slsUpdatePlayerPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "UpdatePlayerPre"));
                var slsUpdatePlayerPost = modDef.Import(IL.GetMethodDefinition(slsHooks, "UpdatePlayerPost"));
                var slsUpdateBuffsPost = modDef.Import(IL.GetMethodDefinition(slsHooks, "UpdateBuffsPost"));
                var slsUpdateEquipsPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "UpdateEquipsPre"));
                var slsUpdateEquipsPost = modDef.Import(IL.GetMethodDefinition(slsHooks, "UpdateEquipsPost"));
                var slsHurtMeMid = modDef.Import(IL.GetMethodDefinition(slsHooks, "HurtMeMid"));
                var slsHurtMePost = modDef.Import(IL.GetMethodDefinition(slsHooks, "HurtMePost"));
                var slsGrabItemsPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "GrabItemsPre"));
                var slsHorizontalMovementPost = modDef.Import(IL.GetMethodDefinition(slsHooks, "HorizontalMovementPost"));
                var slsGetItemPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "GetItemPre"));
                var slsPickAmmoPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "PickAmmoPre"));

                var ctorSpot = ctor.Body.Instructions.IndexOf(ctor.Body.Instructions.FirstOrDefault(x => x.OpCode == OpCodes.Ldc_I4_S && Convert.ToInt32(x.Operand) == 20));
                //ctor.Body.Instructions[ctorSpot] = Instruction.Create(OpCodes.Ldc_I4, (int)23);
                //Console.WriteLine("Player::.ctor() armor size increased!");

                IL.MethodPrepend(itemCheck, new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_0),
                        Instruction.Create(OpCodes.Call, slsItemCheckPre)
                    });
                Console.WriteLine("Player::ItemCheck(int) pre Hook added!");

                IL.MethodPrepend(update, new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_0),
                        Instruction.Create(OpCodes.Call, slsUpdatePlayerPre)
                    });
                Console.WriteLine("Player::Update(int) pre Hook added!");
                
                IL.MethodAppend(update, update.Body.Instructions.Count - 1, 1, new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_0),
                        Instruction.Create(OpCodes.Call, slsUpdatePlayerPost),
                        Instruction.Create(OpCodes.Ret)
                    });
                Console.WriteLine("Player::Update(int) post Hook added!");

                IL.MethodAppend(updateBuffs, updateBuffs.Body.Instructions.Count - 1, 1, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Call, slsUpdateBuffsPost),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Player::UpdateBuffs(int) post Hook added!");

                IL.MethodPrepend(updateEquips, new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_0),
                        Instruction.Create(OpCodes.Ldarg, updateEquips.Parameters[0]),
                        Instruction.Create(OpCodes.Call, slsUpdateEquipsPre)
                    });
                Console.WriteLine("Player::UpdateEquips(int) pre Hook added!");

                IL.MethodAppend(updateEquips, updateEquips.Body.Instructions.Count - 1, 1, new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_0),
                        Instruction.Create(OpCodes.Call, slsUpdateEquipsPost),
                        Instruction.Create(OpCodes.Ret)
                    });
                Console.WriteLine("Player::UpdateEquips(int) post Hook added!");

                var hurtSpot = IL.ScanForOpcodePattern(hurtMe,
                    OpCodes.Ldarg_0,
                    OpCodes.Ldarg_0,
                    OpCodes.Ldfld,
                    OpCodes.Ldloc_S,
                    OpCodes.Conv_I4,
                    OpCodes.Sub,
                    OpCodes.Stfld);

                hurtMe.Body.Instructions.Insert(hurtSpot - 20, Instruction.Create(OpCodes.Ldarg_0));
                hurtMe.Body.Instructions.Insert(hurtSpot - 19, Instruction.Create(OpCodes.Ldloca_S, hurtMe.Body.Variables[4]));
                hurtMe.Body.Instructions.Insert(hurtSpot - 18, Instruction.Create(OpCodes.Call, slsHurtMeMid));
                Console.WriteLine("Player::HurtMe(PlayerDeathReason, int, bool, bool, bool, int) mid function Hook added!");

                IL.MethodAppend(hurtMe, hurtMe.Body.Instructions.Count - 4, 1, new[]
                    {
                        Instruction.Create(OpCodes.Ldarg_0),
                        Instruction.Create(OpCodes.Call, slsHurtMePost),
                        Instruction.Create(OpCodes.Ldloc_S, hurtMe.Body.Variables[4]),
                        Instruction.Create(OpCodes.Ret),
                    });
                Console.WriteLine("Player::HurtMe(PlayerDeathReason, int, bool, bool, int) post Hook added!");

                var grabItemsFirst = grabItems.Body.Instructions.First();

                IL.MethodPrepend(grabItems, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Call, slsGrabItemsPre),
                    Instruction.Create(OpCodes.Brfalse_S, grabItemsFirst),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Player::GrabItems(int) pre Hook added!");

                IL.MethodAppend(horizontalMovement, horizontalMovement.Body.Instructions.Count - 1, 1, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Call, slsHorizontalMovementPost),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Player::HorizontalMovementPost() post Hook added!");
                
                var itemVar = new VariableDefinition("TypeBuffer", IL.GetTypeDefinition(modDef, "Item"));
                getItem.Body.Variables.Add(itemVar);
                var getItemFirst = getItem.Body.Instructions.First();
                IL.MethodPrepend(getItem, new[]
                {
                   Instruction.Create(OpCodes.Ldarg_0),
                   Instruction.Create(OpCodes.Ldarg_2),
                   Instruction.Create(OpCodes.Ldloca_S, itemVar),
                   Instruction.Create(OpCodes.Ldarga_S, getItem.Parameters[3]),
                   Instruction.Create(OpCodes.Call, slsGetItemPre),
                   Instruction.Create(OpCodes.Brfalse_S, getItemFirst),
                   Instruction.Create(OpCodes.Ldloc, itemVar),
                   Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Player::GetItem(int, Item, bool, bool pre Hook added!");

                IL.MethodPrepend(pickAmmo, new[]
                {
                    Instruction.Create(OpCodes.Ldarga, pickAmmo.Parameters[6]),
                    Instruction.Create(OpCodes.Call, slsPickAmmoPre)
                });
                Console.WriteLine("Player::PickAmmo(ref bool) pre hook added!");
            }

            {
                var item = IL.GetTypeDefinition(modDef, "Item");
                var setDefaults = IL.GetMethodDefinition(item, "SetDefaults");
                var prefix = IL.GetMethodDefinition(item, "Prefix");

                var slsItemsSetDefaultPost = modDef.Import(IL.GetMethodDefinition(slsHooks, "ItemSetDefaultsPost"));
                var slsPrefixPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "PrefixPre"));

                IL.MethodAppend(setDefaults, setDefaults.Body.Instructions.Count - 1, 1, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Call, slsItemsSetDefaultPost),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Item::ItemSetDefaults(int, bool) post Hook addded!");

                IL.MethodPrepend(prefix, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Ldarga, prefix.Parameters[0]),
                    Instruction.Create(OpCodes.Call, slsPrefixPre)
                });
                Console.WriteLine("Item::Prefix(int) pre Hook added!");
            }

            {
                var projectile = IL.GetTypeDefinition(modDef, "Projectile");
                var ai001 = IL.GetMethodDefinition(projectile, "AI_001");
                var damage = IL.GetMethodDefinition(projectile, "Damage");

                var slsAI001Post = modDef.Import(IL.GetMethodDefinition(slsHooks, "Projectile_AI001"));
                var slsDamagePost = modDef.Import(IL.GetMethodDefinition(slsHooks, "Projectile_DamagePost"));

                IL.MethodAppend(ai001, ai001.Body.Instructions.Count - 1, 1, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Call, slsAI001Post),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Projectile::AI_001() post Hook added!");

                IL.MethodAppend(damage, damage.Body.Instructions.Count - 1, 1, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Call, slsDamagePost),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("Projectile::Damage() post Hook added!");
            }

            {
                var netMessage = IL.GetTypeDefinition(modDef, "NetMessage");
                var sendChatMessage = IL.GetMethodDefinition(netMessage, "SendChatMessageFromClient");

                var chatMessage = IL.GetTypeDefinition(modDef, "ChatMessage");
                var get_text = IL.GetMethodDefinition(chatMessage, "get_Text");

                var slsSendChatMessagePre = modDef.Import(IL.GetMethodDefinition(slsHooks, "SendChatMessageFromClientPre"));

                var first = sendChatMessage.Body.Instructions.First();

                IL.MethodPrepend(sendChatMessage, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Call, get_text),
                    Instruction.Create(OpCodes.Call, slsSendChatMessagePre),
                    Instruction.Create(OpCodes.Brfalse_S, first),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("NetMessage::SendChatMessageFromClient(ChatMessage) pre Hook added!");
            }

            {
                var chest = IL.GetTypeDefinition(modDef, "Chest");
                var unlock = IL.GetMethodDefinition(chest, "Unlock");

                var spot = IL.ScanForOpcodePattern(unlock, (i, instruction) =>
                    {
                        var f0 = instruction.Operand as FieldReference;
                        return f0.Name == "downedPlantBoss";
                    }, OpCodes.Ldsfld,
                    OpCodes.Brtrue_S,
                    OpCodes.Ldc_I4_0,
                    OpCodes.Ret);

                unlock.Body.Instructions[spot].OpCode = OpCodes.Nop;
                unlock.Body.Instructions[spot + 1].OpCode = OpCodes.Nop;
                unlock.Body.Instructions[spot + 2].OpCode = OpCodes.Nop;
                unlock.Body.Instructions[spot + 3].OpCode = OpCodes.Nop;

                Console.WriteLine("Chest::Unlock() Patched!");
            }

            {
                var itemSlot = IL.GetTypeDefinition(modDef, "ItemSlot");
                var rightClick = IL.GetMethodDefinition(itemSlot, "RightClick", 3);
                var mouseHover = IL.GetMethodDefinition(itemSlot, "MouseHover", 3);

                var slsRightClickItemPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "RightClickItemPre"));

                var first = rightClick.Body.Instructions.First();
                IL.MethodPrepend(rightClick, new[]
                {
                    Instruction.Create(OpCodes.Ldarg_0),
                    Instruction.Create(OpCodes.Ldarg_1),
                    Instruction.Create(OpCodes.Ldarg_2),
                    Instruction.Create(OpCodes.Call, slsRightClickItemPre),
                    Instruction.Create(OpCodes.Brfalse_S, first),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("ItemSlot::RightClick(Item[], int, int) pre Hook added!");

                var spot = IL.ScanForOpcodePattern(mouseHover,
                    OpCodes.Ldarg_1,
                    OpCodes.Ldc_I4_S,
                    OpCodes.Bne_Un_S,
                    OpCodes.Ldsfld,
                    OpCodes.Ldc_I4_1,
                    OpCodes.Stfld);

                mouseHover.Body.Instructions[spot + 4].OpCode = OpCodes.Ldc_I4_0;
                Console.WriteLine("ItemSlot::HoverItem(Item[], int, int) patched at {0}", spot);
            }

            { //Remove Steam depency
                var socialAPI = IL.GetTypeDefinition(modDef, "SocialAPI");
                var loadSteam = IL.GetMethodDefinition(socialAPI, "LoadSteam");

                var slsLoadSteamPre = modDef.Import(IL.GetMethodDefinition(slsHooks, "LoadSteamPre"));

                var first = loadSteam.Body.Instructions.First();
                IL.MethodPrepend(loadSteam, new[]
                {
                    Instruction.Create(OpCodes.Call, slsLoadSteamPre),
                    Instruction.Create(OpCodes.Brfalse_S, first),
                    Instruction.Create(OpCodes.Ret)
                });
                Console.WriteLine("SocialAPI::LoadSteam() pre Hook added!");

                /*loadSteam.Body.Instructions.Clear();
                loadSteam.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));

                Console.WriteLine("SocialAPI::LoadSteam() noped!");*/
            }

            asmDef.Write(fiCur.FullName);
            Console.WriteLine("{0}{0}Done! Press any key to exit.", Environment.NewLine);

            Console.ReadKey(true);
        }
        
        public static void ShowErrorMessage(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
