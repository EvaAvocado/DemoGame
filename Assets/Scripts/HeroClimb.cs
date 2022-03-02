using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroClimb : MonoBehaviour
{
    private BoxCollider2D _collider;
    void Start()
    {
        _collider = GetComponents<BoxCollider2D>()[0];
    }

    public void CheckBoxTrigger(bool status)
    {
        _collider.enabled = status;
    }

}
