using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PhotozoneDetailView : UIView
{

    #region Elements

    private Label nameLabel;
    private Label descriptionLabel;
    private VisualElement backButton;
    private VisualElement locationButton;
    private Label title;

    #endregion
    
    private PoiInfo poiInfo;

    private void Start()
    {
        nameLabel = uiInstance.Q<Label>("name");
        descriptionLabel = uiInstance.Q<Label>("description");
        backButton = uiInstance.Q<VisualElement>("back-button");
        title = uiInstance.Q<Label>("title");
        locationButton = uiInstance.Q<VisualElement>("view-location");
    }

    public override void Show()
    {
        base.Show();
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        locationButton.RegisterCallback<ClickEvent>(OnLocationButtonClicked);
    }

    public override void Hide()
    {
        base.Hide();
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
        locationButton.UnregisterCallback<ClickEvent>(OnLocationButtonClicked);
    }

    private void OnLocationButtonClicked(ClickEvent evt)
    {
        var directionView = UINavigation.Instance.Push(UIViewIndex.DIRECTION) as DirectionView;
        directionView.Init(poiInfo);
    }

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }

    public void Init(PoiInfo poiInfo)
    {
        this.poiInfo = poiInfo;
        if (poiInfo.Type == MarkerType.PHOTOZONE)
        {
            title.text = "AR 포토존";
        }
        else
        {
            title.text = "AR 고스트 도슨트";
        }
        nameLabel.text = poiInfo.Name;
        descriptionLabel.text = poiInfo.Address;
    }
}