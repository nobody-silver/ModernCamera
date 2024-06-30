using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using ModernCamera.Hooks;
using BepInEx.Logging;

namespace ModernCamera;

[BepInProcess("VRising.exe")]
[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    private static Harmony Harmony;

    public static ManualLogSource Logger;

    public override void Load()
    {
        Logger = Log;
        Settings.Init();

        AddComponent<ModernCamera>();

        TopdownCameraSystem_Hook.CreateAndApply();

        Harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        Harmony.PatchAll();

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} is loaded!");
    }

    public override bool Unload()
    {
        Harmony.UnpatchSelf();

        TopdownCameraSystem_Hook.Dispose();

        return true;
    }
}
