using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DirectionView : UIView
{

    #region Elements

    private VisualElement backButton;
    private VisualElement mapElement;
    private Label destinationLabel;

    #endregion

    private NavigationService navigationService;

    private void Start()
    {
        backButton = uiInstance.Q<VisualElement>("back-button");
        mapElement = uiInstance.Q<VisualElement>("flat-map");
        destinationLabel = uiInstance.Q<Label>("destination-label");
        navigationService = Config.Instance.NavigationService();
    }

    public override void Show()
    {
        base.Show();
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
    }

    public override void Hide()
    {
        base.Hide();
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
    }

    public async void Init(PoiInfo poiInfo)
    {
        destinationLabel.text = poiInfo.Name;
        var map = await navigationService.FindMapByCurrentLocation(poiInfo.Coords);
        mapElement.style.backgroundImage = new StyleBackground(map.MapTexture);
    }

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }
}