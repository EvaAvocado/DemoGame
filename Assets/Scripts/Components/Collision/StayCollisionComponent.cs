using System;
using UnityEngine;
using UnityEngine.Events;

public class StayCollisionComponent : MonoBehaviour
{
    [SerializeField] private String _tag;
    [SerializeField] private Cooldown _cooldown;
    [SerializeField] private GameObjectChange _actionStay;
    [SerializeField] private GameObjectChange _actionExit;


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            print(2);
            if (_cooldown.IsReady)
            {
                print(1);
                _actionStay?.Invoke(other.gameObject);
                _cooldown.Reset();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
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