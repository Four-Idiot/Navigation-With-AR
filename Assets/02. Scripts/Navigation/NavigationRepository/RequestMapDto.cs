using System.Collections.Generic;

/// <summary>
/// for static map api
/// </summary>
public record RequestMapDto(double Latitude, double Longitude, uint Width, uint Height, uint Level, List<Coords> markers);