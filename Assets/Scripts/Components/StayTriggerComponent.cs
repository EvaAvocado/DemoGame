using System;
using UnityEngine;
using UnityEngine.Events;

public class StayTriggerComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private UnityEvent _actionEnter;
    [SerializeField] private UnityEvent _actionExit;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _actionEnter?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _actionExit?.Invoke();
        }
    }
}
