using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 네비게이션 UI
/// </summary>
public class NavigationView : UIView
{
    private Button backButton;
    private Button mapTestButton;
    private Image image;

    protected override void Awake()
    {
        base.Awake();
        mapTestButton = new Button();
        image = new Image();
        mapTestButton.style.width = 350;
        mapTestButton.style.height = 200;
        mapTestButton.style.backgroundColor = new StyleColor(Color.yellow);
        uiInstance.Add(mapTestButton);
        backButton = uiInstance.Q<Button>("back-button");

        image.style.width = 350;
        image.style.height = 450;
        uiInstance.Add(image);
    }

    private void OnClickBackButton(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }

    private async void OnClickStaticMapTestButton(ClickEvent evt)
    {
        var texture = await Config.Instance.NavigationService.MapByCurrentLocation();
        image.image = texture;
    }

    public override void Show()
    {
        base.Show();
        backButton.RegisterCallback<ClickEvent>(OnClickBackButton);
        mapTestButton.RegisterCallback<ClickEvent>(OnClickStaticMapTestButton);
    }

    public override void Hide()
    {
        base.Hide();
        backButton.UnregisterCallback<ClickEvent>(OnClickBackButton);
        mapTestButton.UnregisterCallback<ClickEvent>(OnClickStaticMapTestButton);
    }
}