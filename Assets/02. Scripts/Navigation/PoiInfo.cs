public record PoiInfo(
    uint Id,
    PoiType Type,
    string Name,
    // 지점명
    string BranchName,
    Coords Coords
);

public enum PoiType
{
    CAFE,
    RESTAURANT,
    PUBLIC
}