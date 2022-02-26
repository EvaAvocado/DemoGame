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

    [SerializeField] private float _interactionRadius;
    private Collider2D[] _interactionResult = new Collider2D[1];
    [SerializeField] private LayerMask _interactionLayer;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunningKey = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
    
    private float _direction;
    private bool _allowSecondJump;
    
    private int _points;

    private bool _horizontalMove;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _points = 0;
        _allowSecondJump = true;
        _horizontalMove = true;
    }

    private void Update()
    {
        _isGrounded = IsGrounded();
        if (_isGrounded) _allowSecondJump = true;
    }

    private void FixedUpdate()
    {
        Run();
        Flip();
        Animations();
    }

    private void Animations()
    {
        _animator.SetBool(IsRunningKey, _direction != 0);
        _animator.SetFloat(VerticalVelocityKey, _rb.velocity.y);
        _animator.SetBool(IsGroundKey, _isGrounded);
    }
    
    //Задача направления: направо - 1, налево - (-1)
    public void SetDirectionHorizontal(float direction)
    {
        _direction = direction;
        _horizontalMove = true;
    }

    public void Run()
    {
        if (_horizontalMove)
        {
            _rb.velocity = new Vector2(_direction * _speed, _rb.velocity.y);
        }
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
        } else if (!_isGrounded && _allowSecondJump)
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

    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);
        for (int i = 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void AddPoints(int count)
    {
        _points += count;
        print("Points: " + _points);
    }

    //Отскок
    public void Rebound(bool direction)
    {
        _horizontalMove = false;
        if (direction)
        {
            _rb.AddForce (Vector2.up  * _jumpForce + Vector2.left * _jumpForce * 0.25f, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce (Vector2.up  * _jumpForce + Vector2.right * _jumpForce * 0.25f, ForceMode2D.Impulse);
        }
    }
}
