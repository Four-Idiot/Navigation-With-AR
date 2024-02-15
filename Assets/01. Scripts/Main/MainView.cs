using UnityEngine;
using UnityEngine.UIElements;

public class MainView: UIView
{

    private VisualElement navButton;
    private VisualElement seniorButton;

    protected override void Awake()
    {
        base.Awake();
        navButton = uiInstance.Q<VisualElement>("navigation-button");
        navButton.RegisterCallback<ClickEvent>(OnNavButtonClicked);
    }

    private void OnNavButtonClicked(ClickEvent evt)
    {
        Debug.Log("Clicked");
        UINavigation.Instance.Push(UIViewIndex.AR_NAVIGATION);
    }
}