using HarmonyLib;
using PulsarModLoader;
using System.Collections.Generic;
using System.Reflection.Emit;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace HandheldScannerImprovements
{

    //Correct scanner max zoom based on level.
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
            //Zoom      1,2,3,           1,2,3,4,5
            //vanilla = 1,2,3. Current = ???
            //Equations:
            //Vanilla.... Zoom = 5 - zoomLevel
            //Current.... Zoom = 1.8 - ((zoomLevel - 4) * .15)
            //Potential.. Zoom = 7 / zoomLevel *.7
            if (zoomLevel < 4f)
            {
                return 5f - zoomLevel;
            }
            else
            {
                return 1.8f - ((zoomLevel - 4f) * .15f);
            }
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {


            //Implement Scanner Zoom recalculations from patch method.
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

            instructions = PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);



            //Correct Scanner UI zoom to global value.
            targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_I4_3),
                new CodeInstruction(OpCodes.Stloc_1),
            };
            InjectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Global), "HSIUIZoomLevel")),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SaveValue<int>), "get_Value")),
                new CodeInstruction(OpCodes.Stloc_1),
            };
            
            return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.AFTER, CheckMode.NONNULL);
        }
    }
}