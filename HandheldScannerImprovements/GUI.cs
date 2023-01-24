using PulsarModLoader;
using PulsarModLoader.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GUILayout;

namespace HandheldScannerImprovements
{
    internal class GUI : PulsarModLoader.CustomGUI.ModSettingsMenu
    {
        string cachedHostilehex;
        string cachedCrewhex;
        string cachedNPChex;
        string cachedDoorhex;
        string cachedTeleporterhex;
        string cachedItemhex;
        string cachedResearchhex;
        string cachedFirehex;
        string errorMessage;

        bool CheckHex(string hex)
        {
            return ColorUtility.TryParseHtmlString("#" + hex, out _);
        }

        public override void Draw()
        {
            if(Button($"Fire Display on Scanner: {(Global.HSIScannerFireDisplay.Value ? "Enabled" : "Disabled")}"))
            {
                Global.HSIScannerFireDisplay.Value = !Global.HSIScannerFireDisplay.Value;
            }
            BeginHorizontal();
            Label("HostileHex");
            cachedHostilehex = TextField(cachedHostilehex);
            EndHorizontal();

            BeginHorizontal();
            Label("CrewHex");
            cachedCrewhex = TextField(cachedCrewhex);
            EndHorizontal();

            BeginHorizontal();
            Label("FireHex");
            cachedFirehex = TextField(cachedFirehex);
            EndHorizontal();

            BeginHorizontal();
            Label("NPCHex");
            cachedNPChex = TextField(cachedNPChex);
            EndHorizontal();

            BeginHorizontal();
            Label("DoorHex");
            cachedDoorhex = TextField(cachedDoorhex);
            EndHorizontal();

            BeginHorizontal();
            Label("TeleporterHex");
            cachedTeleporterhex = TextField(cachedTeleporterhex);
            EndHorizontal();

            BeginHorizontal();
            Label("ItemHex");
            cachedItemhex = TextField(cachedItemhex);
            EndHorizontal();

            BeginHorizontal();
            Label("ResearchHex");
            cachedResearchhex = TextField(cachedResearchhex);
            EndHorizontal();

            if(errorMessage != null)
            {
                Label(errorMessage);
            }
            if(Button("Apply Colors"))
            {
                if (CheckHex(cachedHostilehex) && cachedHostilehex.Length == 6 && CheckHex(cachedCrewhex) && cachedCrewhex.Length == 6 && CheckHex(cachedFirehex) && cachedFirehex.Length == 6 && CheckHex(cachedNPChex) && cachedNPChex.Length == 6 && CheckHex(cachedDoorhex) && cachedDoorhex.Length == 6 && CheckHex(cachedTeleporterhex) && cachedTeleporterhex.Length == 6 && CheckHex(cachedItemhex) && cachedItemhex.Length == 6 && CheckHex(cachedResearchhex) && cachedResearchhex.Length == 6)
                {
                    Global.hostilehex.Value = cachedHostilehex;
                    Global.crewhex.Value = cachedCrewhex;
                    Global.npchex.Value = cachedNPChex;
                    Global.doorhex.Value = cachedDoorhex;
                    Global.teleporterhex.Value = cachedTeleporterhex;
                    Global.itemhex.Value = cachedItemhex;
                    Global.researchhex.Value = cachedResearchhex;
                    Global.firehex.Value = cachedFirehex;

                    errorMessage = null;
                    Messaging.Notification("<color=green>Success!</color>");
                    Global.ClearScanner();
                }
                else
                {
                    errorMessage = "Failed to Parse Values! Ensure all text fields contain 6 hexadecimal digits!";
                }
            }
            if (Button("Reset Colors"))
            {
                Global.hostilehex.Value = "ff0000";
                Global.crewhex.Value = "00ff00";
                Global.firehex.Value = "E26822";
                Global.npchex.Value = "ffffff";
                Global.doorhex.Value = "6666ff";
                Global.teleporterhex.Value = "ff00ff";
                Global.itemhex.Value = "FFFF00";
                Global.researchhex.Value = "44FFFF";

                Messaging.Notification("<color=green>Success!</color>");
                Global.ClearScanner();
            }
        }

        public override string Name()
        {
            return "Handheld Scanner Improvements";
        }

        public override void OnOpen()
        {
            cachedHostilehex = Global.hostilehex;
            cachedCrewhex = Global.crewhex;
            cachedNPChex = Global.npchex;
            cachedDoorhex = Global.doorhex;
            cachedTeleporterhex = Global.teleporterhex;
            cachedItemhex = Global.itemhex;
            cachedResearchhex = Global.researchhex;
            cachedFirehex = Global.firehex;
        }
    }
}
