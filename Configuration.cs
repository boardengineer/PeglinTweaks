using BepInEx.Configuration;

namespace PeglinTweaks
{
    class Configuration
    {
        private static ConfigEntry<float> cookbookBombChanceCfg;

        public static void BindConfigs(ConfigFile config)
        {
            cookbookBombChanceCfg = config.Bind("Relics", "Cookbook_Bomb_Chance", 0.07F, "Alchemist's cookbook bomb conversion chance. Use a value between 0 and 1");
        }

        public static float CookbookBombChance
        {
            get { return cookbookBombChanceCfg.Value; }
        }
    }
}
