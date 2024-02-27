using UnityEngine.UIElements;

public class PoiListEntryController
{
    private VisualElement container;
    private Label nameLabel;
    private Label descriptionLabel;
    private PoiInfo poi;

    public void SetVisualElement(VisualElement visualElement)
    {
        nameLabel = visualElement.Q<Label>("name");
        descriptionLabel = visualElement.Q<Label>("description");
        container = visualElement.Q<VisualElement>("photozone-list-item");
    }

    public void SetPoiData(PoiInfo poi)
    {
        this.poi = poi;
        nameLabel.text = poi.Name;
        descriptionLabel.text = poi.Address;
        container.RegisterCallback<ClickEvent>(OnItemClicked);
    }

    private void OnItemClicked(ClickEvent evt)
    {
        if (poi.Type == MarkerType.PHOTOZONE)
        {
            var detailView = UINavigation.Instance.Push(UIViewIndex.PHOTOZONE_DETAIL) as PhotozoneDetailView;
            detailView.Init(poi);
        }
        else
        {
            var detailView = UINavigation.Instance.Push(UIViewIndex.DOCENT_DETAIL) as DocentDetailView;
            detailView.Init(poi);
        }
    }
}