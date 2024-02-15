using System.Collections.Generic;
using UnityEngine.UIElements;

/// <summary>
/// 화면 전환할수 있음
/// </summary>
public class UINavigation : Singleton<UINavigation>
{
    private readonly Stack<UIViewIndex> history = new();

    private readonly Dictionary<UIViewIndex, UIView> views = new();

    private void Start()
    {
        Init();
    }

    // 테스트용
    private void Clear()
    {
        var uiDocument = GetComponent<UIDocument>();
        uiDocument.rootVisualElement.Clear();
    }
    
    private void Init()
    {
        Clear();
        foreach (var view in GetComponents<UIView>())
        {
            views[view.ViewIndex] = view;
        }
        Push(UIViewIndex.MAIN);
    }

    /// <summary>
    /// UIViewIndex에 해당하는 UIView로 이동
    /// </summary>
    /// <param name="uiViewIndex"></param>
    /// <returns></returns>
    public UIView Push(UIViewIndex uiViewIndex)
    {
        if (history.Count > 0)
        {
            UIViewIndex currentViewIndex = history.Peek();
            UIView currentView = views[currentViewIndex];
            currentView.Hide();
        }

        UIView newView = views[uiViewIndex];
        newView.Show();
        history.Push(uiViewIndex);
        return newView;
    }

    /// <summary>
    /// 이전 페이지로 돌아감
    /// </summary>
    /// <returns></returns>
    public UIView Pop()
    {
        // 현재 stack에 방문 기록이 1개밖에 없다면 main 페이지 리턴
        if (history.Count <= 1)
            return views[UIViewIndex.MAIN];

        UIViewIndex currentViewIndex = history.Pop();
        UIView currentView = views[currentViewIndex];
        currentView.Hide();

        UIViewIndex previousViewIndex = history.Peek();
        UIView previousView = views[previousViewIndex];
        previousView.Show();

        return previousView;
    }

}