using UnityEngine;
using UnityEngine.UIElements;

public class MarkerView: MonoBehaviour
{
    [field:SerializeField]
    public VisualTreeAsset Uxml { get; private set; }

    [field:SerializeField]
    public MarkerType Type { get; private set; }
}