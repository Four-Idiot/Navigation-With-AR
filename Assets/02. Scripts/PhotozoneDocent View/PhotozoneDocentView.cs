using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PhotozoneDocentView: UIView
{

    private TabMenuController tabMenuController;
    private PoiListController poiListController;
    private MarkerService poiService;
    
    [SerializeField]
    private VisualTreeAsset listEntryTemplate;
    private ListView photozoneList;
    private ListView docentList;
    
    protected override void Awake()
    {
        base.Awake();
        InitElement();
    }

    private void Start()
    {
        poiService = Config.Instance.PoiService();
    }


    private void InitElement()
    {
        tabMenuController = new TabMenuController(uiInstance.Q<VisualElement>("container"));
        poiListController = new PoiListController();
        photozoneList = uiInstance.Q<ListView>("photozone-list");
        docentList = uiInstance.Q<ListView>("docent-list");
        photozoneList.Q<ScrollView>().verticalScrollerVisibility = ScrollerVisibility.Hidden;
        docentList.Q<ScrollView>().verticalScrollerVisibility = ScrollerVisibility.Hidden;
    }

    public override async void Show()
    {
        base.Show();
        tabMenuController.RegisterTabCallbacks();
        var poiList = await poiService.FindPoiInfo();
        poiListController.InitializePoiList(listEntryTemplate, photozoneList, docentList, poiList);
    }

    public override void Hide()
    {
        base.Hide();
        tabMenuController.UnregisterTabCallbacks();
    }
}