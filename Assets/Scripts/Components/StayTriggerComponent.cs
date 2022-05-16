using System;
using UnityEngine;
using UnityEngine.Events;

public class StayTriggerComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private GameObjectChange _actionEnter;
    [SerializeField] private GameObjectChange _actionExit;
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _actionEnter?.Invoke(other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _actionExit?.Invoke(other.gameObject);
        }
    }
    
    [Serializable]
    public class GameObjectChange : UnityEvent<GameObject>
    {
        
    }
}
