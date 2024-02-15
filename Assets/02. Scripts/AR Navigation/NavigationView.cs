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
        var response = await Config.Instance.Repository
            .FindMapByCurrentLocation(
                new(
                    37.3591614, 127.1054221, 1024,
                    1024, 15, true, string.Empty
                ));
        Debug.Log(response.binaryImage);
        Texture2D tex = new(350, 450);
        tex.LoadImage(response.binaryImage);
        image.image = tex;
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