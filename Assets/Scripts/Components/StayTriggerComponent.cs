using System;
using UnityEngine;
using UnityEngine.Events;

public class StayTriggerComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private GameObjectChange _actionWithGameObjectEnter;
    [SerializeField] private GameObjectChange _actionWithGameObjectExit;
    [SerializeField] private UnityEvent _actionEnter;
    [SerializeField] private UnityEvent _actionExit;
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _actionWithGameObjectEnter?.Invoke(other.gameObject);
            _actionEnter?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _actionWithGameObjectExit?.Invoke(other.gameObject);
            _actionExit?.Invoke();
        }
    }
    
    [Serializable]
    public class GameObjectChange : UnityEvent<GameObject>
    {
        
    }
}
