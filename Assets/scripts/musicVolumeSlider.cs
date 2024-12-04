using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicVolumeSlider : MonoBehaviour
{
    public Slider musicVolumeSlider; 
    public TMP_Text musicVolumeText; 

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicVolumeSlider.value = savedVolume;
        UpdateVolumeText(savedVolume);

        musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float value)
    {
        MusicManager.GetInstance().SetMusicVolume(value);

        UpdateVolumeText(value);
    }

    void UpdateVolumeText(float value)
    {
        musicVolumeText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
