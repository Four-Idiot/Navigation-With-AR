using System.Linq;
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
        foreach (var styleSheet in uiTree.stylesheets)
        {
           uiInstance.styleSheets.Add(styleSheet); 
        }
        uiInstance.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        uiInstance.style.opacity = new StyleFloat(0f);
    }

    public virtual void Show()
    {
        uiInstance.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        uiInstance.style.opacity = new StyleFloat(1f);
        uiDocument.rootVisualElement.Add(uiInstance);
    }
    public virtual void Hide()
    {
        uiInstance.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        uiInstance.style.opacity = new StyleFloat(0f);
        uiDocument.rootVisualElement.Clear();
    }
}