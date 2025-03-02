namespace ImprovedStamina
{
    using BepInEx;
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
        private const float maxRegenRate = 8f;
        private const float regenRampUpTime = 3f;
        private const float delayBeforeRegen = 0.5f;

        void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

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

                        if (timeSinceStoppedSprinting >= delayBeforeRegen)
                        {
                            float timeElapsedSinceStart = timeSinceStoppedSprinting - delayBeforeRegen;
                            staminaRegenRate = Mathf.Min(maxRegenRate, (timeElapsedSinceStart / regenRampUpTime) * maxRegenRate);
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