using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;

    [SerializeField]
    AudioTrigger audioTrigger;

    private bool runOnce = true;

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 50));
    }

    public void SetVolume(float value)
    {
        if (runOnce)
        {
            value = 50;
            runOnce = false;
        }

        RefreshSlider(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value/100);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
        audioTrigger.SetVolume();
    }

    public void RefreshSlider(float value)
    {
        soundSlider.value = value;
    }
}
