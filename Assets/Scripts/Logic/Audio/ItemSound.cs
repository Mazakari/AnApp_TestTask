using UnityEngine;

public class ItemSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _pitchSound = false;
    public void Play()
    {
        _audioSource.Stop();

        if (_audioSource.clip != null)
        {
            if (_pitchSound)
            {
                float rnd = Random.Range(0.5f, 1.5f);
                _audioSource.pitch = rnd;
            }

            _audioSource.Play();
        }
    }
}
