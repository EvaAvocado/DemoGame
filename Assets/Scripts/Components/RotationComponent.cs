using System;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{
   [SerializeField] private float _degreeToRotation;

   private void FixedUpdate()
   {
      transform.Rotate(new Vector3(0, 0, _degreeToRotation) * Time.deltaTime);
   }
}
