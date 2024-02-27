using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using UnityEngine;

public class NetworkNavigationRepository : INavigationRepository
{
    private readonly string clientId;
    private readonly string clientSecret;
    private readonly string staticMapBaseUrl;

    private RestClient staticMapClient;
    private readonly CancellationTokenSource tokenSource = new();
    private Coords currentCoords;

    public NetworkNavigationRepository(string clientId, string clientSecret, string staticMapBaseUrl)
    {
        this.clientId = clientId;
        this.clientSecret = clientSecret;
        this.staticMapBaseUrl = staticMapBaseUrl;
        InitStaticMapClient();
    }

    public async Task<MapResponseDto> FindMapByCurrentLocation(MapRequestDto mapRequestDto)
    {
        RestRequest request = new RestRequest();
        request
            .AddParameter("w", mapRequestDto.Width)
            .AddParameter("h", mapRequestDto.Height)
            .AddParameter("center", $"{mapRequestDto.Longitude},{mapRequestDto.Latitude}")
            .AddParameter("level", mapRequestDto.Level)
            .AddParameter("format", "png8")
            .AddParameter("scale", 2)
            .AddParameter("markers", $"pos:{mapRequestDto.Longitude} {mapRequestDto.Latitude}");
        
        foreach (var marker in mapRequestDto.Markers)
        {
            request.AddParameter("markers", $"color:red|pos:{marker.Longitude} {marker.Latitude}");
        }

        Uri uri = staticMapClient.BuildUri(request);
        Debug.Log($"StaticMap API Request Start - {DateTime.Now}\n{uri}");
        byte[] imageBinary = await staticMapClient.DownloadDataAsync(request, tokenSource.Token);
        Debug.Log($"StaticMap API Request Finish - {DateTime.Now}\n{uri}");
        Debug.Log($"result: length = {imageBinary.Length}");
        return new MapResponseDto(imageBinary);
    }

    // public async Task<MapResponseDto> FindMapByCurrentLocationWithMarker(MapRequestDto mapRequestDto)
    // {
    //     RestRequest request = new RestRequest();
    //     request
    //         .AddParameter("w", mapRequestDto.Width)
    //         .AddParameter("h", mapRequestDto.Height)
    //         .AddParameter("center", $"{mapRequestDto.Longitude},{mapRequestDto.Latitude}")
    //         .AddParameter("level", mapRequestDto.Level)
    //         .AddParameter("format", "png8")
    //         .AddParameter("scale", 2);
    //
    //     Debug.Log($"StaticMap API Request Start - {DateTime.Now}");
    //     byte[] imageBinary = await staticMapClient.DownloadDataAsync(request, tokenSource.Token);
    //     Debug.Log($"StaticMap API Request Finish - {DateTime.Now}");
    //     return new MapResponseDto(imageBinary);
    // }

    public async Task<Coords> GetCurrentLocation()
    {
        return currentCoords;
    }

    private void InitStaticMapClient()
    {
        var options = new RestClientOptions(staticMapBaseUrl);
        staticMapClient = new RestClient(options);
        staticMapClient
            .AddDefaultHeader("X-NCP-APIGW-API-KEY-ID", clientId)
            .AddDefaultHeader("X-NCP-APIGW-API-KEY", clientSecret);
    }
}