using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody2D _rb;

    [SerializeField] private LayerMask _groundLayer;
    
    private float _direction;
    private float _directionVertical;
    private bool _pressSpace;

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Run();
        //Jump();
        VerticalMove();
    }
    
    //Задача направления: направо - 1, налево - (-1)
    public void SetDirectionHorizontal(float direction)
    {
        _direction = direction;
    }

    private void Run()
    {
        if (_direction != 0)
        {
            var delta = _direction * _speed * Time.deltaTime;
            var newXPosition = transform.position.x + delta;
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
        //if (_direction != 0) _rb.velocity = new Vector2(_direction * _speed, _rb.velocity.y);
    }

    public void SetPressSpace(bool pressSpace)
    {
        _pressSpace = pressSpace;
    }

    private void Jump()
    {
        if(_pressSpace) _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
    
    
     public void SetDirectionVertical(float direction)
        {
            _directionVertical = direction;
        }

     private void VerticalMove()
        {
            if (_directionVertical != 0)
            {
                var delta = _directionVertical * _speed * Time.deltaTime;
                var newYPosition = transform.position.y + delta;
                transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
            }
        }
}
