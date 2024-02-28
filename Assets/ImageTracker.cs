using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] arObjectsToPlace; 

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager m_TrackedImageManager;

    private void Awake()
    {
        foreach (GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            arObjects.Add(arObject.name, newARObject);
        }
    }

    private void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        foreach (GameObject go in arObjects.Values)
        {
            go.SetActive(false);
        }

        arObjects[trackedImage.name].SetActive(true);
        arObjects[trackedImage.name].transform.position = trackedImage.transform.position;
        arObjects[trackedImage.name].transform.rotation = trackedImage.transform.rotation;
    }
}