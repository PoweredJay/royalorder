using UnityEngine;

public class MusicClass : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) { 
            return;
        } else {
            _audioSource.Play();
        }
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
    public void ChangeVolume(float vol)
    {
        _audioSource.volume = vol;
    }
}