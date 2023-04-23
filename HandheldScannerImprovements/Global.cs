using HarmonyLib;
using PulsarModLoader;
using System.Collections.Generic;
using UnityEngine;

namespace HandheldScannerImprovements
{
    class Global
    {
        public static SaveValue<bool> HSIShowScanner = new SaveValue<bool>("HSIShowScanner", true);
        public static SaveValue<bool> HSIScannerEquipReq = new SaveValue<bool>("HSIScannerEquipReq", false);
        public static SaveValue<bool> HSIScannerFireDisplay = new SaveValue<bool>("HSIScannerFire", true);
        public static SaveValue<int> HSIUIZoomLevel = new SaveValue<int>("HSIZoomLevel", 1);


        public static SaveValue<string> hostilehex = new SaveValue<string>("HostileHex", "ff0000");
        public static SaveValue<string> crewhex = new SaveValue<string>("CrewHex", "00ff00");
        public static SaveValue<string> npchex = new SaveValue<string>("NPCHex", "ffffff");
        public static SaveValue<string> doorhex = new SaveValue<string>("DoorHex", "6666ff");
        public static SaveValue<string> teleporterhex = new SaveValue<string>("TeleporterHex", "ff00ff");
        public static SaveValue<string> itemhex = new SaveValue<string>("ItemHex", "FFFF00");
        public static SaveValue<string> researchhex = new SaveValue<string>("ResearchHex", "44FFFF");
        public static SaveValue<string> firehex = new SaveValue<string>("FireHex", "E26822");

        public static string ScannerIndex() //changes all the color values for the key besides items and research
        {
            return $"[{hostilehex.Value}](*) HOSTILE[-]\n[{crewhex.Value}](*) CREW[-]\n[{firehex.Value}](*) FIRE[-]\n[{npchex.Value}](*) NPC[-]\n[{doorhex.Value}](*) DOOR[-]\n[{teleporterhex.Value}](*) TELEPORTER[-]";
        }
        public static string ItemKey()
        {
            return $"\n[{itemhex.Value}](*) PICKUPS[-]";
        }
        public static string ResearchKey()
        {
            return $"\n[{researchhex.Value}](*) RESEARCH MATS[-]";
        }
        public static Color hostilergb()
        {
            return NGUIText.ParseColor24(hostilehex.Value, 0);
        }
        public static Color crewrgb()
        {
            return NGUIText.ParseColor24(crewhex.Value, 0);
        }
        public static Color npcrgb()
        {
            return NGUIText.ParseColor24(npchex.Value, 0);
        }
        public static Color doorrgb()
        {
            return NGUIText.ParseColor24(doorhex.Value, 0);
        }
        public static Color teleporterrgb()
        {
            return NGUIText.ParseColor24(teleporterhex.Value, 0);
        }
        public static Color itemrgb()
        {
            return NGUIText.ParseColor24(itemhex.Value, 0);
        }
        public static Color researchrgb()
        {
            return NGUIText.ParseColor24(researchhex.Value, 0);
        }
        public static void ClearScanner()
        {
            List<PLScanner.TargetScannerInfo> AllScannerTargets = (List<PLScanner.TargetScannerInfo>)AccessTools.Field(typeof(PLScanner), "m_AllTargetScannerInfos").GetValue(PLScanner.Instance);
            foreach (PLScanner.TargetScannerInfo TSI in AllScannerTargets)
            {
                UnityEngine.Object.Destroy(TSI.Texture.gameObject);
            }
            AllScannerTargets.Clear();
        }
    }
}
