using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    private Animator _animator;

    private const string OPTIONS_ANIM_START = "Show";
    private const string OPTIONS_ANIM_HIDE = "Hide";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSound()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }

    public void ChangeButtonView(Button button)
    {
        Image _image = button.GetComponent<Image>();

        if(_image.color == new Color(1f, 1f, 1f, 1f))
            _image.color = new Color(1f, 1f, 1f, 0.25f);
        else
            _image.color = new Color(1f, 1f, 1f, 1f);
    }

    public void ShowAnimateOptionsPanel()
    {
        _animator.SetTrigger(OPTIONS_ANIM_START);
    }

    public void HideAnimateOptionsPanel()
    {
        _animator.SetTrigger(OPTIONS_ANIM_HIDE);
    }
}
