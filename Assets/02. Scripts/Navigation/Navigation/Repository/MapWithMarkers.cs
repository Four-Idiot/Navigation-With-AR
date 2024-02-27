using System.Collections.Generic;
using UnityEngine;

public record MapWithMarkers(Texture2D MapTexture, Coords Center, List<PoiInfo> Markers);