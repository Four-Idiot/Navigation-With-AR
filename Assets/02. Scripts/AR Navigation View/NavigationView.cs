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
    private NavigationService navigationService;
    private VisualElement map;
    private int currentZoomLevel = 17;
    private float currentScale = 0.597f;
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
        CalcScale(currentZoomLevel);
        Debug.Log($"scale = {currentScale}");
        // PaintMapTest();
        PaintMapTest();
    }
    private async Task PaintMapTest()
    {
        var currentMap = await navigationService.FindMapByCurrentLocation(currentZoomLevel);
        map.style.backgroundImage = new StyleBackground(currentMap.MapTexture);
        map.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        foreach (var poiInfo in currentMap.PoiInfos)
        {
            VisualElement marker = hospitalMarker.Instantiate().Children().First();
            double x = (poiInfo.Coords.Longitude - 126.744577) * 6378 * Mathf.Cos(poiInfo.Coords.Latitude * Mathf.Deg2Rad) / currentScale * currentZoomLevel;
            double y = (poiInfo.Coords.Latitude - 37.713834) * 6378 / currentScale * currentZoomLevel;
            Debug.Log($"{x} {y}");
            marker.style.translate = new StyleTranslate(
                new Translate((float)x + widthHalf, heightHalf - (float)y));
            uiInstance.Q<VisualElement>("flat-map").Add(marker);
        }
        VisualElement centerMarker = hospitalMarker.Instantiate().Children().First();
        centerMarker.style.translate = new StyleTranslate(new Translate(widthHalf, heightHalf));
        uiInstance.Q<VisualElement>("flat-map").Add(centerMarker);

        // VisualElement testMarker = hospitalMarker.Instantiate().Children().First();
        // testMarker.style.translate = new StyleTranslate(new Translate(widthHalf - (8.493650298f * (currentZoomLevel - 1)), heightHalf + (1.698663317f * currentZoomLevel)));
        // uiInstance.Q<VisualElement>("flat-map").Add(testMarker);
    }

    private async Task PaintMap()
    {
        var currentMap = await navigationService.FindMapByCurrentLocation(currentZoomLevel);
        map.style.backgroundImage = new StyleBackground(currentMap.MapTexture);
        map.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        // foreach (var poiInfo in currentMap.PoiInfos)
        // {
        //     float distance = (float)CalculateDistanceBetweenCoords(currentMap.Center, poiInfo.Coords);
        //     VisualElement marker = hospitalMarker.Instantiate().Children().First();
        //     // Vector2 direction = poiInfo.Direction * (distance / Mathf.Cos(poiInfo.Coords.Latitude));
        //     Vector2 direction = poiInfo.Direction * (distance / currentScale);
        //     marker.style.translate = new StyleTranslate(
        //         new Translate(
        //             new Length(widthHalf + direction.x),
        //             new Length(heightHalf + direction.y)
        //         ));
        //     uiInstance.Q<VisualElement>("flat-map").Add(marker);
        // }
        VisualElement centerMarker = hospitalMarker.Instantiate().Children().First();
        centerMarker.style.translate = new StyleTranslate(new Translate(widthHalf, heightHalf));
        uiInstance.Q<VisualElement>("flat-map").Add(centerMarker);
    }

    // private async Task PaintMap()
    // {
    //     var currentMap = await navigationService.FindMapByCurrentLocation();
    //     map.style.backgroundImage = new StyleBackground(currentMap.MapTexture);
    //     map.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    //     Vector2 normalVector = new Vector2(-0.001005f, 0.000159f).normalized;
    //     Debug.Log(normalVector);
    //     float distance = (float)CoordinatesCalculator.CalculateDistanceBetweenCoords(new Coords(126.744577, 37.713834), new Coords(126.743572, 37.713675));
    //     Debug.Log($"distance = {distance}");
    //     distance *= CalcScale(17);
    //     VisualElement marker = hospitalMarker.Instantiate().Children().First();
    //     marker.style.translate = new StyleTranslate(new Translate(new Length(180 + (normalVector.x * 2 * distance)), new Length(400 + (normalVector.y * 2 * distance))));
    //     uiInstance.Q<VisualElement>("flat-map").Add(marker);
    //     Debug.Log($"position = {marker.transform.position.x} {marker.transform.position.y}");
    // }

    private float CalcScale(int level)
    {
        return currentScale = (float)156_543 / (1 << level + 1);
    }
}