using UnityEngine;
using UnityEngine.UIElements;

public class MainView: UIView
{

    private VisualElement navButton;
    
    protected override void Awake()
    {
        base.Awake();
        navButton = uiInstance.Q<VisualElement>("navigation-button");
    }

    public override void Show()
    {
        base.Show();
        navButton.RegisterCallback<ClickEvent>(OnNavButtonClicked);
    }

    public override void Hide()
    {
        base.Hide();
        navButton.UnregisterCallback<ClickEvent>(OnNavButtonClicked);
    }

    private void OnNavButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Push(UIViewIndex.AR_NAVIGATION);
    }
}