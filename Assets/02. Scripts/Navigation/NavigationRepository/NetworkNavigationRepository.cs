using System;
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

    public async Task<ResponseMapDto> FindMapByCurrentLocation(RequestMapDto requestMapDto)
    {
        RestRequest request = new RestRequest();
        request
            .AddParameter("w", requestMapDto.Width)
            .AddParameter("h", requestMapDto.Height)
            .AddParameter("center", $"{requestMapDto.Longitude},{requestMapDto.Latitude}")
            .AddParameter("level", requestMapDto.Level)
            .AddParameter("format", "png8")
            .AddParameter("scale", 2);

        Debug.Log($"StaticMap API Request Start - {DateTime.Now}");
        byte[] imageBinary = await staticMapClient.DownloadDataAsync(request, tokenSource.Token);
        Debug.Log($"StaticMap API Request Finish - {DateTime.Now}");
        return new ResponseMapDto(imageBinary);
    }

    public async Task<ResponseMapDto> FindMapByCurrentLocationWithMarker(RequestMapDto requestMapDto)
    {
        RestRequest request = new RestRequest();
        request
            .AddParameter("w", requestMapDto.Width)
            .AddParameter("h", requestMapDto.Height)
            .AddParameter("center", $"{requestMapDto.Longitude},{requestMapDto.Latitude}")
            .AddParameter("level", requestMapDto.Level)
            .AddParameter("format", "png8")
            .AddParameter("scale", 2);

        Debug.Log($"StaticMap API Request Start - {DateTime.Now}");
        byte[] imageBinary = await staticMapClient.DownloadDataAsync(request, tokenSource.Token);
        Debug.Log($"StaticMap API Request Finish - {DateTime.Now}");
        return new ResponseMapDto(imageBinary);
    }
    
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