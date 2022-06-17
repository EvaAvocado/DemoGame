using UnityEngine;
using UnityEngine.Events;

public class MenuComponent : MonoBehaviour
{
    [SerializeField] private bool _menuStatus;
    [SerializeField] private UnityEvent _action;
    [SerializeField] private UnityEvent _actionIfNull;
    
    private GameSession _session;

    private void Start()
    {
        try
        {
            _session = FindObjectOfType<GameSession>();
        }
        catch (UnityException ex)
        {
            _session = null;
        }

        if (_session == null)
        {
            _actionIfNull?.Invoke();
        }
        else if (_session.playerData.menuStatus == _menuStatus)
        {
            _action?.Invoke();
        }
    }
}