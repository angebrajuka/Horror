public static class Math
{
    public static float Remap(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        return newMin + (oldValue-oldMin)*(newMax-newMin)/(oldMax-oldMin);
    }
}