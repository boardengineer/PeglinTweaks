using Battle;
using BepInEx.Configuration;
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

    [HarmonyPatch(typeof(PlayerHealthController), "Update")]
    class LowHealthVignettePatch
    {
        public static bool Prefix()
        {
            // This is a lazy hack.
            // Currently, the Update method in PHC only handles the vignette logic and nothing else.
            // So we can get away with this, but really this should be a more fine-grained patch.
            return Configuration.EnableLowHealthVignette;
        }
    }
}