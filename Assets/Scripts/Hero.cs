using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _currentSpeed;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;
    private bool _isGrounded;

    [SerializeField] private float _interactionRadius;
    private Collider2D[] _interactionResult = new Collider2D[1];
    [SerializeField] private LayerMask _interactionLayer;

    [SerializeField] private SpawnComponent _particleRun;
    [SerializeField] private SpawnComponent _particleJump;

    [SerializeField] private Transform _additionalPosition;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunningKey = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
    
    private float _direction;
    private bool _allowSecondJump;
    
    private int _points;

    private bool _horizontalMove;
    private bool _windy;
    private bool _windDirectionRight;

    [SerializeField] bool _bloxMoveX;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentSpeed = _speed;
        _points = 0;
        _allowSecondJump = true;
        _horizontalMove = true;
        _windy = false;
        _windDirectionRight = true;
        _bloxMoveX = false;
    }

    private void Update()
    {
        _isGrounded = IsGrounded();
        if (_isGrounded) _allowSecondJump = true;
    }

    private void FixedUpdate()
    {
        Windy(_windDirectionRight);
        if (!_bloxMoveX)
        {
            Run();
            Flip();
        }
    }

    private void LateUpdate()
    {
        if (!_bloxMoveX)
        {
            Animations();
        }
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
            _rb.velocity = new Vector2(_direction * _currentSpeed, _rb.velocity.y);
        }
    }
    
    private void Flip()
    {
        if (_direction > 0)
        { 
            transform.localScale = Vector3.one;
        }
        else if (_direction < 0)
        { 
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            SpawnParticleJump();

        } else if (!_isGrounded && _allowSecondJump)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _allowSecondJump = false;
            SpawnParticleJump();
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
    public void Rebound()
    {
        _horizontalMove = false;
        StartCoroutine(TimerToDelayRebound());
        _horizontalMove = true;
        
    }
    
    IEnumerator TimerToDelayRebound()
    {
        var dir = _direction > 0 ? Vector2.left : Vector2.right;
        _rb.AddForce (Vector2.up  * _jumpForce + dir * _jumpForce * 0.25f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
    }

    public void SpawnParticleRun()
    {
        _particleRun.Spawn();
    }
    
    private void SpawnParticleJump()
    {
        StartCoroutine(TimerToSpawnParticleJump());
    }
    
    IEnumerator TimerToSpawnParticleJump()
    {
        for (int i = 0; i < 10; i++)
        {
            _particleJump.Spawn();
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void Windy(bool windDirectionRight)
    {
        if (_windy && windDirectionRight)
        {
            if (_direction > 0)
            {
                _currentSpeed = 1.35f * _speed; 
            }
            else if (_direction < 0)
            {
                _currentSpeed = 0.65f * _speed; 
            }
        }
        
        if(_windy && !windDirectionRight)
        {
            if (_direction < 0)
            {
                _currentSpeed = 1.35f * _speed; 
            }
            else if (_direction > 0)
            {
                _currentSpeed = 0.65f * _speed; 
            }
        }
    }

    public void SetWindy(bool status)
    {
        _windy = status;
        if (!_windy)
        {
            _currentSpeed = _speed;
        }
    }

    public void SetWindDirectionRight(bool status)
    {
        _windDirectionRight = status;
    }

    public void Climb()
    {
        transform.position = new Vector3(_additionalPosition.position.x, _additionalPosition.position.y,
            transform.position.z);
    }

    public void StartAnimClimb()
    {
        _bloxMoveX = true;
        _rb.velocity = Vector2.zero;
        _animator.Play("climb");
    }
}
