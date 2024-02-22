using UnityEngine;
using UnityEngine.UIElements;

public class MarkerDetailView : UIView
{
    private string name;
    private string description;
    private string address;
    private Texture2D backgroundImage;

    private VisualElement backgroundImageElement;
    private Label nameElement;
    private Label descriptionElement;
    private Label addressElement;
    private VisualElement backButton;

    protected override void Awake()
    {
        base.Awake();
        backgroundImageElement = uiInstance.Q<VisualElement>("background-image");
        nameElement = uiInstance.Q<Label>("name");
        addressElement = uiInstance.Q<Label>("address");
        descriptionElement = uiInstance.Q<Label>("description");
        backButton = uiInstance.Q<VisualElement>("back-button");
    }

    public void SetState(string name = "", string description = "", string address = "", Texture2D backgroundImage = null)
    {
        this.name = name;
        this.description = description;
        this.address = address;
        this.backgroundImage = backgroundImage;
        SetVisualElementValue();
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

    private void SetVisualElementValue()
    {
        if (backgroundImage != null)
            backgroundImageElement.style.backgroundImage = new StyleBackground(backgroundImage);

        nameElement.text = name;
        addressElement.text = address;
        descriptionElement.text = description;
    }

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }
}