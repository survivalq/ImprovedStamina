using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace ImprovedStamina.Patches
{
    [HarmonyPatch(typeof(PlayerController), "LateStart")]
    public static class PlayerControllerLateStartPatch
    {
        public static void Postfix(PlayerController __instance)
        {
            if (__instance == null) return;

            __instance.StartCoroutine(AdjustSprintDrain(__instance));
        }

        private static IEnumerator AdjustSprintDrain(PlayerController player)
        {
            yield return new WaitForSeconds(0.1f);

            float originalDrain = player.EnergySprintDrain;
            float multiplier = ConfigManager.SprintDrainMultiplier.Value;

            player.EnergySprintDrain = originalDrain * multiplier;
        }
    }
}