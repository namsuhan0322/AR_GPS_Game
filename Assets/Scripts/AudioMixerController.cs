using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider musicMasterSlider;
    [SerializeField] private Slider musicBGMSlider;
    [SerializeField] private Slider musicSFXSlider;

    private void Awake()
    {
        float masterVol = PlayerPrefs.GetFloat("Volume_Master", 1f);
        float bgmVol = PlayerPrefs.GetFloat("Volume_BGM", 1f);
        float sfxVol = PlayerPrefs.GetFloat("Volume_SFX", 1f);

        musicMasterSlider.value = masterVol;
        musicBGMSlider.value = bgmVol;
        musicSFXSlider.value = sfxVol;

        musicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicBGMSlider.onValueChanged.AddListener(SetBGMVolume);
        musicSFXSlider.onValueChanged.AddListener(SetSFXVolume);

        SetMasterVolume(masterVol);
        SetBGMVolume(bgmVol);
        SetSFXVolume(sfxVol);
    }

    private float GetDecibel(float volume)
    {
        return Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", GetDecibel(volume));
        PlayerPrefs.SetFloat("Volume_Master", volume);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", GetDecibel(volume));
        PlayerPrefs.SetFloat("Volume_BGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", GetDecibel(volume));
        PlayerPrefs.SetFloat("Volume_SFX", volume);
    }
}