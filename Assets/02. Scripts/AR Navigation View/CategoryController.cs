using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class CategoryController
{
    private readonly VisualElement root;

    private const string buttonClassName = "category-button";
    private const string selectedButtonClassName = "selected-button";
    private const string unselectedButtonClassName = "unselected-button";

    private const string allButtonName = "all-button";
    private const string publicButtonName = "public-button";
    private const string hospitalButtonName = "hospital-button";
    private const string cameraButtonName = "camera-button";

    private const string selectedMarkerClassName = "selected-marker";
    private const string unselectedMarkerClassName = "unselected-marker";

    public CategoryController(VisualElement root)
    {
        this.root = root;
    }

    public void RegisterButtonCallback()
    {
        GetAllButtons()
            .ForEach(button => button.RegisterCallback<ClickEvent>(OnCategoryButtonClicked));
    }

    public void UnregisterCallback()
    {
        GetAllButtons()
            .ForEach(button => button.RegisterCallback<ClickEvent>(OnCategoryButtonClicked));
    }

    private void OnCategoryButtonClicked(ClickEvent evt)
    {
        VisualElement clickedButton = evt.currentTarget as VisualElement;
        if (!ButtonIsCurrentlySelected(clickedButton))
        {
            GetAllButtons()
                .Where(button => button != clickedButton && ButtonIsCurrentlySelected(button))
                .ForEach(UnselectButton);

            SelectButton(clickedButton);
        }
    }

    private void SelectButton(VisualElement button)
    {
        button.RemoveFromClassList(unselectedButtonClassName);
        FindMarker(button).ForEach(marker =>
        {
            marker.RemoveFromClassList(unselectedMarkerClassName);
            marker.AddToClassList(selectedMarkerClassName);
        });
        button.AddToClassList(selectedButtonClassName);
    }

    private void UnselectButton(VisualElement button)
    {
        button.RemoveFromClassList(selectedButtonClassName);
        FindMarker(button).ForEach(marker =>
        {
            marker.RemoveFromClassList(selectedMarkerClassName);
            marker.AddToClassList(unselectedMarkerClassName);
        });
        button.AddToClassList(unselectedButtonClassName);
    }

    private UQueryBuilder<VisualElement> FindMarker(VisualElement button)
    {
        return button.name switch
        {
            allButtonName => root.Query<VisualElement>(className: "marker"),
            publicButtonName => root.Query<VisualElement>(className: "public-marker"),
            hospitalButtonName => root.Query<VisualElement>(className: "hospital-marker"),
            cameraButtonName => root.Query<VisualElement>(className: "camera-marker"),
        };
    }

    private UQueryBuilder<VisualElement> GetAllButtons()
    {
        return root.Query<VisualElement>(className: buttonClassName);
    }

    private bool ButtonIsCurrentlySelected(VisualElement button)
    {
        return button.ClassListContains(selectedButtonClassName);
    }
}