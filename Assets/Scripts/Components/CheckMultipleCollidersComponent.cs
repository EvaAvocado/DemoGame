using System;
using UnityEngine;
using UnityEngine.Events;

public class CheckMultipleCollidersComponent : MonoBehaviour
{
    [SerializeField] private int _requiredNumberCollisions;
    [SerializeField] private UnityEvent _action;
    private int _currentNumberCollisions;

    private void Update()
    {
        CheckNumberCollisions();
    }

    private void CheckNumberCollisions()
    {
        if (_currentNumberCollisions == _requiredNumberCollisions)
        {
            _action?.Invoke();
        }
    }

    public void EnterCollision()
    {
        _currentNumberCollisions++;
    }

    public void ExitCollision()
    {
        _currentNumberCollisions--;
    }
}
