using UnityEngine;
using UnityEngine.Events;

public class StoneDoorWithPortal : MonoBehaviour
{
    [SerializeField] private UnityEvent _opened;
    private bool status;
    private GameSession _session;

    private void Awake()
    {
        _session = FindObjectOfType<GameSession>();
        status = _session.levelData.stateDoorStone;
        if(status) _opened?.Invoke();
    }
}
