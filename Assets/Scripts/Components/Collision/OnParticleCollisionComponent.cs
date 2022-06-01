using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnParticleCollisionComponent : MonoBehaviour
{
   [SerializeField] private List<string> _tags;
   [SerializeField] private GameObjectChange _action;
   
   private void OnParticleCollision(GameObject other)
   {
      foreach (var tag in _tags)
      {
         if (other.CompareTag(tag))
         {
            _action?.Invoke(other);
         } 
      }
   }
   
   [Serializable]
   public class GameObjectChange : UnityEvent<GameObject>
   {
        
   }
}
