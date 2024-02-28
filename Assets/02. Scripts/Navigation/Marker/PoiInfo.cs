﻿using UnityEngine;

public record PoiInfo(
    int Id,
    MarkerType Type,
    string Name,
    string BranchName,
    Coords Coords,
    string Address,
    int OpenTime,
    int CloseTime,
    float PositionX = 0,
    float PositionY = 0,
    Texture2D image = null
);

public enum MarkerType
{
    DOCENT,
    HOSPITAL,
    METRO,
    PARK,
    PARKING_AREA,
    TOILET,
    PHOTOZONE,
}