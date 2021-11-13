using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

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

            harmony.PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }

    [HarmonyPatch(typeof(Battle.PegManager), "ResetPeg")]
    public class CookbookChancePatch
    {
        private static readonly FieldInfo chanceField = AccessTools.DeclaredField(typeof(Relics.RelicManager), "PEG_TO_BOMB_CHANCE");

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.LoadsField(chanceField))
                {
                    //There's an object we don't need on the stack, so pop it
                    yield return new CodeInstruction(OpCodes.Pop);
                    //Push a float32 onto the stack, using the value from our config
                    yield return new CodeInstruction(OpCodes.Ldc_R4, Configuration.CookbookBombChance);
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
