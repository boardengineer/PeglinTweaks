using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace PeglinTweaks
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Peglin.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);

        private void Awake()
        {
            Configuration.BindConfigs(Config);

            if (Configuration.EnableOrbsCollision)
            {
                Physics2D.IgnoreLayerCollision(13, 13, false);
            }
            
            harmony.PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
