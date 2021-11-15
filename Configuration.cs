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

        private static ConfigEntry<float> gravityCfg;
        private static ConfigEntry<float> fireForceCfg;
        private static ConfigEntry<float> maxSpeedCfg;

        //Battle
        private static ConfigEntry<float> playerDmgMultiplierCfg;

        public static void BindConfigs(ConfigFile config)
        {
            //Relics
            cookbookBombChanceCfg = config.Bind("Relics", "Cookbook_Bomb_Chance", 0.07F, "Alchemist's cookbook bomb conversion chance. Use a value between 0 and 1");

            //Pachinko
            aimerLengthCfg = config.Bind("Pachinko", "Aimer_Length", 35, "The length of the aiming reticle when firing");
            aimerBouncesCfg = config.Bind("Pachinko", "Aimer_Bounces", 1, "The amount of bounces that the aiming reticle shows");

            gravityCfg = config.Bind("Pachinko", "Gravity", 1.2f, "The strength of gravity on orbs");
            fireForceCfg = config.Bind("Pachinko", "Fire_Force", 400f, "The strength with which orbs are fired");
            maxSpeedCfg = config.Bind("Pachinko", "Max_Speed", 55f, "The maximum speed of an orb");

            //Battle
            playerDmgMultiplierCfg = config.Bind("Battle", "Player_Dmg_Multiplier", 1f,
                "Multiplier for all damage dealt by the player");
        }

        //Relics
        public static float CookbookBombChance => cookbookBombChanceCfg.Value;

        //Pachinko
        public static int AimerLength => aimerLengthCfg.Value;
        public static int AimerBounces => aimerBouncesCfg.Value;

        public static float Gravity => gravityCfg.Value;
        public static float FireForce => fireForceCfg.Value;
        public static float MaxSpeed => maxSpeedCfg.Value;
        
        //Battle
        public static float PlayerDmgMultiplier => playerDmgMultiplierCfg.Value;
    }
}
