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
}
