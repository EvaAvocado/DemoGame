using UnityEngine;

public class HealthRestoreComponent : MonoBehaviour
{
    [SerializeField] private int _hpDelta;

    public void SetHpDelta(int delta)
    {
        _hpDelta = delta;
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
