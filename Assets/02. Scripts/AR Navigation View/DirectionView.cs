using UnityEngine.UIElements;

public class DirectionView: UIView
{
    private VisualElement backButton;
    
    protected override void Awake()
    {
        base.Awake();
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
}