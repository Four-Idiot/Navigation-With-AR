using UnityEngine.UIElements;

public class PoiListEntryController
{
    private VisualElement container;
    private Label nameLabel;
    private Label descriptionLabel;

    public void SetVisualElement(VisualElement visualElement)
    {
        nameLabel = visualElement.Q<Label>("name");
        descriptionLabel = visualElement.Q<Label>("description");
        container = visualElement.Q<VisualElement>("photozone-list-item");
    }

    public void SetPoiData(Marker poi)
    {
        nameLabel.text = poi.Name;
        descriptionLabel.text = poi.Address;
        container.RegisterCallback<ClickEvent>(OnItemClicked);
    }

    private void OnItemClicked(ClickEvent evt)
    {
        var detailView = UINavigation.Instance.Push(UIViewIndex.PHOTOZONE_DOCENT_DETAIL) as PhotoZoneDocentDetailView;
        detailView.Init(nameLabel.text, descriptionLabel.text);
    }
}