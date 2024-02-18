using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LocalPoiRepository : IPoiRepository
{
    private readonly List<PoiInfo> poiInfos;

    public LocalPoiRepository()
    {
        int id = 1;
        poiInfos = new()
        {
            new PoiInfo(id++, PoiType.CAFE, "cafe 07 am", String.Empty, new Coords(126.741800f, 37.714779f), "경기 파주시 와석순환로192번길 14-38 1층 카페.07.am", 9, 20),
            new PoiInfo(id++, PoiType.RESTAURANT, "더 브래드 36.5도", "운정점", new Coords(126.741665f,37.714677f), "경기 파주시 와석순환로192번길 14-43",9, 20),
            new PoiInfo(id++, PoiType.RESTAURANT, "마장동뚝배기&1인한우육회", String.Empty, new Coords(126.741080f,37.714145f),"경기 파주시 와석순환로172번길 3", 9, 20),
            new PoiInfo(id++, PoiType.RESTAURANT, "홍익돈까스", "파주운정점", new Coords(126.740820f,37.714277f), "경기 파주시 와석순환로172번길 3", 9, 20),
            new PoiInfo(id++, PoiType.PUBLIC, "대한상공회의소 경기인력개발원", String.Empty, new Coords(126.743572f,37.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
        };
    }

    public Task<PoiInfoResponseDto> FindPoiInfo()
    {
        return Task.FromResult(new PoiInfoResponseDto(poiInfos));
    }
}