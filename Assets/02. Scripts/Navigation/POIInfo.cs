public class POIInfo
{
    public readonly POIType type;
    public readonly string name;
    public readonly string branchName;
    public readonly Coords coords;
}

public enum POIType
{
    CAFE, RESTAURANT, PUBLIC
}