using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private HealthChange _onSetMaxHealth;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private UnityEvent _onRestoreHealth;
    [SerializeField] private HealthChange _onChange;

    private int _maxHealth;

    private GameSession _session;

    private void Start()
    {
        StartCoroutine(TimerToSetMaxHealth());
    }

    IEnumerator TimerToSetMaxHealth()
    {
        yield return new WaitForSeconds(0.01f);
        _maxHealth = _health;
        _onSetMaxHealth?.Invoke(_maxHealth);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _onChange?.Invoke(_health);
        print("Health: " + _health);
        
        _onDamage?.Invoke();
        if (_health <= 0)
        {
            _onDie?.Invoke();
        }
    }

    public void RestoreHealth(int healthReplenishment)
    {
        _health += healthReplenishment;
        _onChange?.Invoke(_health);
        
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        _onRestoreHealth?.Invoke();
        print("Health: " + _health);
    }
    
    [Serializable]
    public class HealthChange : UnityEvent<int>
    {
        
    }
}
