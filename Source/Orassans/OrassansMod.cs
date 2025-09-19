using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace Orassans;

public class OrassansMod : Mod
{
	public OrassansMod(ModContentPack content)
		: base(content)
	{
        Harmony harmony = new Harmony("zal.orassan");
        Type typeFromHandle = typeof(Plant);
        PropertyInfo propertyInfo = AccessTools.Property(typeFromHandle, "GrowthRateFactor_Temperature");
        MethodInfo getMethod = propertyInfo.GetGetMethod();
        HarmonyMethod prefix = new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_Plant_GrowthRateFactor_Temperature_get));
        harmony.Patch(getMethod, prefix);       
        Type typeFromHandle2 = typeof(Zone_Growing);
        MethodInfo method = typeFromHandle2.GetMethod("GetInspectString");
        HarmonyMethod prefix2 = new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_Zone_Growing_GetInspectString));
        harmony.Patch(method, prefix2);
        Type typeFromHandle3 = typeof(PlantUtility);
        MethodInfo method2 = typeFromHandle3.GetMethod(
            "GrowthSeasonNow",
            new[] { typeof(IntVec3), typeof(Map), typeof(ThingDef) }
        );
        HarmonyMethod prefix3 = new HarmonyMethod(typeof(HarmonyPatches), nameof(HarmonyPatches.Patch_GrowthSeasonNow));
        harmony.Patch(method2, prefix3);


    }
}
