using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.LocationServiceStatus;

public class AndroidGpsRepository : IGpsRepository
{
    private LocationService locationService;
    private Coords coords = new(126.744577f, 37.713834f);

    public AndroidGpsRepository(LocationService locationService)
    {
        this.locationService = locationService;
    }

    public async Task StartLocationService()
    {
        if (!locationService.isEnabledByUser)
            Debug.Log("LocationService is not enabled by user");

        locationService.Start();
        int maxWait = 5;
        while (locationService.status == Initializing && maxWait > 0)
        {
            await Task.Delay(1000);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("LocationService Start Sequence finish - Timed out");
            return;
        }

        if (locationService.status == Failed)
        {
            Debug.Log("LocationService Start Sequence finish - Failed");
        }

        if (locationService.status == Running)
        {
            Debug.Log("LocationService Start Sequence finish - Running");
        }
    }

    public void StopLocationService()
    {
        locationService.Stop();
    }

    public async Task<Coords> FindCoordsByCurrentLocationOrDefault()
    {
        Debug.Log($"status = {locationService.status}");
        if (locationService.status == Stopped)
            await StartLocationService();
        
        Coords findCoords = null;
        
        if (locationService.isEnabledByUser && locationService.status == Running)
        {
            var lastLocation = locationService.lastData;
            findCoords = new(lastLocation.longitude, lastLocation.longitude);
            coords = findCoords;
        }
        return await Task.FromResult(findCoords ?? coords);
    }

}