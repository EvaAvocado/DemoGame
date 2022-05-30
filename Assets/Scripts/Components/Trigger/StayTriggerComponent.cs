using System;
using UnityEngine;
using UnityEngine.Events;

public class StayTriggerComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private GameObjectChange _actionStay;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _actionStay?.Invoke(other.gameObject);
        }
    }

    [Serializable]
    public class GameObjectChange : UnityEvent<GameObject>
    {
        
    }
}
