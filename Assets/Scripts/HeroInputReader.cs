using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero _hero;

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _hero.SetDirectionHorizontal(direction);
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        bool pressSpace = context.ReadValue<float>() > 0;
        _hero.SetPressSpace(pressSpace);
    }
}
