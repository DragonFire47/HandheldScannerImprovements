using PulsarModLoader;
using PulsarModLoader.Keybinds;

namespace HandheldScannerImprovements
{
    public class Mod : PulsarMod, IKeybind
    {
        public override string Version => "1.0.0";

        public override string Author => "Dragon";

        public override string Name => "Handheld Scanner Improvements";

        public override string LongDescription => "Provides improvements to the handheld scanner.\n- Adds fire detection to the scanner\n- Scanner levels effect the scanner range\n- Scanner colors can be customized\n- Adds keybind for toggling the scanner UI\n- Scanner no longer needs to be equipped on the hotbar to appear in the UI";

        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }

        public void RegisterBinds(KeybindManager manager)
        {
            manager.NewBind("Toggle Scanner", "HSIToggleScanner", "Basics", "o");
        }
    }
}
