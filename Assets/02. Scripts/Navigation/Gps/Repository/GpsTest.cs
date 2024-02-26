using System;
using UnityEngine;
using UnityEngine.Android;

public class GpsTest : MonoBehaviour
{
    public static string DebugMessage = "For Debug";
    
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 54;
        style.normal.textColor = Color.black;

        // GUI.Label(new Rect(5, 0, Screen.width, 20), $"{myLatitude},{myLongitude}", style);
        GUI.Label(new Rect(5, 0, Screen.width, 20), $"{DebugMessage}", style);
    }
}