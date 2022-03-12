using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponParticleCollision : MonoBehaviour
{
   [SerializeField] private string _tag;
   [SerializeField] private Weapon _weapon;
   
   private void OnParticleCollision(GameObject other)
   {
      if (other.CompareTag(_tag))
      {
         _weapon.ApplyRayDamage(other);
      }
   }
}
