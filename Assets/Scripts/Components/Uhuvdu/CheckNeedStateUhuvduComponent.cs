using UnityEngine;
using UnityEngine.Events;

public class CheckNeedStateUhuvduComponent : MonoBehaviour
{
    [SerializeField] private Level1Data.ColorLevel _needColorLevel;
    [SerializeField] private UnityEvent _action;

    private GameSession _session;
    
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        CheckNeedStateUhuvdu();
    }

    public void CheckNeedStateUhuvdu()
    {
        if (_session.level1Data.stateColorLevel == _needColorLevel)
        {
            _action?.Invoke();
        }
    }
}