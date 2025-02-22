using BepInEx.Unity.IL2CPP.UnityEngine;
using HarmonyLib;
using ProjectM.UI;
using UnityEngine;

namespace ModernCamera.Patches;

[HarmonyPatch]
internal static class ActionWheelSystem_Patch
{
    internal static bool WheelVisible;

    [HarmonyPostfix]
    [HarmonyPatch(typeof(ActionWheelSystem), nameof(ActionWheelSystem.OnUpdate))]
    private static void OnUpdate(ActionWheelSystem __instance)
    {
        if (__instance == null)
        {
            return;
        }

        if (WheelVisible)
        {
            if (__instance._CurrentActiveWheel != null && !__instance._CurrentActiveWheel.IsVisible())
            {
                Plugin.Logger.LogInfo("No wheel visible");
                ModernCameraState.IsMenuOpen = false;
                WheelVisible = false;
            }
            else if (__instance._CurrentActiveWheel == null)
            {
                Plugin.Logger.LogInfo("Wheel is null");
                ModernCameraState.IsMenuOpen = false;
                WheelVisible = false;
            }
        }
        else if (__instance._CurrentActiveWheel != null && __instance._CurrentActiveWheel.IsVisible())
        {
            Plugin.Logger.LogInfo("CurrentActiveWheel is visible");
            WheelVisible = true;
            ModernCameraState.IsMenuOpen = true;
        }
    }
}
