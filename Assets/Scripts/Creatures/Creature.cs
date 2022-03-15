using System;
using System.Collections;
using UnityEngine;

public class Creature : MonoBehaviour
{       
        [Header("Params")]
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private int _damage = 2;
        [SerializeField] private float _jumpDownVelocity;
       
        [Header("Checkers")]
        [SerializeField] private LayerCheckComponent _groundCheck;
        [SerializeField] protected SpawnListComponent Particles;

        public int damage => _damage;
        
        protected float CurrentSpeed;
        protected Rigidbody2D Rb;
        protected Animator Animator;
        protected float DirectionHorizontal;
        protected bool IsGrounded;
        
        private bool _allowSecondJump = true;
        private bool _windy = false;
        private bool _windDirectionRight = true;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
            
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
        
        private void Animations()
        {
            Animator.SetBool(IsRunningKey, DirectionHorizontal != 0);
            Animator.SetFloat(VerticalVelocityKey, Rb.velocity.y);
            Animator.SetBool(IsGroundKey, IsGrounded);
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                Rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                Particles.Spawn("ParticleJump");

            } else if (!IsGrounded && _allowSecondJump)
            {
                Rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _allowSecondJump = false;
                Particles.Spawn("ParticleJump");
            }
        }
        
        public void JumpBroke()
        {
            if (Rb.velocity.y > 0)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * 0.5f);
            }
        }
        
        private void SpawnParticleJump()
        {
            StartCoroutine(TimerToSpawnParticleJump());
        }
    
        IEnumerator TimerToSpawnParticleJump()
        {
            for (int i = 0; i < 10; i++)
            {
                Particles.Spawn("ParticleJump");
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
                    Particles.Spawn("ParticleJumpDown");
                }
            }
        }
        
        public void Rebound()
        {
            StartCoroutine(TimerToDelayRebound());

        }
    
        IEnumerator TimerToDelayRebound()
        {
            var dir = DirectionHorizontal > 0 ? Vector2.left : Vector2.right;
            Rb.AddForce (Vector2.up  * _jumpForce + dir * _jumpForce * 0.25f, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
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
}