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

    public void OnVertical(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _hero.SetDirectionVertical(direction);
    }
}
