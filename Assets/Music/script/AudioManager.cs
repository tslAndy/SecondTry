using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds;
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "musiVolume";
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }
    void LoadVolume()     //Volume saved in volume settings
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
    }
}
