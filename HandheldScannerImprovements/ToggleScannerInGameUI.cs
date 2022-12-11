using HarmonyLib;
using PulsarModLoader.Keybinds;
using System.Collections.Generic;
using System.Reflection.Emit;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace HandheldScannerImprovements
{
    [HarmonyPatch(typeof(PLInGameUI), "Update")]
    internal class ToggleScannerInGameUI
    {
        static bool PatchMethod(bool DisplayScanner)
        {
            return DisplayScanner & Global.HSIShowScanner.Value;
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PLGlobal), "SafeGameObjectSetActive"))
            };

            List<CodeInstruction> InjectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ToggleScannerInGameUI), "PatchMethod")),
                new CodeInstruction(OpCodes.Stloc_0),
            };

            return PatchBySequence(instructions, targetSequence, InjectedSequence, PatchMode.BEFORE, CheckMode.NONNULL);
        }
        
        static void Postfix()
        {
            if(KeybindManager.Instance.GetButtonDown("HSIToggleScanner"))
            {
                Global.HSIShowScanner.Value = !Global.HSIShowScanner.Value;
            }
        }
    }
}
