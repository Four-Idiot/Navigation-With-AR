using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DocentAudioManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    //public GameObject trackedPrefab;
   // private GameObject prefabGameObject;
    public Slider audioSlider;

    private void Awake()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
       // prefabGameObject = Instantiate(trackedPrefab);
       // prefabGameObject.SetActive(false);
    }

    private void Update()
    {
        foreach (var audioSource in audioSources.Values)
        {
            if (audioSource.isPlaying)
            {
                audioSlider.maxValue = audioSource.clip.length;
                audioSlider.value = audioSource.time;
            }
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if(trackedImage.trackingState == TrackingState.Tracking)
            {
                PlayAudio(trackedImage);
                ShowPrefab(trackedImage);
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                PlayAudio(trackedImage);
                ShowPrefab(trackedImage);
            }
            else
            {
                if (audioSources.ContainsKey(trackedImage.referenceImage.name))
                {
                    audioSources[trackedImage.referenceImage.name].Stop();
                }
                HidePrefab(trackedImage);
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (audioSources.ContainsKey(trackedImage.referenceImage.name))
            {
                audioSources[trackedImage.referenceImage.name].Stop();
            }
            HidePrefab(trackedImage);
        }
    }

    private void PlayAudio(ARTrackedImage trackedImage)
    {
        if (!audioSources.ContainsKey(trackedImage.referenceImage.name))
        {
            var newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = Resources.Load<AudioClip>(trackedImage.referenceImage.name);
            audioSources.Add(trackedImage.referenceImage.name, newAudioSource);
        }

        if (!audioSources[trackedImage.referenceImage.name].isPlaying)
        {
            audioSources[trackedImage.referenceImage.name].Play();
        }
    }

    private void ShowPrefab(ARTrackedImage trackedImage)
    {
        /*if (!trackedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            var newPrefab = Instantiate(Resources.Load<GameObject>(trackedImage.referenceImage.name));
            trackedPrefabs.Add(trackedImage.referenceImage.name, newPrefab);
        }*/
        //prefabGameObject.transform.position = trackedImage.transform.position;
       // prefabGameObject.SetActive(true);

        // trackedPrefabs[trackedImage.referenceImage.name].SetActive(true);
    }

    private void HidePrefab(ARTrackedImage trackedImage)
    {
        /*if (trackedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            trackedPrefabs[trackedImage.referenceImage.name].SetActive(false);
        }*/
        //prefabGameObject.SetActive(false);
    }
}
