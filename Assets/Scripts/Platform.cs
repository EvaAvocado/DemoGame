using System;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private UnityEvent _action;

    private void Update()
    {
        if (_hero.IsPlatformAndDown())
        {
            _action?.Invoke();
        }
    }
}
