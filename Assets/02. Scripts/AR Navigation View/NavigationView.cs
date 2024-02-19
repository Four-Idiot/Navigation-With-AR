using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static MarkerType;

/// <summary>
/// 네비게이션 UI
/// </summary>
public class NavigationView : UIView
{

    #region markers

    [Space]
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

    #endregion

    private NavigationService navigationService;
    private VisualElement map;
    private int currentZoomLevel = 15;
    private const int widthHalf = 180;
    private const int heightHalf = 400;

    private VisualElement backButton;

    private CategoryController categoryController;
    
    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        navigationService = Config.Instance.NavigationService();
        categoryController = new CategoryController(uiInstance);
        map = uiInstance.Q<VisualElement>("flat-map");
        backButton = uiInstance.Q<VisualElement>("back-button");
    }

    public override void Show()
    {
        base.Show();
        categoryController.RegisterButtonCallback();
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        PaintMapTest();
    }

    public override void Hide()
    {
        base.Hide();
        categoryController.UnregisterCallback();
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
    }

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }
    
    private async Task PaintMapTest()
    {
        var currentMap = await navigationService.FindMapByCurrentLocation(currentZoomLevel);
        map.style.backgroundImage = new StyleBackground(currentMap.MapTexture);
        map.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        foreach (var markerInfo in currentMap.Markers)
        {
            var marker = GetMarkerVisualElement(markerInfo.Type);
            marker.style.translate = new StyleTranslate(
                new Translate(widthHalf + markerInfo.PositionX, heightHalf - markerInfo.PositionY)
            );
            uiInstance.Q<VisualElement>("flat-map").Add(marker);
        }
    }
    private VisualElement GetMarkerVisualElement(MarkerType type)
    {
        VisualElement marker = type switch
        {
            DOSENT => dosentMarker.Instantiate(),
            HOSPITAL => hospitalMarker.Instantiate(),
            METRO => metroMarker.Instantiate(),
            PARK => parkMarker.Instantiate(),
            PARKING_AREA => parkingAreaMarker.Instantiate(),
            TOILET => toiletMarker.Instantiate()
        };
        return marker;
    }
}