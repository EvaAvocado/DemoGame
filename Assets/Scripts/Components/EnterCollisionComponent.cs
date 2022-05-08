using System;
using UnityEngine;
using UnityEngine.Events;

public class EnterCollisionComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private GameObjectChange _actionWithGameObject;
    [SerializeField] private UnityEvent _action;

    private GameObject _gameObject;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _gameObject = other.gameObject;
            _actionWithGameObject?.Invoke(_gameObject);
            _action?.Invoke();
            print("MEOW");
        }
    }
    
    [Serializable]
    public class GameObjectChange : UnityEvent<GameObject>
    {
        
    }
}
