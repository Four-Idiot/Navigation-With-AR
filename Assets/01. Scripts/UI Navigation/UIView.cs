using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument), typeof(UINavigation))]
public abstract class UIView : MonoBehaviour
{
    [field: SerializeField]
    public UIViewIndex ViewIndex { get; private set; }

    [SerializeField]
    protected VisualTreeAsset uiTree;

    protected VisualElement uiInstance;

    private UIDocument uiDocument;

    protected virtual void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        InitUIInstance();
    }
    private void InitUIInstance()
    {
        uiInstance = uiTree.Instantiate().Q<VisualElement>("container");
    }

    public virtual void Show()
    {
        uiDocument.rootVisualElement.Add(uiInstance);
    }
    public virtual void Hide()
    {
        uiDocument.rootVisualElement.Clear();
    }
}