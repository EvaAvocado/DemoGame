using System;
using UnityEngine;
using UnityEngine.Events;

public class PlaySoundsComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;

    public void Play(string id)
    {
        foreach (var audioData in _sounds)
        {
            if (audioData.id != id) continue;

            _source.PlayOneShot(audioData.clip);
            break;
        }
    }

    public void Mute()
    {
        _source.mute = true;
    }


    public void UnMute()
    {
        _source.mute = false;
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string id => _id;
        public AudioClip clip => _clip;
    }
}