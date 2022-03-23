using System;
using System.Collections;
using UnityEngine;

public class Creature : MonoBehaviour
{       
        [Header("Params")]
        [SerializeField] private float _speed;
        [SerializeField] protected float JumpForce;
        [SerializeField] private int _damage = 2;
        [SerializeField] private float _jumpDownVelocity;
        [SerializeField] private int _countOfJumpParticles;
        [SerializeField] private bool _secondJumpEnabled = true;
       
        [Header("Checkers")]
        [SerializeField] private LayerCheckComponent _groundCheck;
        [SerializeField] protected SpawnListComponent Particles;
        
        protected float CurrentSpeed;
        protected Rigidbody2D Rb;
        protected Animator Animator;
        protected float DirectionHorizontal;
        protected bool IsGrounded;

        public bool isGrounded => IsGrounded;
        public Rigidbody2D rb => Rb;
        public int damage => _damage;
        public float currentSpeed => CurrentSpeed;
        public float speed => _speed;
        
        private bool _allowFirstJump = true;
        private bool _allowSecondJump = true;
        private bool _windy = false;
        private bool _windDirectionRight = true;

        private static readonly int IsGroundKey = Animator.StringToHash("is-not-ground");
        protected static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int IsHit = Animator.StringToHash("hit");
        protected static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        
        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            CurrentSpeed = _speed;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.isTouchingLayer;
            if (IsGrounded) _allowSecondJump = true;
            if (!IsGrounded) _allowFirstJump = true;
        }

        //Choose direction: to right - 1, to left - (-1)
        public void SetDirectionHorizontal(float direction)
        {
            DirectionHorizontal = direction;
        }

        protected virtual void FixedUpdate()
        {
            Windy(_windDirectionRight);
            Run();
            Flip();
        }

        protected virtual void LateUpdate()
        {
            Animations(); 
        }
        
        public void Run()
        {
            Rb.velocity = new Vector2(DirectionHorizontal * CurrentSpeed, Rb.velocity.y);
        }
        
        private void Flip()
        {
            if (DirectionHorizontal > 0)
            { 
                transform.localScale = Vector3.one;
            }
            else if (DirectionHorizontal < 0)
            { 
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
        protected virtual void Animations()
        {
            Animator.SetBool(IsRunningKey, DirectionHorizontal != 0);
            if (IsGrounded)
            {
                Animator.SetFloat(VerticalVelocityKey, 0);
            }
            else
            {
              Animator.SetFloat(VerticalVelocityKey, Rb.velocity.y);  
            }
        }

        public void Jump()
        {
            if (IsGrounded && _allowFirstJump)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, 0.0f); 
                Rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                SpawnParticleJump(_countOfJumpParticles);
                _allowFirstJump = false;

            }
            if (!IsGrounded && _allowSecondJump && _secondJumpEnabled)
            {
                Rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                SpawnParticleJump(_countOfJumpParticles);
                _allowSecondJump = false;
            }
            if (!IsGrounded)
            {
                Animator.SetTrigger(IsGroundKey);
            }
        }
        
        public void JumpBroke()
        {
            if (Rb.velocity.y > 0)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * 0.5f);
            }
            if (!IsGrounded)
            {
                Animator.SetTrigger(IsGroundKey);
            }
        }
        
        protected void SpawnParticleJump(int time)
        {
            StartCoroutine(TimerToSpawnParticleJump(time));
        }
    
        IEnumerator TimerToSpawnParticleJump(int time)
        {
            for (int i = 0; i < time; i++)
            {
                if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("climb"))
                {
                    Particles.Spawn("ParticleJump");  
                }
                yield return new WaitForSeconds(0.03f);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _jumpDownVelocity)
                {
                    StartCoroutine(TimerToDealyJumpDown());
                }
            }
        }

        IEnumerator TimerToDealyJumpDown()
        {
            yield return new WaitForSeconds(0.05f);
            Particles.Spawn("ParticleJumpDown");
        }

        public void Rebound()
        {
            StartCoroutine(TimerToDelayRebound());
        }
    
        IEnumerator TimerToDelayRebound()
        {
            var dir = DirectionHorizontal > 0 ? Vector2.left : Vector2.right;
            Rb.AddForce (Vector2.up  * JumpForce + dir * JumpForce * 0.25f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
        }

        public void TakeDamage()
        {
            Rebound();
            Animator.SetTrigger(IsHit);
        }
        
        private void Windy(bool windDirectionRight)
        {
            if (_windy && windDirectionRight)
            {
                if (DirectionHorizontal > 0)
                {
                    CurrentSpeed = 1.35f * _speed; 
                }
                else if (DirectionHorizontal < 0)
                {
                    CurrentSpeed = 0.75f * _speed; 
                }
            }
        
            if(_windy && !windDirectionRight)
            {
                if (DirectionHorizontal < 0)
                {
                    CurrentSpeed = 1.35f * _speed; 
                }
                else if (DirectionHorizontal > 0)
                {
                    CurrentSpeed = 0.65f * _speed; 
                }
            }
        }
        
        public void SetWindy(bool status)
        {
            _windy = status;
            if (!_windy)
            {
                CurrentSpeed = _speed;
            }
        }
        
        public void SetWindDirectionRight(bool status)
        {
            _windDirectionRight = status;
        }

        public void SetCurrentSpeed(float speed)
        {
            CurrentSpeed = speed;
        }
}