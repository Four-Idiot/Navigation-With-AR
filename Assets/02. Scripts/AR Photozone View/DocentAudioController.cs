using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DocentAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
       audioSource.Play(); 
    }

    private void OnDisable()
    {
       audioSource.Stop(); 
    }
}