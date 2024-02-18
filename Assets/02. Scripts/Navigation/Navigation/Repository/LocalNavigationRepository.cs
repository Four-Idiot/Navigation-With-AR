using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class LocalNavigationRepository : INavigationRepository
{
    private readonly CancellationTokenSource tokenSource = new();

    public async Task<MapResponseDto> FindMapByCurrentLocation(MapRequestDto mapRequestDto)
    {
        //Assets/04. Resources/sample_map.png
        string filePath = $"{Application.dataPath}/04. Resources/sample_map.png";
        Debug.Log("Start Local Image Loading");
        var binaryImage = await File.ReadAllBytesAsync(filePath);
        Debug.Log($"Finish Local Image Loading: result length = {binaryImage.Length}");
        return new MapResponseDto(binaryImage);
    }
}