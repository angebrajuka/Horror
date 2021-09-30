public static class Math
{
    public static readonly Vector3Int N     = new Vector3Int( 0,  0,  1);
    public static readonly Vector3Int E     = new Vector3Int( 1,  0,  0);
    public static readonly Vector3Int S     = new Vector3Int( 0,  0, -1);
    public static readonly Vector3Int W     = new Vector3Int(-1,  0,  0);
    public static readonly Vector3Int NE    = new Vector3Int( 1,  0,  1);
    public static readonly Vector3Int SE    = new Vector3Int(-1,  0,  1);
    public static readonly Vector3Int NW    = new Vector3Int( 1,  0, -1);
    public static readonly Vector3Int SW    = new Vector3Int(-1,  0, -1);

    public static float Remap(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        return newMin + (oldValue-oldMin)*(newMax-newMin)/(oldMax-oldMin);
    }
}