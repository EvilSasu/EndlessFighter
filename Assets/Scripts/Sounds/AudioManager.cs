using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
            PlayMusic("MainMenuMusic");
    }

    public void PlayMusic(string name)
    {
        Sound _s = System.Array.Find(musicSounds, x => x.name == name);

        if (_s == null)
            Debug.Log("Sound Not Found");
        else
        {
            musicSource.clip = _s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound _s = System.Array.Find(sfxSounds, x => x.name == name);

        if (_s == null)
            Debug.Log("Sound Not Found");
        else
        {
            sfxSource.clip = _s.clip;
            sfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        if (musicSource != null)
            musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        if (sfxSource != null)
            sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        if(musicSource != null)
            musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = volume;
    }
}
