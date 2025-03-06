using HarmonyLib;
using UnityEngine;

namespace ImprovedStamina.Patches
{
    [HarmonyPatch(typeof(PlayerController), "Update")]
    public static class PlayerControllerPatch
    {
        private static float timeSinceStoppedSprinting = 0f;
        private static float staminaRegenRate = 0f;

        public static void Postfix(PlayerController __instance)
        {
            if (__instance == null) return;

            if (__instance.sprinting)
            {
                timeSinceStoppedSprinting = 0f;
                staminaRegenRate = 0f;
            }
            else
            {
                timeSinceStoppedSprinting += Time.deltaTime;

                if (timeSinceStoppedSprinting >= ConfigManager.DelayBeforeRegen.Value)
                {
                    float timeElapsedSinceStart = timeSinceStoppedSprinting - ConfigManager.DelayBeforeRegen.Value;
                    staminaRegenRate = Mathf.Min(ConfigManager.MaxRegenRate.Value,
                        timeElapsedSinceStart / ConfigManager.RegenRampUpTime.Value * ConfigManager.MaxRegenRate.Value);
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