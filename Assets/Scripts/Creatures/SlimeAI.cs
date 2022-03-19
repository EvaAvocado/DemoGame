using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SlimeAI : MonoBehaviour
{
    [Header("Params")] [SerializeField] private LayerCheckComponent _vision;
    [SerializeField] private float _alarmDelay = 0.5f;
    [SerializeField] private float _missCooldown = 0.5f;
    [SerializeField] private float _makeDamageCooldown = 0.5f;
    
    [Header("Events")]
    [SerializeField] private OnDamage _makeDamage;

    private SpawnListComponent _particles;
    private Coroutine _current;
    private GameObject _target;
    private Creature _creature;
    private Patrol _patrol;

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
        _patrol = GetComponent<Patrol>();
    }

    private void Start()
    {
        StartCoroutine(_patrol.DoPatrol());
    }

    public void OnHeroIsVision(GameObject go)
    {
        _target = go;
        _creature.SetCurrentSpeed(_creature.currentSpeed * 2);
        StartState(AgrToHero());
    }

    private IEnumerator AgrToHero()
    {
        _particles.Spawn("Exclamation");
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
        _particles.Spawn("Miss");
        yield return new WaitForSeconds(_missCooldown);
    }

    private void SetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        _creature.SetDirectionHorizontal(direction.normalized.x);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MakeDamage());
        }
    }

    private IEnumerator MakeDamage()
    {
        _makeDamage?.Invoke(_creature.damage);
        yield return new WaitForSeconds(_makeDamageCooldown);
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