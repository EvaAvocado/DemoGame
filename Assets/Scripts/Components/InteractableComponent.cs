using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent _action;
    [SerializeField] private bool _enabled = true;

    public void Interact()
    {
        if (_enabled)
        {
            _action?.Invoke();   
        }
    }

    public void SetEnabled(bool status)
    {
        _enabled = status;
    }
}
