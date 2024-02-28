using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using static MarkerType;

public class LocalMarkerRepository : IMarkerRepository
{
    private readonly List<PoiInfo> poiInfos;
    private int idSequence = 0;
    private bool isPhotozoneAdded = false;
    private bool isDocentAdded = false;

    public LocalMarkerRepository()
    {
        poiInfos = new()
        {
            new PoiInfo(idSequence++, PARK, "cafe 07 am", String.Empty, new Coords(126.741800f, 37.714779f), "경기 파주시 와석순환로192번길 14-38 1층 카페.07.am", 9, 20),
            new PoiInfo(idSequence++, PARKING_AREA, "더 브래드 36.5도", "운정점", new Coords(126.741665f, 37.714677f), "경기 파주시 와석순환로192번길 14-43", 9, 20),
            new PoiInfo(idSequence++, METRO, "마장동뚝배기&1인한우육회", String.Empty, new Coords(126.741080f, 37.714145f), "경기 파주시 와석순환로172번길 3", 9, 20),
            new PoiInfo(idSequence++, PARKING_AREA, "홍익돈까스", "파주운정점", new Coords(126.740820f, 37.714277f), "경기 파주시 와석순환로172번길 3", 9, 20),
            new PoiInfo(idSequence++, HOSPITAL, "병원", String.Empty, new Coords(126.743572f, 37.713675f), "경기 파주시 와석순환로172번길 16", 9, 20),
        };
    }

    private void AddPhotozone()
    {
        for (int i = 1; i <= 5; ++i)
        {
            var findImages = FindPhotozoneImages(i);
            poiInfos.Add(new(
                idSequence++,
                PHOTOZONE,
                $"포토존 {i} 이름",
                $"포토존 {i} 지점명",
                new(126.741800f, 37.714779f),
                $"포토존 {i} 주소",
                0,
                0,
                image: findImages
            ));
        }
        isPhotozoneAdded = true;
    }

    private void AddDocent()
    {
        string[] docentName = { "영스퀘어", "주인공원", "지하상가", "숭의목공예", "제물포역" };

        for (int i = 0; i < docentName.Length; ++i)
        {
            string name = docentName[i];
            string filePath = $"Tracking Image/{name}";
            Debug.Log($"Start Local Image Loading {name}");
            var loadedImage = Resources.Load<Texture2D>(filePath);
            Debug.Log($"Finish Local Image Loading {name}: result name = {loadedImage.name}");
            poiInfos.Add(new(
                idSequence++,
                DOCENT,
                $"도슨트 {i + 1} 이름",
                $"도슨트 {i + 1} 지점명",
                new(126.741800f, 37.713779f),
                $"도슨트 {i + 1} 주소",
                0,
                0,
                image: loadedImage
            ));
        }
        isDocentAdded = true;
    }
    public async Task<MarkerResponseDto> FindPoiInfo()
    {
        if (!isPhotozoneAdded)
            AddPhotozone();
        if (!isDocentAdded)
            AddDocent();
        return new MarkerResponseDto(poiInfos);
    }

    private Texture2D FindPhotozoneImages(int index)
    {
        string fileName = $"포토존{index}";
        string filePath = $"Tracking Image/{fileName}";
        Debug.Log($"Start Local Image Loading {fileName}");
        var loadedImage = Resources.Load<Texture2D>(filePath);
        Debug.Log($"Finish Local Image Loading {fileName}: result name = {loadedImage.name}");
        return loadedImage;
    }

}