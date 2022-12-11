using HarmonyLib;
using System.Collections.Generic;
using static PulsarModLoader.Patches.HarmonyHelpers;
using System.Reflection.Emit;
using ProFlares;

namespace HandheldScannerImprovements
{
    [HarmonyPatch(typeof(PLPawnItem_Scanner), "OnActive")]
    internal class ScannerMaxZoomPatch
    {
        static void Postfix(PLPawnItem_Scanner __instance)
        {
            if (__instance.MyItemInstance != null)
            {
                __instance.MaxZoomLevel = __instance.Level + 3;
            }
        }
    }

    [HarmonyPatch(typeof(PLScanner), "Update")]
    internal class ScannerZoomPatch
    {
        static float PatchMethod(float zoomLevel)
        {
            //Zoom      1,2,3,        1,2,3,4.0,5.0
            //vanilla = 1,2,3. Goal = 3,2,1,0.8,0.6,etc
            if (zoomLevel < 3f)
            {
                return 5f - zoomLevel;
            }
            else
            {
                return 2f - ((zoomLevel - 4f) * .2f);
            }
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Conv_R4),
            };
            List<CodeInstruction> InjectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ScannerZoomPatch), "PatchMethod")),
            };

            instructions = PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);


            targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Add),
                new CodeInstruction(OpCodes.Conv_R4),
            };
            return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);
        }
    }
}