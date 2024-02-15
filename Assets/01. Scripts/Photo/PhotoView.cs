using UnityEngine.UIElements;

public class PhotoView : UIView
{
    private VisualElement backButton;

    protected override void Awake()
    {
        base.Awake();
        backButton = uiInstance.Q<VisualElement>("back-button");
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
    }

    private void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }

}
