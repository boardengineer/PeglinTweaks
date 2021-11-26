using Battle;
using HarmonyLib;
using Map.MinimapView;

namespace PeglinTweaks.Animation
{
    [HarmonyPatch(typeof(ScreenShaker), "Awake")]
    class ScreenShakerPatch
    {
        public static void Postfix(ScreenShaker __instance)
        {
            __instance.blocked = !Configuration.EnableScreenShake;
        }
    }

    [HarmonyPatch(typeof(MapPreviewCameraMovement), "CloseLerpComplete")]
    class MinimapShakePatch
    {
        public static void Postfix(ScreenShaker ____screenShaker)
        {
            ____screenShaker.blocked = !Configuration.EnableScreenShake;
        }
    }
}