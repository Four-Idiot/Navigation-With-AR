using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhotozoneView : UIView
{
    private CameraService cameraService;

    private VisualElement captureButton;
    private VisualElement backButton;
    private VisualElement videoButton;

    protected override void Awake()
    {
        base.Awake();
        cameraService = Config.Instance.CameraService();
        captureButton = uiInstance.Q<Image>("capture-button");
    }
    
    public override void Show()
    {
        base.Show();
        captureButton.RegisterCallback<ClickEvent>(OnCaptureButtonClicked);
        backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        videoButton.RegisterCallback<ClickEvent>(OnVideoButtonClicked);
    }
    
    public override void Hide()
    {
        base.Hide();
        captureButton.UnregisterCallback<ClickEvent>(OnCaptureButtonClicked);
        backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
    }
    
    void OnBackButtonClicked(ClickEvent evt)
    {
        UINavigation.Instance.Pop();
    }

    void OnVideoButtonClicked(ClickEvent evt)
    {
        captureButton.UnregisterCallback<ClickEvent>(OnCaptureButtonClicked);
    }

    void OnVideRecordingbButtonClicked(ClickEvent evt)
    {
        //아직 미완성
    }
    
    void OnCaptureButtonClicked(ClickEvent evt)
    {
        RenderTexture result = cameraService.TakePhoto();
        Destroy(result);
    }
}