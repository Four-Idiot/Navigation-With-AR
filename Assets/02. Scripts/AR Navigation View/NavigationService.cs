using System.Threading.Tasks;
using UnityEngine;

public class NavigationService
{
    private readonly INavigationRepository navigationRepository;
    private readonly GpsService gpsService;
    private Coords currentCoords;

    public NavigationService(INavigationRepository navigationRepository, GpsService gpsService)
    {
        this.navigationRepository = navigationRepository;
    }

    // public async Task<Texture2D> MapByCurrentLocation()
    // {
    //     var response = await navigationRepository
    //         .FindMapByCurrentLocation(
    //             new(
    //                 37.3591614, 127.1054221, 540,
    //                 1200, 17, null
    //                 ));
    //     Texture2D tex = new(1080, 2400);
    //     tex.LoadImage(response.BinaryImage);
    //     return tex;
    // }
    
    public async Task<Texture2D> MapByCurrentLocation()
    {
        currentCoords = await gpsService.FindCurrentCoordinates();
        var response = await navigationRepository
            .FindMapByCurrentLocation(
                new(
                    currentCoords.Latitude, currentCoords.Longitude, 540,
                    1200, 17, null
                    ));
        Texture2D tex = new(1080, 2400);
        tex.LoadImage(response.BinaryImage);
        return tex;
    }
}