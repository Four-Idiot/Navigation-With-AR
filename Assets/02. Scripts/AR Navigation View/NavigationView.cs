using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 네비게이션 UI
/// </summary>
public class NavigationView : UIView
{
    [SerializeField]
    private VisualTreeAsset hospitalMarker;
    [SerializeField]
    private VisualTreeAsset dosentMarker;
    [SerializeField]
    private VisualTreeAsset metroMarker;
    [SerializeField]
    private VisualTreeAsset parkMarker;
    [SerializeField]
    private VisualTreeAsset parkingAreaMarker;
    [SerializeField]
    private VisualTreeAsset toiletMarker;
    
    private NavigationService navigationService;
    private VisualElement map;
    private int currentZoomLevel = 15;
    private const int widthHalf = 180;
    private const int heightHalf = 400;


    protected override void Awake()
    {
        base.Awake();
        map = uiInstance.Q<VisualElement>("flat-map");
        navigationService = Config.Instance.NavigationService();
    }

    public override void Show()
    {
        base.Show();
        PaintMapTest();
    }
    private async Task PaintMapTest()
    {
        var currentMap = await navigationService.FindMapByCurrentLocation(currentZoomLevel);
        map.style.backgroundImage = new StyleBackground(currentMap.MapTexture);
        map.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        foreach (var poiInfo in currentMap.PoiInfos)
        {
            VisualElement marker = hospitalMarker.Instantiate();
            marker.style.translate = new StyleTranslate(
                new Translate(widthHalf + poiInfo.PositionX, heightHalf - poiInfo.PositionY)
            );
            uiInstance.Q<VisualElement>("flat-map").Add(marker);
        }
        VisualElement centerMarker = hospitalMarker.Instantiate();
        centerMarker.style.translate = new StyleTranslate(new Translate(widthHalf, heightHalf));
        uiInstance.Q<VisualElement>("flat-map").Add(centerMarker);
    }
}