using Battle;
using Battle.Enemies;
using HarmonyLib;

namespace PeglinTweaks.Battle
{
    [HarmonyPatch(typeof(Enemy), "Damage")]
    class PlayerDmgMultiplierPatch
    {
        public static void Prefix(ref float damage) => damage *= Configuration.PlayerDmgMultiplier;
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
            ___StartingHealth *= Configuration.EnemyHealthMultiplier;
        }
    }
}