using UnityEngine.UIElements;

public class DocentDetailView : UIView
{

    #region Elements

    private Label nameLabel;
    private Label descriptionLabel;
    private VisualElement backButton;
    private VisualElement locationButton;
    private VisualElement cameraButton;
    private VisualElement locationImage;

    #endregion
    
    private PoiInfo poiInfo;

    private void Start()
    {
        nameLabel = uiInstance.Q<Label>("name");
        descriptionLabel = uiInstance.Q<Label>("description");
        backButton = uiInstance.Q<VisualElement>("back-button");
        locationButton = uiInstance.Q<VisualElement>("view-location");
        cameraButton = uiInstance.Q<VisualElement>("camera-button");
        locationImage = uiInstance.Q<VisualElement>("location-image");
    }

    public override void Show()
    {
        base.Show();
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        locationButton.RegisterCallback<ClickEvent>(OnLocationButtonClicked);
        cameraButton.RegisterCallback<ClickEvent>(OnCameraButtonClicked);
    }

    public override void Hide()
    {
        base.Hide();
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
        locationButton.UnregisterCallback<ClickEvent>(OnLocationButtonClicked);
        cameraButton.UnregisterCallback<ClickEvent>(OnCameraButtonClicked);
    }

    private void OnCameraButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Push(UIViewIndex.AR_PHOTOZONE);
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
        nameLabel.text = poiInfo.Name;
        descriptionLabel.text = poiInfo.Address;
        locationImage.style.backgroundImage = new StyleBackground(poiInfo.image);
    }
}