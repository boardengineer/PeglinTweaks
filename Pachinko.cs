using HarmonyLib;
using Relics;
using System.Collections.Generic;
using UnityEngine;

namespace PeglinTweaks.Pachinko
{
    public static class holder
    {
        public static int numBounces = 0;
        public static BattleController battleController = null;
        public static DamageCountDisplay damageDisplay = null;
    }

    [HarmonyPatch(typeof(PredictionManager), "Initialize")]
    class PredictionManagerPatch
    {
        public static void Prefix(ref int ____baseMaxIterations)
        {
            BepInEx.Logging.Logger.CreateLogSource("Test Log Source").LogInfo("Prediction Manager Init");

            ____baseMaxIterations = Configuration.AimerLength;
        }

        public static void Postfix(ref int ____bounceCount, RelicManager relicManager)
        {
            var bouncesBase = relicManager.RelicEffectActive(RelicEffect.LONGER_AIMER) ? 1 : 0;
            ____bounceCount = bouncesBase + Configuration.AimerBounces;
        }
    }

    [HarmonyPatch(typeof(PredictionManager), "Predict")]
    class PredictPatch
    {
        public static void Prefix()
        {
            holder.numBounces = 0;
        }

        public static void Postfix()
        {
            BepInEx.Logging.Logger.CreateLogSource("Test Log Source").LogInfo("Bounced from (changed) " + holder.numBounces);
        }
    }

    [HarmonyPatch(typeof(PredictionManager), "CreateBounceIndicator")]
    class BounceIndicatorPatch
    {
        public static bool Prefix(PredictionManager __instance, Vector3 collisionPoint, Vector3 collisionOrbPosition, ref ObjectPool ____bounceIndicatorObjectPool, ref float ____additionalBounceOffset, ref List<GameObject> ____bounceIndicatorList)
        {
            GameObject s = ____bounceIndicatorObjectPool.GetPooledObject();

            //__instance.GetComponent
            //Transform b = s.transform;
            holder.numBounces += 1;

            //FloatingText test = new FloatingText();
            //UnityEngine.Object.Instantiate<FloatingText>(collisionPoint);
            //test.Awake();
            DamageCountDisplay test = __instance.GetComponent<DamageCountDisplay>();
            //test.Awake();

            BattleController battleController = holder.battleController;

            if (holder.damageDisplay != null) {
                test = holder.damageDisplay.GetComponent<DamageCountDisplay>();
            }

            test.DisplayDamage(1, new Vector2(collisionPoint.x, collisionPoint.y));

            BepInEx.Logging.Logger.CreateLogSource("Test Log Source").LogInfo("We're still updating " + (test == null) + " stooff " + (holder.damageDisplay == null));
            //__insta

            if (s == null) {
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(PachinkoBall), "Start")]
    class PhysicsPatch
    {
        public static void Prefix(ref float ___GravityScale, ref float ___FireForce)
        {
            ___GravityScale = Configuration.Gravity;
            ___FireForce = Configuration.FireForce;
        }
    }

    [HarmonyPatch(typeof(PachinkoBall), "DoLateUpdate")]
    class PrePredictPatch
    {
        public static void Prefix()
        {
            BepInEx.Logging.Logger.CreateLogSource("Test Log Source").LogInfo("Pre Predction");
        }
    }

    [HarmonyPatch(typeof(PachinkoBall), "DoLateUpdate")]
    class CheckMultiballPatch
    {
        public static void Prefix(PachinkoBall __instance)
        {
            BepInEx.Logging.Logger.CreateLogSource("Test Log Source").LogInfo("We're Doing it " + __instance.multiballLevel);
        }
    }
}