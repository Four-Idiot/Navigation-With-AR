using UnityEngine;

public record Marker(
    int Id,
    MarkerType Type,
    string Name,
    string BranchName,
    Coords Coords,
    string Address,
    int OpenTime,
    int CloseTime,
    float PositionX = 0,
    float PositionY = 0
);

public enum MarkerType
{
    DOSENT,
    HOSPITAL,
    METRO,
    PARK,
    PARKING_AREA,
    TOILET,
}