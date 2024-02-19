using UnityEngine;

public record PoiInfo(
    int Id,
    PoiType Type,
    string Name,
    string BranchName,
    Coords Coords,
    string Address,
    int OpenTime,
    int CloseTime,
    float PositionX = 0,
    float PositionY = 0
);

public enum PoiType
{
    CAFE,
    RESTAURANT,
    PUBLIC
}