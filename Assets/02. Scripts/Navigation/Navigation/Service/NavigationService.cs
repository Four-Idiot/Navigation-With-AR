using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NavigationService
{
    private readonly INavigationRepository navigationRepository;
    private readonly GpsService gpsService;
    private readonly PoiService poiService;
    public NavigationService(INavigationRepository navigationRepository, GpsService gpsService, PoiService poiService)
    {
        this.navigationRepository = navigationRepository;
        this.gpsService = gpsService;
        this.poiService = poiService;
    }

    public async Task<Map> FindMapByCurrentLocation(int zoomLevel)
    {
        var findCoordsTask = gpsService.FindCurrentCoordinates();
        var poiInfosTask = poiService.FindPoiInfo();
        await Task.WhenAll(findCoordsTask, poiInfosTask);

        var currentCoords = findCoordsTask.Result;
        var poiInfos = poiInfosTask.Result;
        List<Coords> coords = new();
        List<PoiInfo> calculatedPoiInfo = new();
        foreach (var poiInfo in poiInfos)
        {
            coords.Add(poiInfo.Coords);
            Vector2 direction = new Vector2(
                poiInfo.Coords.Longitude - currentCoords.Longitude,
                poiInfo.Coords.Latitude - currentCoords.Latitude
            ).normalized;
            calculatedPoiInfo.Add(poiInfo with { Direction = direction });
        }

        var response = await navigationRepository
            .FindMapByCurrentLocation(
                new(
                    currentCoords.Latitude, currentCoords.Longitude, 540,
                    1200, zoomLevel, coords
                ));
        Texture2D tex = new(1080, 2400);
        tex.LoadImage(response.BinaryImage);
        return new Map(tex, currentCoords, calculatedPoiInfo);
    }
}