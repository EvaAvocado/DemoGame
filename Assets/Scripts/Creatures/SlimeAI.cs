using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SlimeAI : MonoBehaviour
{
    [Header("Params")] [SerializeField] private LayerCheckComponent _vision;
    [SerializeField] private float _alarmDelay = 0.4f;
    [SerializeField] private float _missCooldown = 1f;
    [SerializeField] private float _timeBeforeApplyDamage = 0;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _treshold = 0.5f;

    [Header("Events")]
    [SerializeField] private OnDamage _makeDamage;

    public bool allowJump = true;
    public bool stopMoveX = false;
    
    private SpawnListComponent _particles;
    private Coroutine _current;
    private GameObject _target;
    private Creature _creature;
    
    private int _destinationPointIndex;
    private bool _patrooling = true;

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
    }

    private void Update()
    {
        if (_timeBeforeApplyDamage >= 0)
        {
            _timeBeforeApplyDamage -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_patrooling)
        {
          DoPatrol();  
        }

        if (stopMoveX)
        {
            _creature.SetDirectionHorizontal(0);
        }
    }

    private void DoPatrol()
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
    }

    private bool IsOnPoint()
    {
        return (_points[_destinationPointIndex].position - transform.position).magnitude < _treshold;
    }

    public void OnHeroIsVision(GameObject go)
    {
        _target = go;
        _creature.SetCurrentSpeed(_creature.currentSpeed * 2f);
        _patrooling = false;
        StartState(AgrToHero());
    }

    private IEnumerator AgrToHero()
    {
        yield return new WaitForSeconds(_alarmDelay);
        _particles.Spawn("ParticleExclamation"); 
        yield return new WaitForSeconds(_alarmDelay);
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
        
        _particles.Spawn("ParticleMiss");
        yield return new WaitForSeconds(_missCooldown);
        _patrooling = true;
    }

    private void SetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
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
            if (_timeBeforeApplyDamage < 0)
            {
                _timeBeforeApplyDamage = 0.5f;
                _makeDamage?.Invoke(_creature.damage);
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
    
    [Serializable]
    public class OnDamage : UnityEvent<int>
    {
        
    }
}