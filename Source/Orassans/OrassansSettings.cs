using Verse;

namespace Orassan
{

    public enum LitterCurveMode
    {
        Orassan,
        Human
    }

    public class OrassansSettings : ModSettings
    {
        public LitterCurveMode curveMode = LitterCurveMode.Orassan;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref curveMode, "curveMode", LitterCurveMode.Orassan);
        }
    }
}
