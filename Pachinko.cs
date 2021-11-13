using HarmonyLib;

namespace PeglinTweaks.Pachinko
{
    [HarmonyPatch(typeof(PredictionManager), "Initialize")]
    class PredictionManagerPatch
    {
        public static void Prefix(ref int ____baseMaxIterations)
        {
           ____baseMaxIterations = Configuration.AimerLength;
        }

        public static void Postfix(ref int ____bounceCount)
        {
            ____bounceCount = Configuration.AimerBounces;
        }
    }

    [HarmonyPatch(typeof(PachinkoBall), "Start")]
    class PhysicsPatch
    {
        public static void Prefix(ref float ___GravityScale, ref float ___FireForce, ref float ___MaxSpeed)
        {
            ___GravityScale = Configuration.Gravity;
            ___FireForce = Configuration.FireForce;
            ___MaxSpeed = Configuration.MaxSpeed;
        }
    }
}
