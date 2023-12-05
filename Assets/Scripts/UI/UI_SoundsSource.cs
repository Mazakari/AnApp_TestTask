using UnityEngine;

public class UI_SoundsSource : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable() =>
        UI_ButtonClick.OnClickSound += PlayClickSound;

    private void OnDisable() =>
        UI_ButtonClick.OnClickSound -= PlayClickSound;

    private void PlayClickSound(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
