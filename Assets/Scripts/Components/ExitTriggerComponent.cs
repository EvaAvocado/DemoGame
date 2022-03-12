using System;
using UnityEngine;
using UnityEngine.Events;

public class ExitTriggerComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private UnityEvent _action;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _action?.Invoke();
        }
    }
}
