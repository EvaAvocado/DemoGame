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
    public int _damage = 2;
    
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;
    private bool _isGrounded;
    
    [SerializeField] private LayerMask _platformLayer;
    private float _platformCheckRadius;
    private Vector3 _platformCheckPositionDelta;
    private bool _isPlatform;

    [SerializeField] private float _interactionRadius;
    private Collider2D[] _interactionResult = new Collider2D[1];
    [SerializeField] private LayerMask _interactionLayer;

    [SerializeField] private SpawnComponent _particleRun;
    [SerializeField] private SpawnComponent _particleJump;
    [SerializeField] private SpawnComponent _particleJumpDown;
    [SerializeField] private Transform _additionalPosition;
    
    [SerializeField] private float _jumpDownVelocity;
    
    [SerializeField] bool _bloxMoveX = false;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunningKey = Animator.StringToHash("is-running");
    private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
    
    private Rigidbody2D _rb;
    private Animator _animator;

    private float _directionHorizontal;
    private float _directionVertical;
    private bool _allowSecondJump = true;
    private bool _horizontalMove = true;
    
    private bool _windy = false;
    private bool _windDirectionRight = true;

    private GameSession _session;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        
        _currentSpeed = _speed;
        _platformCheckRadius = _groundCheckRadius;
        _platformCheckPositionDelta = _groundCheckPositionDelta;
        var health = GetComponent<HealthComponent>();
        

        transform.position = _session.playerData.playerPositionAfterLoadingScene;
    }

    private void Update()
    {
        _isGrounded = IsGrounded();
        _isPlatform = IsPlatform();
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

    public void SetMaxHealth(int maxHealth)
    {
        _session.playerData.maxHealth = maxHealth;
    }
    
    public void OnHealthChange(int currentHealth)
    {
        _session.playerData.currentHealth = currentHealth;
    }

    private void Animations()
    {
        _animator.SetBool(IsRunningKey, _directionHorizontal != 0);
        _animator.SetFloat(VerticalVelocityKey, _rb.velocity.y);
        _animator.SetBool(IsGroundKey, _isGrounded);
    }
    
    //Задача направления: направо - 1, налево - (-1)
    public void SetDirectionHorizontal(float direction)
    {
        _directionHorizontal = direction;
        _horizontalMove = true;
    }

    public void Run()
    {
        if (_horizontalMove)
        {
            _rb.velocity = new Vector2(_directionHorizontal * _currentSpeed, _rb.velocity.y);
        }
    }
    
    private void Flip()
    {
        if (_directionHorizontal > 0)
        { 
            transform.localScale = Vector3.one;
        }
        else if (_directionHorizontal < 0)
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

    //Отскок
    public void Rebound()
    {
        _horizontalMove = false;
        StartCoroutine(TimerToDelayRebound());
        _horizontalMove = true;
        
    }
    
    IEnumerator TimerToDelayRebound()
    {
        var dir = _directionHorizontal > 0 ? Vector2.left : Vector2.right;
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
            if (_directionHorizontal > 0)
            {
                _currentSpeed = 1.35f * _speed; 
            }
            else if (_directionHorizontal < 0)
            {
                _currentSpeed = 0.75f * _speed; 
            }
        }
        
        if(_windy && !windDirectionRight)
        {
            if (_directionHorizontal < 0)
            {
                _currentSpeed = 1.35f * _speed; 
            }
            else if (_directionHorizontal > 0)
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
    
    public void SetDirectionVertical(float direction)
    {
        _directionVertical = direction;
    }

    private bool IsPlatform()
    {
        var hit = Physics2D.CircleCast(transform.position + _platformCheckPositionDelta, _platformCheckRadius, Vector2.down, 0, _platformLayer);
        return hit.collider != null;
    }

    public bool IsPlatformAndDown()
    {
        if (_isPlatform && _directionVertical == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            var contact = other.contacts[0];
            if (contact.relativeVelocity.y >= _jumpDownVelocity)
            {
                _particleJumpDown.Spawn();  
            }
        }
    }
}
