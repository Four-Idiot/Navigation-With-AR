using System.Collections.Generic;

/// <summary>
/// for static map api
/// </summary>
public record MapRequestDto(double Latitude, double Longitude, int Width, int Height, int Level, List<Coords> Markers);