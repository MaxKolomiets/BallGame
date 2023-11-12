using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType { 
Win, Lose, BallThrow, BallExplotion
}

public class AudioController : MonoBehaviour
{
    public static AudioController instance = null;
    private AudioSource _audioSource = new();
    [SerializeField] private List<AudioClip> _win = new();
    [SerializeField] private List<AudioClip> _lose = new();
    [SerializeField] private List<AudioClip> _ballThrow = new();
    [SerializeField] private List<AudioClip> _ballExplotion = new();
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }
    private AudioClip GetRandomClip(List<AudioClip> clips) {
        return clips[Random.Range(0, clips.Count)];
    }

    public void PlaySound(AudioType type) {
        switch (type)
        {
            case AudioType.Win:
                _audioSource.PlayOneShot(GetRandomClip(_win));
                break;
            case AudioType.Lose:
                _audioSource.PlayOneShot(GetRandomClip(_lose));
                break;
            case AudioType.BallThrow:
                _audioSource.PlayOneShot(GetRandomClip(_ballThrow));
                break;
            case AudioType.BallExplotion:
                _audioSource.PlayOneShot(GetRandomClip(_ballExplotion));
                break;
            default:
                break;
        }
    }
}
