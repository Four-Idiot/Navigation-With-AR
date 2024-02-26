using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

public class PoiListController
{
    private VisualTreeAsset listEntryTemplate;

    private ListView photozoneList;
    private ListView docentList;
    private List<Marker> poiList;

    public void InitializePoiList(VisualTreeAsset listTemplate, ListView photozoneList, ListView docentList, List<Marker> poiList)
    {
        listEntryTemplate = listTemplate;
        this.photozoneList = photozoneList;
        this.docentList = docentList;
        this.poiList = poiList;
        FillPhotozoneList();
        FillDocentList();
    }

    private void FillPhotozoneList()
    {
        var photozonePoiList = poiList.Where(poi => poi.Type == MarkerType.PHOTOZONE)
            .ToList();
        FillList(photozoneList, photozonePoiList);
    }
    
    private void FillDocentList()
    {
        var docentPoiList = poiList.Where(poi => poi.Type == MarkerType.DOCENT)
            .ToList();
        FillList(docentList, docentPoiList);
    }

    private void FillList(ListView list, List<Marker> selectedPoiList)
    {
        list.makeItem = () =>
        {
            var newListEntry = listEntryTemplate.Instantiate();
            var newListEntryLogic = new PoiListEntryController();
            newListEntry.userData = newListEntryLogic;
            newListEntryLogic.SetVisualElement(newListEntry);
            return newListEntry;
        };

        list.bindItem = (element, index) =>
        {
            (element.userData as PoiListEntryController).SetPoiData(selectedPoiList[index]);
        };

        list.fixedItemHeight = 220;

        list.itemsSource = selectedPoiList;
    }
}