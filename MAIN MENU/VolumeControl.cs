using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; // Reference to your UI Slider
    public AudioSource audioSource; // Reference to your Audio Source

    private void Start()
    {
        // Initialize the slider value to the current volume level
        volumeSlider.value = audioSource.volume;
    }

    public void SetVolume(float volume)
    {
        // Set the volume of the Audio Source
        audioSource.volume = volume;
    }
}