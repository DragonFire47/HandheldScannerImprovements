using PulsarModLoader;
using PulsarModLoader.Keybinds;

namespace HandheldScannerImprovements
{
    public class Mod : PulsarMod, IKeybind
    {
        public override string Version => "0.0.1";

        public override string Author => "Dragon";

        public override string Name => "Handheld Scanner Improvements";

        public override string LongDescription => "";

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
