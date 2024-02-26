using UnityEngine.UIElements;

public class TabMenuController
{
    private const string selectedTabClassName = "selected-tab-menu";
    private const string unselectedContentName = "unselected-tab-content";
    private const string tabClassName = "tab";
    private const string tabNameSuffix = "tab";
    private const string contentNameSuffix = "content";

    private readonly VisualElement root;

    public TabMenuController(VisualElement root)
    {
        this.root = root;
    }

    public void RegisterTabCallbacks()
    {
        var tabs = GetAllTabs();
        tabs.ForEach(tab => { tab.RegisterCallback<ClickEvent>(OnTabMenuButtonClicked); });
    }
    
    public void UnregisterTabCallbacks()
    {
        var tabs = GetAllTabs();
        tabs.ForEach(tab => { tab.UnregisterCallback<ClickEvent>(OnTabMenuButtonClicked); });
    }

    private void OnTabMenuButtonClicked(ClickEvent evt)
    {
        var clickedTabMenu = evt.currentTarget as VisualElement;
        if (!TabIsCurrentlySelected(clickedTabMenu))
        {
            GetAllTabs().Where(
                tab => tab != clickedTabMenu && TabIsCurrentlySelected(tab)
            ).ForEach(UnselectTab);
            SelectedTab(clickedTabMenu);
        }
    }

    private void SelectedTab(VisualElement tab)
    {
        tab.AddToClassList(selectedTabClassName);
        var content = FindContent(tab);
        content.RemoveFromClassList(unselectedContentName);
    }

    private void UnselectTab(VisualElement tab)
    {
        tab.RemoveFromClassList(selectedTabClassName);
        var content = FindContent(tab);
        content.AddToClassList(unselectedContentName);
    }

    private static string GenerateContentName(VisualElement tab) => tab.name.Replace(tabNameSuffix, contentNameSuffix);

    private VisualElement FindContent(VisualElement tab)
    {
        return root.Q(GenerateContentName(tab));
    }


    private static bool TabIsCurrentlySelected(VisualElement tab)
    {
        return tab.ClassListContains(selectedTabClassName);
    }

    private UQueryBuilder<VisualElement> GetAllTabs()
    {
        return root.Query<VisualElement>(className: tabClassName);
    }
}