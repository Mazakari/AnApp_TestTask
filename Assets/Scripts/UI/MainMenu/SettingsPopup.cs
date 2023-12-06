using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private GameObject _musicOnImage;
    [SerializeField] private GameObject _musicOffImage;

    [Header("Sounds")]
    [SerializeField] private Toggle _soundsToggle;
    [SerializeField] private GameObject _soundsOnImage;
    [SerializeField] private GameObject _soundsOffImage;

    private ISaveLoadService _saveLoadService;
    private VolumeControl _volumeControl;

    public static event Action OnSettingsSaved;

    private void OnEnable()
    {
        _volumeControl = FindObjectOfType<VolumeControl>();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        SubscribeSlidersValueChangeCallbacks();

        LoadAudioSettings();
        InitAudioTogglesImages();
    }

    private void OnDisable() => 
        UnsubscribeSlidersValueChangeCallbacks();

    public void SaveAudioSettings()
    {
        _saveLoadService.SaveProgress();
        OnSettingsSaved?.Invoke();
    }

    private void SubscribeSlidersValueChangeCallbacks()
    {
        _musicToggle.onValueChanged.AddListener(HandleMusicToggle);
        _soundsToggle.onValueChanged.AddListener(HandleSoundsToggle);
    }
    private void UnsubscribeSlidersValueChangeCallbacks()
    {
        _musicToggle.onValueChanged.RemoveAllListeners();
        _soundsToggle.onValueChanged.RemoveAllListeners();
    }

    private void HandleMusicToggle(bool value)
    {
        HandleMusicToggleImages(value);

        _volumeControl.HandleMusicToggleChanged(value, _musicToggle);
    }
    private void HandleMusicToggleImages(bool value)
    {
        try
        {
            TurnMusicImageOn(value);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void TurnMusicImageOn(bool value)
    {
        _musicOffImage.SetActive(!value);
        _musicOnImage.SetActive(value);
    }

    private void HandleSoundsToggle(bool value)
    {
        HandleSoundsToggleImages(value);

        _volumeControl.HandleSoundsToggleChanged(value, _soundsToggle);
    }
    private void HandleSoundsToggleImages(bool value)
    {
        try
        {
            TurnSoundsImageOn(value);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void TurnSoundsImageOn(bool value)
    {
        _soundsOffImage.SetActive(!value);
        _soundsOnImage.SetActive(value);
    }

    private void LoadAudioSettings()
    {
        _musicToggle.isOn = _volumeControl.MusicOn;
        _soundsToggle.isOn = _volumeControl.SoundsOn;
    }

    private void InitAudioTogglesImages()
    {
        TurnMusicImageOn(_musicToggle.isOn);
        TurnSoundsImageOn(_soundsToggle.isOn);
    }
}
