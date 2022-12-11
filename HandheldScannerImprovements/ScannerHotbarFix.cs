using HarmonyLib;
using static PulsarModLoader.Patches.HarmonyHelpers;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HandheldScannerImprovements
{
    [HarmonyPatch(typeof(PLScanner), "Update")]
    internal class ScannerHotbarFix
    {
        static int ScannerReqForStatic()
        {
            if(Global.HSIScannerEquipReq.Value)
            {
                return -1;
            }
            else
            {
                return -2;
            }
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_I4_M1)
            };
            List<CodeInstruction> InjectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ScannerHotbarFix), "ScannerReqForStatic"))
            };

            return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.REPLACE);
        }
    }
}
