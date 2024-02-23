using System.Collections.Generic;
using System.Threading.Tasks;

public class MarkerService: Component
{
    private readonly IMarkerRepository markerRepository;

    public MarkerService(IMarkerRepository markerRepository)
    {
        this.markerRepository = markerRepository;
    }

    public async Task<List<Marker>> FindPoiInfo()
    {
        var responsePoiDto = await markerRepository.FindPoiInfo();
        return responsePoiDto.PoiInfos;
    }
}