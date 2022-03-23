using UnityEngine;

public class EndLevelToLever : MonoBehaviour
{
    private Animator _animator;
    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _animator = GetComponent<Animator>();
        
        if (_session.level1Data.stateEndLevel)
        {
            _animator.Play("open");
        }
        else if (!_session.level1Data.stateEndLevel)
        {
            _animator.Play("close");
        }
    }
}