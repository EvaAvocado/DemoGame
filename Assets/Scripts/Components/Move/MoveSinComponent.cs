using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSinComponent : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;
    [SerializeField] private bool _ampitudeDirectionBool;

    [SerializeField] private bool _directionToPlayer;

    private Rigidbody2D _rb;
    private int _direction;
    
    private float _originalY;
    private float _time;
    private int _amplitudeDirection;

    private Hero _player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Hero>();
    }

    private void Start()
    {
        _direction = transform.lossyScale.x > 0 ? 1 : -1;

        if (_directionToPlayer)
        {
            _direction = _player.transform.localScale.x > 0 ? 1 : -1;
        }

        _originalY = _rb.position.y;
        _amplitudeDirection = _ampitudeDirectionBool ? 1 : -1;
    }

    private void FixedUpdate()
    {

        var position = _rb.position;
        position.x += _direction * _speed;
        position.y = _originalY + Mathf.Sin(_frequency * _time) * _amplitudeDirection * _amplitude;
        _rb.MovePosition(position);
        _time += Time.fixedDeltaTime;
    }
}
