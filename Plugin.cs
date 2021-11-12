using BepInEx;
using BepInEx.Configuration;
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

        private ConfigEntry<float> cookbookBombChanceCfg;

        public static float cookbookBombChance;

        private void Awake()
        {
            cookbookBombChanceCfg = Config.Bind("Relics", "Cookbook_Bomb_Chance", 0.07F, "Alchemist's cookbook bomb conversion chance. Use a value between 0 and 1");

            cookbookBombChance = cookbookBombChanceCfg.Value;

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
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldc_R4, Plugin.cookbookBombChance);
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
