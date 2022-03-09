using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private UnityEvent _onRestoreHealth;

    private int _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
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
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        _onRestoreHealth?.Invoke();
        print("Health: " + _health);
    }
}
