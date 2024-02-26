using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LocalMarkerRepository : IMarkerRepository
{
    private readonly List<Marker> poiInfos;

    public LocalMarkerRepository()
    {
        int id = 1;
        poiInfos = new()
        {
            new Marker(id++, MarkerType.PARK, "cafe 07 am", String.Empty, new Coords(126.741800f, 37.714779f), "경기 파주시 와석순환로192번길 14-38 1층 카페.07.am", 9, 20),
            new Marker(id++, MarkerType.PARKING_AREA, "더 브래드 36.5도", "운정점", new Coords(126.741665f,37.714677f), "경기 파주시 와석순환로192번길 14-43",9, 20),
            new Marker(id++, MarkerType.METRO, "마장동뚝배기&1인한우육회", String.Empty, new Coords(126.741080f,37.714145f),"경기 파주시 와석순환로172번길 3", 9, 20),
            new Marker(id++, MarkerType.DOCENT, "홍익돈까스", "파주운정점", new Coords(126.740820f,37.714277f), "경기 파주시 와석순환로172번길 3", 9, 20),
            new Marker(id++, MarkerType.DOCENT, "도슨트 샘플1", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.DOCENT, "도슨트 샘플2", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.HOSPITAL, "병원", String.Empty, new Coords(126.743572f,37.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.PHOTOZONE, "포토존 샘플1", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.PHOTOZONE, "포토존 샘플2", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.PHOTOZONE, "포토존 샘플3", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.PHOTOZONE, "포토존 샘플4", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.PHOTOZONE, "포토존 샘플5", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
            new Marker(id++, MarkerType.PHOTOZONE, "포토존 샘플6", String.Empty, new Coords(127.743572f,39.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
        };
    }

    public Task<MarkerResponseDto> FindPoiInfo()
    {
        return Task.FromResult(new MarkerResponseDto(poiInfos));
    }
}