using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;

    public const string MIXER_MUSIC = "MusicVolume";


    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    
}
