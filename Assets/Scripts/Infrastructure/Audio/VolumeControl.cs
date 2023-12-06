using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour, ISavedProgress
{
    [SerializeField] public AudioMixer _audioMixer;
    public AudioMixer AudioMixer => _audioMixer;

    public bool MusicOn { get; private set; } = true;
    public bool SoundsOn { get; private set; } = false;

    private string _musicVolumeParameter = Constants.MUSIC_VOLUME_PARAMETER;
    private float _musicVolume = Constants.DEFAULT_MUSIC_VOLUME;
   
    private string _soundsVolumeParameter = Constants.SOUNDS_VOLUME_PARAMETER;
    private float _soundsVolume = Constants.DEFAULT_SOUNDS_VOLUME;

    private float _multiplier = 30f;
    private float _mutedValue = -80f;

    private void Awake() =>
      DontDestroyOnLoad(gameObject);

    public void HandleMusicToggleChanged(bool value, Toggle toggle)
    {
        if (!value)
        {
            DisableMusic();
        }
        else
        {
            EnableMusic();
        }

        MusicOn = toggle.isOn;
    }
    public void HandleSoundsToggleChanged(bool value, Toggle toggle)
    {
        if (!value)
        {
            DisableSound();
        }
        else
        {
            EnableSound();
        }

        SoundsOn = toggle.isOn;
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.gameData.musicToggle = MusicOn;
        progress.gameData.soundToggle = SoundsOn;
    }
    public void LoadProgress(PlayerProgress progress)
    {
        LoadMusicData(progress);
        LoadSoundsData(progress);
    }

    private void LoadMusicData(PlayerProgress progress)
    {
        try
        {
            MusicOn = progress.gameData.musicToggle;

            _musicVolume = SetDefaultVolume(_musicVolume);
            if (!MusicOn)
            {
                _musicVolume = _mutedValue;
            }

            _audioMixer.SetFloat(_musicVolumeParameter, _musicVolume);
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    private void LoadSoundsData(PlayerProgress progress)
    {
        try
        {
            SoundsOn = progress.gameData.soundToggle;

            _soundsVolume = SetDefaultVolume(_soundsVolume);
            if (!SoundsOn)
            {
                _soundsVolume = _mutedValue;
            }

            _audioMixer.SetFloat(_soundsVolumeParameter, _soundsVolume);
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    private void DisableMusic() => 
        _audioMixer.SetFloat(_musicVolumeParameter, _mutedValue);
    private void DisableSound() => 
        _audioMixer.SetFloat(_soundsVolumeParameter, _mutedValue);

    private void EnableMusic()
    {
        _musicVolume = Constants.DEFAULT_MUSIC_VOLUME;
        _musicVolume = SetDefaultVolume(_musicVolume);

        _audioMixer.SetFloat(_musicVolumeParameter, _musicVolume);
    }

    private void EnableSound()
    {
        _soundsVolume = Constants.DEFAULT_SOUNDS_VOLUME;
        _soundsVolume = SetDefaultVolume(_soundsVolume);

        _audioMixer.SetFloat(_soundsVolumeParameter, _soundsVolume);
    }

    private float SetDefaultVolume(float incomingValue) =>
        Mathf.Log10(incomingValue) * _multiplier;
}
