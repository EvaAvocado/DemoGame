using System;
using UnityEngine;

public class SwitchComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _state;
    [SerializeField] private string _animationKey;

    private void Awake()
    {
        _animator.SetBool(_animationKey, _state);
    }

    public void Switch()
    {
        _state = !_state;
        _animator.SetBool(_animationKey, _state);
    }
}
