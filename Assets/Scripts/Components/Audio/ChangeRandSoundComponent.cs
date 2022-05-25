using UnityEngine;

public class ChangeRandSoundComponent : MonoBehaviour
{
    [SerializeField] private string[] _soundsName;
        
    private PlaySoundsComponent _sounds;

    private void Awake()
    {
        _sounds = GetComponent<PlaySoundsComponent>();
    }

    public void PlayRandSound()
    {
        int randSound = Random.Range(0, _soundsName.Length);
        _sounds.Play(_soundsName[randSound]);
    }
}