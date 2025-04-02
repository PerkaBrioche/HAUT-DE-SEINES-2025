using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    private AudioSource _audioSource;
    
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    
    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void Start()
    {
        if(masterVolumeSlider != null)
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master", 1);
        
        if(musicVolumeSlider != null)
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        
        if(sfxVolumeSlider != null)
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        
        if(audioMixer == null){return;}
        audioMixer.SetFloat("Master", GetDB( masterVolumeSlider.value));
        audioMixer.SetFloat("MusicVolume", GetDB( musicVolumeSlider.value));
        audioMixer.SetFloat("SFXVolume", GetDB( sfxVolumeSlider.value));
    }

    public void SetMasterVolume()
    {
        float dB = GetDB(masterVolumeSlider.value);
        audioMixer.SetFloat("Master", dB);
        PlayerPrefs.SetFloat("Master", masterVolumeSlider.value);
    }
    public void SetMusicVolume()
    {
        float dB = GetDB(musicVolumeSlider.value);
        audioMixer.SetFloat("MusicVolume", dB);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void SetSFXVolume()
    {
        float dB = GetDB(sfxVolumeSlider.value);
        audioMixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    public float GetDB(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1);
        return Mathf.Log10(value) * 20;
    }

    public enum SoundList // IN ORDER
    {
        jumOneWalk, //0
        jumTwoWalk,
        GMWalk,
        LitteGWalk,//3
        GMSmoke,
        GMSmokeEnd,//5
        GlitchHospital,
        TransitionHospital,
        TransitonHouse, //8
        ChracterDiseapearSmoke,
        AmbienceForest , //10
        
    }
    
    public void PlayButtonSound()
    {
    }

    public void PlaySoundIndex(int index)
    {
        if (index >= 0 && index < _audioClips.Count)
        {
            _audioSource.PlayOneShot(_audioClips[index]);
            print("Playing sound at index: " + index + " with name: " + _audioClips[index].name);
        }
    }
    public void PlaySound(SoundList sound)
    {
        int index = (int)sound;
        if (index >= 0 && index < _audioClips.Count)
        {
            _audioSource.PlayOneShot(_audioClips[index]);
        }
    }

}
