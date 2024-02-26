using UnityEngine.UIElements;

public class PoiListEntryController
{
    private Label nameLabel;
    private Label descriptionLabel;

    public void SetVisualElement(VisualElement visualElement)
    {
        nameLabel = visualElement.Q<Label>("name");
        descriptionLabel = visualElement.Q<Label>("description");
    }

    public void SetPoiData(Marker poi)
    {
        nameLabel.text = poi.Name;
        descriptionLabel.text = poi.Address;
    }
}