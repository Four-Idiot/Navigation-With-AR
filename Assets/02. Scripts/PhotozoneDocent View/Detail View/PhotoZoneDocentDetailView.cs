using UnityEngine.UIElements;

public class PhotoZoneDocentDetailView : UIView
{

    private Label nameLabel;
    private Label descriptionLabel;
    private VisualElement backButton;

    protected override void Awake()
    {
        base.Awake();
        nameLabel = uiInstance.Q<Label>("name");
        descriptionLabel = uiInstance.Q<Label>("description");
        backButton = uiInstance.Q<VisualElement>("back-button");
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

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }

    public void Init(string name, string description)
    {
        nameLabel.text = name;
        descriptionLabel.text = description;
    }
}