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
    Vector2 Direction = new()
);

public enum PoiType
{
    CAFE,
    RESTAURANT,
    PUBLIC
}