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
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;
    
    private float _direction;
    private bool _pressSpace;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Run();
        Jump();
    }
    
    //������ �����������: ������� - 1, ������ - (-1)
    public void SetDirectionHorizontal(float direction)
    {
        _direction = direction;
    }

    private void Run()
    {
        if (_direction != 0) _rb.velocity = new Vector2(_direction * _speed, _rb.velocity.y);
    }

    public void SetPressSpace(bool pressSpace)
    {
        _pressSpace = pressSpace;
    }

    private void Jump()
    {
        if(_pressSpace && IsGrounded()) _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        else if (_rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _groundLayer);
        return hit.collider != null;
    }
}
