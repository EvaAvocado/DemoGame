using UnityEngine;

public class HealthChangeComponent : MonoBehaviour
{
    [SerializeField] private int _hpDelta;

    public void SetHpDelta(int delta)
    {
        _hpDelta = delta;
    }
    
    public void ApplyDamage(GameObject gameObject)
    {
        var target = gameObject.GetComponent<HealthComponent>();
        if (target != null)
        {
            target.ApplyDamage(_hpDelta);
        }
    }
    
    public void RestoreHealth(GameObject gameObject)
    {
        var target = gameObject.GetComponent<HealthComponent>();
        if (target != null)
        {
            target.RestoreHealth(_hpDelta);
        }
    }
}
