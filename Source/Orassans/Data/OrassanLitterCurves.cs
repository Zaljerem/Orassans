using Verse;

namespace Orassan
{
    public static class OrassanLitterCurves
    {
        public static readonly SimpleCurve OrassanDefault = new SimpleCurve
    {
        new CurvePoint(0.5f, 0f),
        new CurvePoint(1f, 0.30f),
        new CurvePoint(2f, 0.40f),
        new CurvePoint(3f, 0.10f),
        new CurvePoint(4f, 0.20f),
        new CurvePoint(5f, 0f)
    };

        public static readonly SimpleCurve HumanDefault = new SimpleCurve
    {
        new CurvePoint(0.5f, 0f),
        new CurvePoint(1f, 1f),
        new CurvePoint(1.01f, 0.02f),
        new CurvePoint(3.5f, 0f)
    };
    }
}
