using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour, ISavedProgress
{
    [SerializeField] public AudioMixer _audioMixer;
    public AudioMixer AudioMixer => _audioMixer;

    private void Awake() =>
        DontDestroyOnLoad(gameObject);

    private string _musicVolumeParameter = "MusicVolume";
    private bool _disableMusicToggleEvent;

    public float MusicVolume { get; private set; }
    public bool MusicOn { get; private set; } = true;

    private string _soundsVolumeParameter = "SoundsVolume";
    private bool _disableSoundsToggleEvent = false;

    public float SoundsVolume { get; private set; }
    public bool SoundsOn { get; private set; } = false;

    private float _multiplier = 30f;

    //private float _curMusicVol;

    public void HandleMusicSliderValueChanged(float value, Slider slider, Toggle toggle)
    {
        value = Mathf.Clamp(value, 0.001f, slider.maxValue);
        MusicVolume = value;

        _audioMixer.SetFloat(_musicVolumeParameter, Mathf.Log10(value) * _multiplier);
        _disableMusicToggleEvent = true;
        toggle.isOn = slider.value > slider.minValue;
        MusicOn = toggle.isOn;
        _disableMusicToggleEvent = false;
    }

    public void HandleSoundsSliderValueChanged(float value, Slider slider, Toggle toggle)
    {
        value = Mathf.Clamp(value, 0.001f, slider.maxValue);
        SoundsVolume = value;

        _audioMixer.SetFloat(_soundsVolumeParameter, Mathf.Log10(value) * _multiplier);
        _disableSoundsToggleEvent = true;
        toggle.isOn = slider.value > slider.minValue;
        SoundsOn = toggle.isOn;
        _disableSoundsToggleEvent = false;
    }

    public void HandleMusicToggleChanged(bool value, Slider slider, Toggle toggle)
    {
        if (_disableMusicToggleEvent) return;

        if (!value)
        {
            MusicVolume = slider.value;
            slider.value = slider.minValue;
        }
        else
        {
            slider.value = MusicVolume;
        }

        MusicOn = toggle.isOn;
    }

    public void HandleSoundsToggleChanged(bool value, Slider slider, Toggle toggle)
    {
        if (_disableSoundsToggleEvent) return;

        if (!value)
        {
            SoundsVolume = slider.value;
            slider.value = slider.minValue;

        }
        else
        {
            slider.value = SoundsVolume;
        }

        SoundsOn = toggle.isOn;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.gameData.musicVolume = MusicVolume;
        progress.gameData.musicToggle = MusicOn;

        progress.gameData.soundVolume = SoundsVolume;
        progress.gameData.soundToggle = SoundsOn;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        MusicVolume = progress.gameData.musicVolume;
        MusicOn = progress.gameData.musicToggle;
        _audioMixer.SetFloat(_musicVolumeParameter, Mathf.Log10(MusicVolume) * _multiplier);

        SoundsVolume = progress.gameData.soundVolume;
        SoundsOn = progress.gameData.soundToggle;
        _audioMixer.SetFloat(_soundsVolumeParameter, Mathf.Log10(SoundsVolume) * _multiplier);

        if (!MusicOn)
        {
            MusicVolume = -80f;
            _audioMixer.SetFloat(_musicVolumeParameter, MusicVolume);
        }

        if (!SoundsOn)
        {
            SoundsVolume = -80f;
            _audioMixer.SetFloat(_soundsVolumeParameter, SoundsVolume);
        }
    }

    public void DisableAudio()
    {
        float muted = -80f;
        _audioMixer.SetFloat(_musicVolumeParameter, muted);
        _audioMixer.SetFloat(_soundsVolumeParameter, muted);
    }

    public void EnableAudio()
    {
        _audioMixer.SetFloat(_musicVolumeParameter, Mathf.Log10(MusicVolume) * _multiplier);
        _audioMixer.SetFloat(_soundsVolumeParameter, Mathf.Log10(SoundsVolume) * _multiplier);
    }
}
