using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.Client.NoObf;

namespace kombat {
    [HarmonyPatch(typeof(ClientMain), "TryAttackEntity", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(EntitySelection) })]
    public static class TryAttackEntity {

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            MethodInfo toCheck = typeof(CollectibleObject).GetMethod("GetAttackRange");

            List<CodeInstruction> toReturn = new List<CodeInstruction>(instructions);
            for (int i = 0; i < toReturn.Count; i++) {
                if (toReturn[i].Is(OpCodes.Callvirt, toCheck)) {
                    toReturn[i - 2] = new CodeInstruction(OpCodes.Ldarg_0);
                    toReturn[i - 1] = CodeInstruction.Call(typeof(ClientMain), "get_EntityPlayer");
                    toReturn[i] = CodeInstruction.Call(typeof(Kombat), "GetDynamicAttackRange");
                    break;
                }
            }
            return toReturn;
        }
    }

    [HarmonyPatch(typeof(SystemRenderAim), "DrawAim", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(ClientMain) })]
    public static class DrawAim {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            MethodInfo toCheck = typeof(CollectibleObject).GetMethod("GetAttackRange");

            List<CodeInstruction> toReturn = new List<CodeInstruction>(instructions);
            for(int i = 0; i < toReturn.Count; i++) {
                if(toReturn[i].Is(OpCodes.Callvirt, toCheck)) {
                    toReturn[i - 2] = new CodeInstruction(OpCodes.Ldarg_1);
                    toReturn[i - 1] = CodeInstruction.Call(typeof(ClientMain), "get_EntityPlayer");
                    toReturn[i] = CodeInstruction.Call(typeof(Kombat), "GetDynamicAttackRange");
                    break;
                }
            }
            return toReturn;
        }
    }

    [HarmonyPatch(typeof(EntityAgent), "OnInteract", MethodType.Normal)]
    public static class OnInteract {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator) {
            MethodInfo toCheck = typeof(CollectibleObject).GetMethod("GetAttackPower");
            MethodInfo toCheck2 = typeof(EntityAgent).GetMethod("DidAttack");

            List<CodeInstruction> toReturn = new List<CodeInstruction>(instructions);
            for(int i = 0; i < toReturn.Count; i++) {
                if(toReturn[i].Is(OpCodes.Callvirt, toCheck)) {
                    toReturn[i - 5] = new CodeInstruction(OpCodes.Ldarg_1);
                    toReturn[i - 4] = new CodeInstruction(OpCodes.Ldarg_2);
                    toReturn[i - 3] = new CodeInstruction(OpCodes.Ldarg_3);
                    toReturn[i - 2] = new CodeInstruction(OpCodes.Ldarg_0);
                    toReturn[i - 1] = CodeInstruction.Call(typeof(Kombat), "GetDynamicAttackPower");
                    toReturn[i] = new CodeInstruction(OpCodes.Nop);
                } else if(toReturn[i].Is(OpCodes.Callvirt, toCheck2)) {
                    toReturn[i - 3] = new CodeInstruction(OpCodes.Ldarg_2);
                    toReturn[i] = CodeInstruction.Call(typeof(Kombat), "DidDamageExtended");
                }
            }
            return toReturn;
        }
    }

    [HarmonyPatch(typeof(Entity), "ReceiveDamage", MethodType.Normal)]
    public static class ReceiveDamage {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            FieldInfo toCheck = typeof(EntityProperties).GetField("KnockbackResistance");
            List<CodeInstruction> toReturn = new List<CodeInstruction>(instructions);
            for(int i = 0; i < toReturn.Count; i++) {
                //knockback system
                if(toReturn[i].Is(OpCodes.Ldfld, toCheck)) {
                    toReturn.RemoveRange(i-14, 42);
                }
            }
            return toReturn;
        }
    }
}
