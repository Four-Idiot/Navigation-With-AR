using System.Collections.Generic;
using System.Threading.Tasks;

public class PoiService
{
    private readonly IPoiRepository poiRepository;

    public PoiService(IPoiRepository poiRepository)
    {
        this.poiRepository = poiRepository;
    }

    public async Task<List<PoiInfo>> FindPoiInfo()
    {
        var responsePoiDto = await poiRepository.FindPoiInfo();
        return responsePoiDto.PoiInfos;
    }
}