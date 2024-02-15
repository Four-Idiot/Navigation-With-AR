using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using UnityEngine;

public class NetworkRepository : IRepository
{
    private readonly string clientId;
    private readonly string clientSecret;
    private readonly string staticMapBaseUrl;

    private RestClient staticMapClient;
    private readonly CancellationTokenSource tokenSource = new();

    public NetworkRepository(string clientId, string clientSecret, string staticMapBaseUrl)
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
            .AddParameter("w", requestMapDto.width)
            .AddParameter("h", requestMapDto.height)
            .AddParameter("center", $"{requestMapDto.longitude},{requestMapDto.latitude}")
            .AddParameter("level", requestMapDto.level)
            .AddParameter("format", "png8");
        
        Debug.Log("Start DownloadData");
        byte[] imageBinary = await staticMapClient.DownloadDataAsync(request, tokenSource.Token);
        Debug.Log("End DownloadData");
        Debug.Log(imageBinary.Length);
        return new ResponseMapDto(imageBinary);
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