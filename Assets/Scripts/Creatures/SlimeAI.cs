using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SlimeAI : MonoBehaviour
{
    [Header("Params")] [SerializeField] private LayerCheckComponent _vision;
    [SerializeField] private Cooldown _attackCooldown;
    [SerializeField] private float _alarmDelay = 0.4f;
    [SerializeField] private float _missCooldown = 1f;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _treshold = 0.5f;

    [Header("Events")] [SerializeField] private OnDamage _makeDamage;

    public bool allowJump = true;
    public bool stopMoveX = false;

    private SpawnListComponent _particles;
    private Coroutine _current;
    private Vector3 _target;
    private Creature _creature;

    private bool firstAgrToHero = true;
    private bool _timerToNoise;
    private bool _isNoise;
    private bool _isNoiseComplete;
    private int _destinationPointIndex;

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
    }

    private void Start()
    {
        StartState(DoPatrol());
    }

    private void FixedUpdate()
    {
        if (stopMoveX)
        {
            _creature.SetDirectionHorizontal(0);
        }
    }

    private IEnumerator DoPatrol()
    {
        while (true)
        {
            if (IsOnPoint())
            {
                _destinationPointIndex = (int) Mathf.Repeat(_destinationPointIndex + 1, _points.Length);
            }

            var direction = _points[_destinationPointIndex].position - transform.position;
            if (allowJump)
            {
                _creature.SetDirectionHorizontal(direction.normalized.x);
                _creature.Jump();
            }

            yield return null; 
        }
        
    }

    private bool IsOnPoint()
    {
        return (_points[_destinationPointIndex].position - transform.position).magnitude < _treshold;
    }

    public void OnHeroIsVision(GameObject go)
    {
        if (firstAgrToHero)
        {
            firstAgrToHero = false;
            _creature.SetCurrentSpeed(_creature.speed);

            if (!_isNoise)
            {
                _target = go.transform.position;
                _creature.SetCurrentSpeed(_creature.currentSpeed * 2f);
                StartState(AgrToHero());
            }  
        }
    }

    public void UpdateHeroPosition(GameObject go)
    {
        if (!_isNoise)
        {
            _target = go.transform.position;
        }

        if (_isNoiseComplete)
        {
            firstAgrToHero = true;
            _isNoiseComplete = false;
            OnHeroIsVision(go);
        }
    }

    private IEnumerator AgrToHero()
    {
        yield return new WaitForSeconds(_alarmDelay * 2);
        _particles.SpawnWithoutLossyScale("ParticleExclamation");
        yield return new WaitForSeconds(_alarmDelay);
        firstAgrToHero = true;
        StartState(GoToHero());
    }

    private IEnumerator GoToHero()
    {
        while (_vision.isTouchingLayer)
        {
            SetDirectionToTarget();
            yield return null;
        }

        _creature.SetCurrentSpeed(_creature.speed);

        yield return new WaitForSeconds(_missCooldown * 2);
        _particles.SpawnWithoutLossyScale("ParticleMiss");
        yield return new WaitForSeconds(_missCooldown);
        StartState(DoPatrol());
    }

    private void SetDirectionToTarget()
    {
        var direction = _target - transform.position;
        if (allowJump)
        {
            _creature.SetDirectionHorizontal(direction.normalized.x);
            _creature.Jump();
        }
    }

    private void StartState(IEnumerator coroutine)
    {
        _creature.SetDirectionHorizontal(Vector2.zero.x);

        if (_current != null)
        {
            StopCoroutine(_current);
        }

        _current = StartCoroutine(coroutine);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_attackCooldown.IsReady)
            {
                _makeDamage?.Invoke(_creature.damage);
                _attackCooldown.Reset();
            }
        }
    }

    public void OnDie()
    {
        if (_current != null)
        {
            StopCoroutine(_current);
        }
    }

    public void OnNoiseIsVision(GameObject go)
    {
        _creature.SetCurrentSpeed(_creature.speed);
        _isNoise = true;
        _isNoiseComplete = false;

        _target = go.transform.position;

        _creature.SetCurrentSpeed(_creature.currentSpeed * 2f);
        StartState(AgrToNoise());
    }

    private IEnumerator AgrToNoise()
    {
        yield return new WaitForSeconds(_alarmDelay * 2);
        _particles.SpawnWithoutLossyScale("ParticleExclamation");
        yield return new WaitForSeconds(_alarmDelay);
        StartState(GoToNoise());
    }

    private IEnumerator GoToNoise()
    {
        _timerToNoise = false;
        StartCoroutine(TimerToFindNoise());

        while (!_isNoiseComplete)
        {
            SetDirectionToTarget();

            if (Math.Abs(_target.x - gameObject.transform.position.x) <= 0.5 || _timerToNoise)
            {
                yield return new WaitForSeconds(_missCooldown * 2);
                _particles.SpawnWithoutLossyScale("ParticleMiss");
                yield return new WaitForSeconds(_missCooldown);
                StartState(DoPatrol());
                
                _timerToNoise = false;
                _isNoise = false;
                _isNoiseComplete = true;
            }

            yield return null;
        }
    }

    private IEnumerator TimerToFindNoise()
    {
        yield return new WaitForSeconds(5);
        _timerToNoise = true;
        _isNoiseComplete = true;
    }

    [Serializable]
    public class OnDamage : UnityEvent<int>
    {
    }
}