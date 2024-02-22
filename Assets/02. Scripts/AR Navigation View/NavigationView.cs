using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static MarkerType;
using static UIViewIndex;

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
    private Map map;
    private VisualElement mapElement;
    private int currentZoomLevel = 15;
    private const int widthHalf = 180;
    private const int heightHalf = 400;

    private VisualElement backButton;
    private SliderInt zoomLevelSlider; 

    private CategoryController categoryController;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        navigationService = Config.Instance.NavigationService();
        mapElement = uiInstance.Q<VisualElement>("flat-map");
        backButton = uiInstance.Q<VisualElement>("back-button");
        zoomLevelSlider = uiInstance.Q<SliderInt>("level-slider");
        categoryController = new CategoryController(uiInstance);
    }

    public override void Show()
    {
        base.Show();
        categoryController.RegisterButtonCallback();
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        zoomLevelSlider.RegisterCallback<ChangeEvent<int>>(OnLevelSliderValueChanged);
        PaintMap();
    }

    public override void Hide()
    {
        base.Hide();
        CleanMap();
    }

    private void OnLevelSliderValueChanged(ChangeEvent<int> evt)
    {
        currentZoomLevel = evt.newValue;
        Debug.Log(currentZoomLevel);
        PaintMap(true);
    }
    
    private void CleanMap()
    {
        categoryController.UnregisterCallback();
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
        zoomLevelSlider.UnregisterCallback<ChangeEvent<int>>(OnLevelSliderValueChanged);
        mapElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        mapElement.style.opacity = new StyleFloat(0f);
    }

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }

    private async Task PaintMap(bool repaint = false)
    {
        if (map == null || repaint)
            map = await navigationService.FindMapByCurrentLocation(currentZoomLevel);
        
        mapElement.Clear();
        mapElement.style.backgroundImage = new StyleBackground(map.MapTexture);
        mapElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        mapElement.style.opacity = new StyleFloat(0f);
        PaintMarker(map);
        mapElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        mapElement.style.opacity = new StyleFloat(1f);
    }
    
    
    private void PaintMarker(Map currentMap)
    {
        foreach (var markerInfo in currentMap.Markers)
        {
            var marker = GetMarkerVisualElement(markerInfo.Type);
            marker.style.translate = new StyleTranslate(
                new Translate(widthHalf + markerInfo.PositionX, heightHalf - markerInfo.PositionY)
            );
            VisualElement markerIcon = marker.Q<VisualElement>(className: "marker-icon");
            markerIcon.RegisterCallback(delegate(ClickEvent _) { OnMarkerClicked(markerInfo); });
            uiInstance.Q<VisualElement>("flat-map").Add(marker);
        }
    }

    private void OnMarkerClicked(Marker marker)
    {
        var markerDetailView = UINavigation.Instance.Push(MARKER_DETAIL) as MarkerDetailView;
        markerDetailView.SetState(marker.Name, marker.BranchName, marker.Address);
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