namespace ImprovedStamina
{
    using BepInEx;
    using BepInEx.Configuration;
    using BepInEx.Logging;
    using HarmonyLib;
    using UnityEngine;

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private static Harmony _harmony;

        private static float timeSinceStoppedSprinting = 0f;
        private static float staminaRegenRate = 0f;

        private static ConfigEntry<float> maxRegenRate;
        private static ConfigEntry<float> regenRampUpTime;
        private static ConfigEntry<float> delayBeforeRegen;

        void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            maxRegenRate = Config.Bind("Stamina Settings", "MaxRegenRate", 8f,
                "Maximum stamina regeneration multiplier after not sprinting for some time.");

            regenRampUpTime = Config.Bind("Stamina Settings", "RegenRampUpTime", 3f,
                "Time in seconds before stamina regeneration reaches max multiplier.");

            delayBeforeRegen = Config.Bind("Stamina Settings", "DelayBeforeRegen", 0.5f,
                "Time in seconds before stamina starts regenerating after stopping sprinting.");

            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            _harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PlayerController), "Update")]
        public static class PlayerControllerPatch
        {
            public static void Postfix(PlayerController __instance)
            {
                if (__instance)
                {
                    if (__instance.sprinting)
                    {
                        timeSinceStoppedSprinting = 0f;
                        staminaRegenRate = 0f;
                    }
                    else
                    {
                        timeSinceStoppedSprinting += Time.deltaTime;

                        if (timeSinceStoppedSprinting >= delayBeforeRegen.Value)
                        {
                            float timeElapsedSinceStart = timeSinceStoppedSprinting - delayBeforeRegen.Value;
                            staminaRegenRate = Mathf.Min(maxRegenRate.Value, (timeElapsedSinceStart / regenRampUpTime.Value) * maxRegenRate.Value);
                        }
                    }

                    if (staminaRegenRate > 0f && __instance.EnergyCurrent < __instance.EnergyStart)
                    {
                        __instance.EnergyCurrent += staminaRegenRate * Time.deltaTime;

                        if (__instance.EnergyCurrent > __instance.EnergyStart)
                        {
                            __instance.EnergyCurrent = __instance.EnergyStart;
                        }
                    }
                }
            }
        }
    }
}