using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 네비게이션 UI
/// </summary>
public class NavigationView : UIView
{
   [SerializeField]
   private VisualTreeAsset hospitalMarker;

   public override void Show()
   {
      base.Show();
      VisualElement marker = hospitalMarker.Instantiate().Children().First();
      marker.style.translate = new StyleTranslate(new Translate(new Length(180), new Length(400)));
      uiInstance.Q<VisualElement>("flat-map").Add(marker);
      Debug.Log($"position = {marker.transform.position.x} {marker.transform.position.y}");
   }
}