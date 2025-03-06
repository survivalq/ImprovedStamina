using BepInEx.Configuration;

namespace ImprovedStamina
{
    public static class ConfigManager
    {
        public static ConfigEntry<float> MaxRegenRate { get; private set; }
        public static ConfigEntry<float> RegenRampUpTime { get; private set; }
        public static ConfigEntry<float> DelayBeforeRegen { get; private set; }
        public static ConfigEntry<float> SprintDrainMultiplier { get; private set; }

        public static void Initialize(ConfigFile config)
        {
            MaxRegenRate = config.Bind("Stamina Settings", "MaxRegenRate", 8.0f,
                new ConfigDescription(
                    "Maximum stamina regeneration multiplier after not sprinting for some time.",
                    new AcceptableValueRange<float>(1f, 25f)
                ));

            RegenRampUpTime = config.Bind("Stamina Settings", "RegenRampUpTime", 3.0f,
                new ConfigDescription(
                    "Time in seconds before stamina regeneration reaches max multiplier.",
                    new AcceptableValueRange<float>(0f, 25f)
                ));

            DelayBeforeRegen = config.Bind("Stamina Settings", "DelayBeforeRegen", 0.5f,
                new ConfigDescription(
                    "Time in seconds before stamina starts regenerating after stopping sprinting.",
                    new AcceptableValueRange<float>(0f, 25f)
                ));

            SprintDrainMultiplier = config.Bind("Stamina Settings", "SprintDrainMultiplier", 1.0f,
                new ConfigDescription(
                    "Multiplier for stamina cost when sprinting. 1.0x is default.",
                    new AcceptableValueRange<float>(0f, 10f)
                ));
        }
    }
}