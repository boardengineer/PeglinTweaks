using Battle;
using Battle.Attacks;
using Battle.Enemies;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PeglinTweaks.Battle
{
    [HarmonyPatch(typeof(Attack), "Initialize")]
    class PlayerDmgMultiplierPatch
    {
        public static void Prefix(ref float ___DamagePerPeg, ref float ___CritDamagePerPeg)
        {
            Logger.CreateLogSource("Test Log Source").LogInfo("Attack Init");

            ___DamagePerPeg *= Configuration.PlayerDmgMultiplier;
            ___CritDamagePerPeg *= Configuration.PlayerDmgMultiplier;
        }
    }

    [HarmonyPatch(typeof(BattleController), "Awake")]
    class BombDmgMultiplierPatch
    {
        public static void Postfix(BattleController __instance, ref DamageCountDisplay ____damageCountDisplay)
        {
            __instance._baseBombDamage = Configuration.BombBaseDamage;
            Pachinko.holder.battleController = __instance;
            Pachinko.holder.damageDisplay = ____damageCountDisplay;
        }
    }

    [HarmonyPatch(typeof(PlayerHealthController), "Damage")]
    class EnemyDmgMultiplierPatch
    {
        public static void Prefix(ref float damage) => damage *= Configuration.EnemyDmgMultiplier;
    }

    [HarmonyPatch(typeof(GameInit), "Start")]
    class PlayerStartHealthPatch
    {
        public static void Prefix(FloatVariable ___maxPlayerHealth, FloatVariable ___playerHealth)
        {
            ___maxPlayerHealth._initialValue = Configuration.PlayerStartingHealth;
            ___playerHealth._initialValue = Configuration.PlayerStartingHealth;
        }
    }

    [HarmonyPatch(typeof(Enemy), "Initialize")]
    class EnemyHealthMultiplierPatch
    {
        public static void Prefix(ref float ___StartingHealth)
        {
            ___StartingHealth = (float)Math.Round(___StartingHealth * Configuration.EnemyHealthMultiplier);
        }
    }

    [HarmonyPatch(typeof(BattleController), "MaxDiscardedShots", MethodType.Getter)]
    class OrbDiscardCountPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var patched = false;
            foreach (var instruction in instructions)
            {
                if (!patched && instruction.opcode == OpCodes.Ldc_I4_1)
                {
                    patched = true;
                    yield return new CodeInstruction(OpCodes.Ldc_I4, (Configuration.OrbDiscardAmount));
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}