using System;
using UnityEngine;
using UnityEngine.Events;

public class ExitCollisionComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private GameObjectChange _action;
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _action?.Invoke(other.gameObject);
        }
    }
    
    [Serializable]
    public class GameObjectChange : UnityEvent<GameObject>
    {
        
    }
}