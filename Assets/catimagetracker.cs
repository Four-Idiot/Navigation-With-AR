using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARImageTracking : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject prefabToInstantiate; // 생성할 Prefab

    private Dictionary<string, GameObject> prefabInstances = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // 새로 트래킹 된 이미지에 대해
        foreach (var trackedImage in eventArgs.added)
        {
            if (prefabToInstantiate != null)
            {
                var instance = Instantiate(prefabToInstantiate, trackedImage.transform.position, trackedImage.transform.rotation);
                prefabInstances.Add(trackedImage.referenceImage.name, instance);
            }
        }

        // 업데이트 된 이미지에 대해
        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                if (prefabInstances.ContainsKey(trackedImage.referenceImage.name))
                {
                    var instance = prefabInstances[trackedImage.referenceImage.name];
                    instance.transform.position = trackedImage.transform.position;
                    instance.transform.rotation = trackedImage.transform.rotation;
                    instance.SetActive(true);
                }
            }
            else // 이미지가 더 이상 추적되지 않을 때
            {
                if (prefabInstances.ContainsKey(trackedImage.referenceImage.name))
                {
                    var instance = prefabInstances[trackedImage.referenceImage.name];
                    instance.SetActive(false);
                }
            }
        }

        // 제거된 이미지에 대해
        foreach (var trackedImage in eventArgs.removed)
        {
            if (prefabInstances.ContainsKey(trackedImage.referenceImage.name))
            {
                var instance = prefabInstances[trackedImage.referenceImage.name];
                Destroy(instance);
                prefabInstances.Remove(trackedImage.referenceImage.name);
            }
        }
    }
}
