using HarmonyLib;
using PulsarModLoader.Patches;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace HandheldScannerImprovements
{
    [HarmonyPatch(typeof(PLScanner), "Update")]
    internal class ScannerFireDisplay
    {
        public static bool CheckIfValid(PLFire inFire)
        {
            return inFire != null && inFire.MyShip != null && inFire.MyShip.MyTLI != null && PLNetworkManager.Instance.ViewedPawn != null && inFire.MyShip.MyTLI == PLNetworkManager.Instance.ViewedPawn.MyCurrentTLI;
        }

        static MethodInfo IDTMethod = AccessTools.Method(typeof(PLScanner), "IsDisplayingTransform");
        static FieldInfo AllTargetScannerInfos = AccessTools.Field(typeof(PLScanner), "m_AllTargetScannerInfos");
        static void PatchMethod()
        {
            if(!Global.HSIScannerFireDisplay.Value)
            {
                return;
            }
            foreach (PLShipInfoBase currentShip in PLEncounterManager.Instance.AllShips.Values)
            {
                if (currentShip is PLShipInfo)
                {
                    PLShipInfo ship = (PLShipInfo)currentShip;
                    foreach (PLFire CurrentFire in ship.AllFires.Values)
                    {
                        if (CurrentFire != null && CheckIfValid(CurrentFire) && !(bool)IDTMethod.Invoke(PLScanner.Instance, new object[] { CurrentFire.transform }))
                        {
                            PLScanner.TargetScannerInfo targetScannerInfo_Fire = (PLScanner.TargetScannerInfo)AccessTools.Constructor(typeof(PLScanner.TargetScannerInfo)).Invoke(new object[] { });
                            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(PLScanner.Instance.Target_Scanner_Prefab);
                            gameObject.transform.parent = PLScanner.Instance.RadarBG.transform;
                            gameObject.transform.localPosition = Vector3.zero;
                            gameObject.transform.localRotation = Quaternion.identity;
                            gameObject.transform.localScale = Vector3.one;
                            //targetScannerInfo_Fire.MyFire = CurrentFire;
                            targetScannerInfo_Fire.Texture = gameObject.GetComponent<UITexture>();
                            foreach (object obj in targetScannerInfo_Fire.Texture.transform)
                            {
                                UITexture component = ((Transform)obj).GetComponent<UITexture>();
                                if (component != null)
                                {
                                    targetScannerInfo_Fire.TextureBG = component;
                                    break;
                                }
                            }
                            targetScannerInfo_Fire.Transform = CurrentFire.transform;
                            targetScannerInfo_Fire.Color = NGUIText.ParseColor24(Global.firehex.Value, 0);
                            ((List<PLScanner.TargetScannerInfo>)AllTargetScannerInfos.GetValue(PLScanner.Instance)).Add(targetScannerInfo_Fire);
                        }
                    }
                }
            }
        }
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) //Patch After num local var is stored
        {
            List<CodeInstruction> TargetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Stloc_1),
            };
            List<CodeInstruction> InjectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ScannerFireDisplay), "PatchMethod")),
            };
            return HarmonyHelpers.PatchBySequence(instructions, TargetSequence, InjectedSequence, HarmonyHelpers.PatchMode.AFTER, HarmonyHelpers.CheckMode.NONNULL);
        }
    }
    /*class TargetScannerInfo_Fire : PLScanner.TargetScannerInfo
    {
        public static bool CheckIfValid(PLFire inFire)
        {
            return inFire != null && inFire.MyShip != null && inFire.MyShip.MyTLI != null && PLNetworkManager.Instance.ViewedPawn != null && inFire.MyShip.MyTLI == PLNetworkManager.Instance.ViewedPawn.MyCurrentTLI;
        }
        public override bool IsStillValid()
        {
            return CheckIfValid(MyFire);
        }
        public PLFire MyFire;

        public TargetScannerInfo_Fire()
        {
        }
    }*/
}
