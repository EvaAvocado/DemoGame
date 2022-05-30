using System;
using UnityEngine;
using UnityEngine.Events;

public class StayCollisionComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private Cooldown _cooldown;
    [SerializeField] private GameObjectChange _actionStay;


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            if (_cooldown.IsReady)
            {
                _actionStay?.Invoke(other.gameObject);
                _cooldown.Reset();
            }
        }
    }

    [Serializable]
    public class GameObjectChange : UnityEvent<GameObject>
    {
        
    }
}