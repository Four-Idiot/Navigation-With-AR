using System.Collections.Generic;
using UnityEngine;

public record Map(Texture2D MapTexture, Coords Center, List<Marker> Markers);