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
        seniorButton = uiInstance.Q<VisualElement>("senior-button");
        navButton.RegisterCallback<ClickEvent>(OnNavButtonClicked);
        seniorButton.RegisterCallback<ClickEvent>(OnSeniorButtonClicked);
    }

    private void OnSeniorButtonClicked(ClickEvent evt)
    {
        Debug.Log("Photo Clicked");
        UINavigation.Instance.Push(UIViewIndex.PHOTO);
    }

    private void OnNavButtonClicked(ClickEvent evt)
    {
        Debug.Log("Clicked");
        // var navView = UINavigation.Instance.Push(UIViewIndex.AR_NAVIGATION) as NavigationView;
        UIView navView = UINavigation.Instance.Push(UIViewIndex.AR_NAVIGATION);
        NavigationView nav = navView as NavigationView;
    }
}