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
        uiInstance = uiTree.Instantiate();
        // for (int i = 0; i < uiInstance.styleSheets.count; i++)
        // {
        //     uiInstance.ElementAt(0).styleSheets.Add(uiInstance.styleSheets[i]);
        // }
    }

    public virtual void Show()
    {
        // uiDocument.rootVisualElement.Add(uiInstance.ElementAt(0));
        // uiInstance.styleSheets.Add(uiInstance.styleSheets[0]);
        uiDocument.rootVisualElement.Add(uiInstance);
    }
    public virtual void Hide()
    {
        uiDocument.rootVisualElement.Clear();
    }
}