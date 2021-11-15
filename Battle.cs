﻿using Battle.Enemies;
using HarmonyLib;

namespace PeglinTweaks.Battle
{
    [HarmonyPatch(typeof(Enemy), "Damage")]
    class PlayerDmgMultiplierPatch
    {
        public static void Prefix(ref float damage) => damage *= Configuration.PlayerDmgMultiplier;
    }
}