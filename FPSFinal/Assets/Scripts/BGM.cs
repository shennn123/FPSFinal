using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeControl : MonoBehaviour
{
    public AudioSource musicSource;   // 拖入 Audio Source
    public Slider volumeSlider;       // 拖入 Slider

    void Start()
    {
        if (volumeSlider != null && musicSource != null)
        {
            volumeSlider.value = musicSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}