using System;
using UnityEngine;

public class ShopItemSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _unlockItemSound;
    [SerializeField] private AudioClip _equipItemSound;

    public void PlayUnlockSound()
    {
        if (_audioSource)
        {
            if (_unlockItemSound)
            {
                _audioSource.Stop();
                _audioSource.clip = _unlockItemSound;
                _audioSource.Play();
            }
        }
    }

    public void PlayEquipSound()
    {
        if (_audioSource)
        {
            if (_equipItemSound)
            {
                _audioSource.Stop();
                _audioSource.clip = _equipItemSound;
                _audioSource.Play();
            }
        }
    }
}
