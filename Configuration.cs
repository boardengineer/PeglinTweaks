using BepInEx.Configuration;

namespace PeglinTweaks
{
    class Configuration
    {
        //Relics
        private static ConfigEntry<float> cookbookBombChanceCfg;

        //Pachinko
        private static ConfigEntry<int> aimerLengthCfg;
        private static ConfigEntry<int> aimerBouncesCfg;

        public static void BindConfigs(ConfigFile config)
        {
            //Relics
            cookbookBombChanceCfg = config.Bind("Relics", "Cookbook_Bomb_Chance", 0.07F, "Alchemist's cookbook bomb conversion chance. Use a value between 0 and 1");

            //Pachinko
            aimerLengthCfg = config.Bind("Pachinko", "Aimer_Length", 35, "The length of the aiming reticle when firing");
            aimerBouncesCfg = config.Bind("Pachinko", "Aimer_Bounces", 1, "The amount of bounces that the aiming reticle shows");
        }

        //Relics
        public static float CookbookBombChance => cookbookBombChanceCfg.Value;

        //Pachinko
        public static int AimerLength => aimerLengthCfg.Value;
        public static int AimerBounces => aimerBouncesCfg.Value;
    }
}
