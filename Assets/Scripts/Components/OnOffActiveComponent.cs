using UnityEngine;
using UnityEngine.Events;

public class OnOffActiveComponent : MonoBehaviour
{
    [SerializeField] private bool _status;
    [SerializeField] private UnityEvent _actionIfTrue;
    [SerializeField] private UnityEvent _actionIfFalse;

    public void SetActive()
    {
        gameObject.SetActive(_status);
        if (_status)
        {
            _actionIfTrue?.Invoke();
        }
        else if (!_status)
        {
            _actionIfFalse?.Invoke();
        }
    }
}
