using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Orassan
{

    public class OrassansMod : Mod
    {
        public static OrassansSettings Settings;

        public OrassansMod(ModContentPack content)
            : base(content)
        {

            Settings = GetSettings<OrassansSettings>();

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

            LongEventHandler.ExecuteWhenFinished(ApplyLitterCurve);


        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label("OrassansLitterSizeCurve".Translate());
            listing.Label("OrassansAffectsBoth".Translate());
            listing.Label("OrassansMustRestart".Translate());

            if (listing.RadioButton("OrassansOrassanDefault".Translate(), Settings.curveMode == LitterCurveMode.Orassan))
                Settings.curveMode = LitterCurveMode.Orassan;

            if (listing.RadioButton("OrassansHumanDefault".Translate(), Settings.curveMode == LitterCurveMode.Human))
                Settings.curveMode = LitterCurveMode.Human;

            listing.End();
        }


        static void ApplyLitterCurve()
        {
            ApplyCurveToRace("Alien_Orassan");
            ApplyCurveToRace("Alien_OrassanHumanHybrid");
        }

        static void ApplyCurveToRace(string defName)
        {
            var raceDef = DefDatabase<ThingDef>.GetNamedSilentFail(defName);
            if (raceDef?.race == null)
                return;

            raceDef.race.litterSizeCurve =
                OrassansMod.Settings.curveMode == LitterCurveMode.Human
                    ? OrassanLitterCurves.HumanDefault
                    : OrassanLitterCurves.OrassanDefault;
        }

        public override string SettingsCategory()
        {
            return "OrassansModName".Translate();
        }


    }
}
