using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPhotozoneView : UIView
{

    [SerializeField]
    private ARSession arSession;

    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private GameObject[] placeablePrefabs;

    private Dictionary<string, GameObject> spawnedObjects = new();

    #region Elements

    private VisualElement captureButton;
    private VisualElement recordButton;
    private VisualElement stopRecordButton;

    #endregion

    public override void Show()
    {
        base.Show();
        arSession.gameObject.SetActive(true);
        trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
        captureButton.RegisterCallback<ClickEvent>(OnCaptureButtonClicked);
        recordButton.RegisterCallback<ClickEvent>(OnRecordButtonClicked);
        stopRecordButton.RegisterCallback<ClickEvent>(OnRecordStopClicked);
    }

    public override void Hide()
    {
        base.Hide();
        arSession.gameObject.SetActive(false);
        trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
        captureButton.UnregisterCallback<ClickEvent>(OnCaptureButtonClicked);
        recordButton.UnregisterCallback<ClickEvent>(OnRecordButtonClicked);
        stopRecordButton.UnregisterCallback<ClickEvent>(OnRecordStopClicked);
    }

    protected override void Awake()
    {
        base.Awake();
        captureButton = uiInstance.Q<VisualElement>("capture-button");
        recordButton = uiInstance.Q<VisualElement>("record-button");
        stopRecordButton = uiInstance.Q<VisualElement>("stop-record-button");
        foreach (GameObject obj in placeablePrefabs)
        {
            GameObject newObject = Instantiate(obj);
            newObject.name = obj.name;
            newObject.SetActive(false);

            spawnedObjects.Add(newObject.name, newObject);
        }
    }

    private void OnRecordButtonClicked(ClickEvent evt)
    {
        Debug.Log("Onclick");
        if (arSession.subsystem is ARCoreSessionSubsystem subsystem)
        {
            Debug.Log("inside if");
            RecordExample(subsystem, "");
        }
    }

    private void OnRecordStopClicked(ClickEvent evt)
    {
        Debug.Log("Onclick stopRecording");
        if (arSession.subsystem is ARCoreSessionSubsystem subsystem)
        {
            if (subsystem.recordingStatus.Recording())
            {
                subsystem.StopRecording();
            }
        }
    }

    private void RecordExample(ARCoreSessionSubsystem subsystem, string mp4Path)
    {
        var session = subsystem.session;
        string filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mp4";
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        using (var config = new ArRecordingConfig(session))
        {
            config.SetMp4DatasetFilePath(session, filePath);
            config.SetRecordingRotation(session, 90);
            config.SetAutoStopOnPause(session, false);
            var status = subsystem.StartRecording(config);
            Debug.Log($"StartRecording to {config.GetMp4DatasetFilePath(session)} => {status}");
        }
    }

    private async void OnCaptureButtonClicked(ClickEvent evt)
    {
        int width = Screen.width;
        int height = Screen.height;
        Camera camera = Camera.main;

        camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));

        //스크린샷 찍을 RenderTexture 생성 !
        RenderTexture rt = new RenderTexture(width, height, 32);
        camera.targetTexture = rt;

        var currentRt = RenderTexture.active;
        RenderTexture.active = rt;

        camera.Render();
        camera.cullingMask = -1;

        var image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        camera.targetTexture = null;
        RenderTexture.active = currentRt;

        byte[] bytes = image.EncodeToPNG();
        string filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, filename);

        await File.WriteAllBytesAsync(filePath, bytes);
        Debug.Log($"photo save\nsaved at: {filePath}");
        Destroy(rt);
        Destroy(image);
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateSpwanObject(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateSpwanObject(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            string referenceImageName = trackedImage.referenceImage.name;
            spawnedObjects[referenceImageName].SetActive(false);
        }
    }

    private void UpdateSpwanObject(ARTrackedImage trackedImage)
    {
        string referenceImageName = trackedImage.referenceImage.name;

        spawnedObjects[referenceImageName].transform.position = trackedImage.transform.position;
        spawnedObjects[referenceImageName].transform.rotation = trackedImage.transform.rotation;

        // Check the tracking state of the image
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            spawnedObjects[referenceImageName].SetActive(true);
        }
        else
        {
            spawnedObjects[referenceImageName].SetActive(false);
        }
    }
}