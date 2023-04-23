using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;
using UnityEngine;


namespace HandheldScannerImprovements
{
    class Commands : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "scannercolors", "scannercolor", "sc" };
        }

        public override string Description()
        {
            return "Controls colors displayed on the scanner. Subcommands: values, reset, colorblind, hostiles, crew, fire, npc, teleporter, door, items, research";
        }

        

        public override void Execute(string arguments)
        {
            string[] args = arguments.Split(' ');
            bool validhex = false;
            if (args.Length > 1)
            {
                validhex = ColorUtility.TryParseHtmlString("#" + args[1], out _);
                if(!validhex && !args[1].StartsWith("#"))
                {
                    validhex = ColorUtility.TryParseHtmlString(args[1], out Color color);
                    if(validhex)
                    {
                        args[1] = ColorUtility.ToHtmlStringRGB(color);
                    }
                }
            }
            switch (args[0].ToLower())
            {
                case "hostiles":
                    if (validhex)
                    {
                        Global.hostilehex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"Hostile color set to {args[1]}");
                    }
                    break;
                case "crew":
                    if (validhex)
                    {
                        Global.crewhex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"Crew color set to {args[1]}");
                    }
                    break;
                case "fire":
                    if (validhex)
                    {
                        Global.firehex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"Fire color set to {args[1]}");
                    }
                    break;
                case "npc":
                    if (validhex)
                    {
                        Global.npchex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"NPC color set to {args[1]}");
                    }
                    break;
                case "teleporter":
                    if (validhex)
                    {
                        Global.teleporterhex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"Teleporter color set to {args[1]}");
                    }
                    break;
                case "door":
                    if (validhex)
                    {
                        Global.doorhex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"Door color set to {args[1]}");
                    }
                    break;
                case "items":
                    if (validhex)
                    {
                        Global.itemhex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"Item color set to {args[1]}");
                    }
                    break;
                case "research":
                    if (validhex)
                    {
                        Global.researchhex.Value = args[1];
                        Global.ClearScanner();
                        Messaging.Notification($"Research color set to {args[1]}");
                    }
                    break;
                case "colorblind":
                    validhex = true;
                    Global.hostilehex.Value = "DC267F";
                    Global.crewhex.Value = "FE6100";
                    Global.npchex.Value = "ffffff";
                    Global.doorhex.Value = "FEFE62";
                    Global.teleporterhex.Value = "CC79A7";
                    Global.itemhex.Value = "FFB000";
                    Global.researchhex.Value = "785EF0";
                    Global.ClearScanner();
                    Messaging.Notification("Colors set to colorblind friendly (hopefully) settings!");
                    break;
                case "reset":
                    validhex = true;
                    Global.hostilehex.Value = "ff0000";
                    Global.crewhex.Value = "00ff00";
                    Global.firehex.Value = "E26822";
                    Global.npchex.Value = "ffffff";
                    Global.doorhex.Value = "6666ff";
                    Global.teleporterhex.Value = "ff00ff";
                    Global.itemhex.Value = "FFFF00";
                    Global.researchhex.Value = "44FFFF";
                    Global.ClearScanner();
                    Messaging.Notification("All colors set to default!");
                    break;
                case "values":
                    validhex = true;
                    Messaging.Notification($"[{Global.hostilehex}](*) HOSTILE[-]\n[{Global.crewhex}](*) CREW[-]\n[{Global.npchex}](*) NPC[-]\n[{Global.doorhex}](*) DOOR[-]\n[{Global.teleporterhex}](*) TELEPORTER[-]\n[{Global.itemhex}](*) PICKUPS[-]\n[{Global.researchhex}](*) RESEARCH MATS[-]");
                    break;
                case "togglefire":
                    validhex = true;
                    Global.HSIScannerFireDisplay.Value = !Global.HSIScannerFireDisplay.Value;
                    Messaging.Notification("Fire display disabled!");
                    break;
                case "zoomlevel":
                    validhex = true;
                    if (int.TryParse(args[1], out int zoomlevel))
                    {
                        Global.HSIUIZoomLevel.Value = zoomlevel;
                        Messaging.Notification("UIZoom level set to " + zoomlevel);
                    }
                    else
                    {
                        Messaging.Notification("Input value could not be parsed. Please use a number.");
                    }
                    break;
                default:
                    validhex = true;
                    Messaging.Notification("Subcommand not found. Subcommands: values, reset, colorblind, hostiles, crew, npc, teleporter, door, items, research");
                    break;
            }
            if(!validhex)
            {
                Messaging.Notification($"Invalid hex code!");
            }
        }

        //public string UsageExample()
        //{
        //    return "/cuscanner [crew | hostiles | npc | teleporter | door | items | research | colorblind | values | reset]";
        //}
    }
}
/*    color blind canidate #1    (contrasting colors)              
 *                  Global.hostilehex = "D35FB7";
                     Global.crewhex = "FEFE62";
                     Global.npchex = "ffffff";
                     Global.doorhex = "80BCFF";
                     Global.teleporterhex = "FF126A";
                     Global.itemhex = "40B0A6";
                     Global.researchhex = "E1BE6A";
   colorblind canidate #2 (wong)
                   Global.hostilehex = "E69F00";
                    Global.crewhex = "D55E00";
                    Global.npchex = "ffffff";
                    Global.doorhex = "0072B2";
                    Global.teleporterhex = "CC79A7";
                    Global.itemhex = "F0E442";
                    Global.researchhex = "0072B2";
 */