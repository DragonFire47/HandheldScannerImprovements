using PulsarModLoader;
using UnityEngine;

namespace HandheldScannerImprovements
{
    class Global
    {
        public static SaveValue<bool> HSIShowScanner = new SaveValue<bool>("HSIShowScanner", true);
        public static SaveValue<bool> HSIScannerEquipReq = new SaveValue<bool>("HSIScannerEquipReq", false);


        public static SaveValue<string> hostilehex = new SaveValue<string>("HostileHex", "ff0000");
        public static SaveValue<string> crewhex = new SaveValue<string>("CrewHex", "00ff00");
        public static SaveValue<string> npchex = new SaveValue<string>("NPCHex", "ffffff");
        public static SaveValue<string> doorhex = new SaveValue<string>("DoorHex", "6666ff");
        public static SaveValue<string> teleporterhex = new SaveValue<string>("TeleporterHex", "ff00ff");
        public static SaveValue<string> itemhex = new SaveValue<string>("ItemHex", "FFFF00");
        public static SaveValue<string> researchhex = new SaveValue<string>("ResearchHex", "44FFFF");

        public static string ScannerIndex() //changes all the color values for the key besides items and research
        {
            return $"[{hostilehex}](*) HOSTILE[-]\n[{crewhex}](*) CREW[-]\n[{npchex}](*) NPC[-]\n[{doorhex}](*) DOOR[-]\n[{teleporterhex}](*) TELEPORTER[-]";
        }
        public static string ItemKey()
        {
            return $"\n[{itemhex}](*) PICKUPS[-]";
        }
        public static string ResearchKey()
        {
            return $"\n[{researchhex}](*) RESEARCH MATS[-]";
        }
        public static Color hostilergb()
        {
            return NGUIText.ParseColor24(hostilehex, 0);
        }
        public static Color crewrgb()
        {
            return NGUIText.ParseColor24(crewhex, 0);
        }
        public static Color npcrgb()
        {
            return NGUIText.ParseColor24(npchex, 0);
        }
        public static Color doorrgb()
        {
            return NGUIText.ParseColor24(doorhex, 0);
        }
        public static Color teleporterrgb()
        {
            return NGUIText.ParseColor24(teleporterhex, 0);
        }
        public static Color itemrgb()
        {
            return NGUIText.ParseColor24(itemhex, 0);
        }
        public static Color researchrgb()
        {
            return NGUIText.ParseColor24(researchhex, 0);
        }
    }
}
