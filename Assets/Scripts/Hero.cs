using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sprite;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;
    private bool _isGrounded;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunningKey = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
    
    private float _direction;
    public int points;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        points = 0;
    }

    private void FixedUpdate()
    {
        _isGrounded = IsGrounded();
        Run();
        Flip();
        Animation();
    }

    private void Animation()
    {
        _animator.SetBool(IsRunningKey, _direction != 0);
        _animator.SetFloat(VerticalVelocityKey, _rb.velocity.y);
        _animator.SetBool(IsGroundKey, _isGrounded);
    }
    
    //Задача направления: направо - 1, налево - (-1)
    public void SetDirectionHorizontal(float direction)
    {
        _direction = direction;
    }

    private void Run()
    {
        _rb.velocity = new Vector2(_direction * _speed, _rb.velocity.y);
    }

    private void Flip()
    {
        if (_direction > 0)
        {
            _sprite.flipX = false;
        }
        else if (_direction < 0)
        {
            _sprite.flipX = true;  
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    public void JumpBroke()
    {
        if (_rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _groundLayer);
        return hit.collider != null;
    }
}
