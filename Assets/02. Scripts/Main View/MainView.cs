using UnityEngine;
using UnityEngine.UIElements;

public class MainView: UIView
{

    private VisualElement navButton;
    private VisualElement arButton;
    
    protected override void Awake()
    {
        base.Awake();
        navButton = uiInstance.Q<VisualElement>("navigation-button");
        arButton = uiInstance.Q<VisualElement>("ar-button");
    }

    public override void Show()
    {
        base.Show();
        navButton.RegisterCallback<ClickEvent>(OnNavButtonClicked);
        arButton.RegisterCallback<ClickEvent>(OnArButtonClicked);
    }

    public override void Hide()
    {
        base.Hide();
        navButton.UnregisterCallback<ClickEvent>(OnNavButtonClicked);
        arButton.UnregisterCallback<ClickEvent>(OnArButtonClicked);
    }

    private void OnNavButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Push(UIViewIndex.AR_NAVIGATION);
    }
    private void OnArButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Push(UIViewIndex.PHOTOZONE_DOCENT);
    }
}