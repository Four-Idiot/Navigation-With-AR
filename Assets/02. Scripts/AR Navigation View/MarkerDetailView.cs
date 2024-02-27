using UnityEngine;
using UnityEngine.UIElements;
using static UIViewIndex;

public class MarkerDetailView : UIView
{
    private PoiInfo poiInfo;
    private Texture2D backgroundImage;

    private VisualElement backgroundImageElement;
    private Label nameElement;
    private Label descriptionElement;
    private Label addressElement;
    private VisualElement backButton;
    private VisualElement guideButton;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        backgroundImageElement = uiInstance.Q<VisualElement>("background-image");
        nameElement = uiInstance.Q<Label>("name");
        addressElement = uiInstance.Q<Label>("address");
        descriptionElement = uiInstance.Q<Label>("description");
        backButton = uiInstance.Q<VisualElement>("back-button");
        guideButton = uiInstance.Q<VisualElement>("guide-button");
    }

    public override void Show()
    {
        base.Show();
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        guideButton.RegisterCallback<ClickEvent>(OnGuideButtonClicked);
    }
    
    public override void Hide()
    {
        base.Hide();
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
        guideButton.UnregisterCallback<ClickEvent>(OnGuideButtonClicked);
    }

    private void OnGuideButtonClicked(ClickEvent evt)
    {
        var directionView = UINavigation.Instance.Push(DIRECTION) as DirectionView;
        directionView.Init(poiInfo);
    } 
    
    public void SetState(PoiInfo poiInfo)
    {
        this.poiInfo = poiInfo;
        SetVisualElementValue();
    }


    private void SetVisualElementValue()
    {
        if (backgroundImage != null)
            backgroundImageElement.style.backgroundImage = new StyleBackground(backgroundImage);

        nameElement.text = poiInfo.Name;
        addressElement.text = poiInfo.Address;
        descriptionElement.text = poiInfo.BranchName;
    }

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }
}