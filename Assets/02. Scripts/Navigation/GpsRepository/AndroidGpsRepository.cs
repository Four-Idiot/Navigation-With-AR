using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

public class AndroidGpsRepository : IGpsRepository
{
    private LocationService locationService;
    private Coords coords;

    public void StartLocationService(Action callback)
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            locationService.Start();
        else
            callback?.Invoke();
    }

    public async Task<Coords> FindCoordsByCurrentLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        else
        {
        }
        throw new NotImplementedException();
    }
}