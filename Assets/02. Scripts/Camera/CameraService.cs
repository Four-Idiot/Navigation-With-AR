using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraService
{
    public RenderTexture TakePhoto()
    {
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        
        Camera camera = Camera.main;
        var renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        var texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture.active = renderTexture;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        byte[] pngImage = texture.EncodeToPNG();

        string fileName = DateTime.Now + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, pngImage);

        RenderTexture.active = null;
        camera.targetTexture = null;

        return renderTexture;
    }
}
