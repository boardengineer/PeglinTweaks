using HarmonyLib;
using UnityEngine;
using Worldmap;

namespace PeglinTweaks.Misc
{
    [HarmonyPatch]
    class IntroMapSkipPatch
    {
        private static bool isIntroFade = false;
        private static bool grabbedRootNode = false;
        private static MapNode rootNode = null;

        [HarmonyPatch(typeof(MapController), "Start")]
        [HarmonyPrefix]
        public static void StartPrefix(MapController __instance)
        {
            if (__instance._loadMapData.NewGame)
            {
                isIntroFade = true;
                rootNode = __instance.rootNode;
                grabbedRootNode = true;
                Debug.Log("Entering intro fade");
            }
        }

        [HarmonyPatch(typeof(MapController), "StartGoblinWalk")]
        [HarmonyPostfix]
        public static void StartPostfix()
        {
            isIntroFade = false;
            grabbedRootNode = false;
            rootNode = null;
            Debug.Log("Exited intro fade");
        }

        [HarmonyPatch(typeof(PauseMenu), "Update")]
        [HarmonyPrefix]
        public static void UpdatePrefix(PauseMenu __instance)
        {
            if (isIntroFade && Input.anyKeyDown)
            {
                Debug.Log("grabbedRootNode: " + grabbedRootNode);
                Debug.Log(rootNode);
                //Reflection hacks because my IDE won't let me do it the sane way
                typeof(MapNode).GetMethod("ActivateNode").Invoke(rootNode, null);
                Debug.Log("Got keypress during intro fade");
            }
        }


    }
}
