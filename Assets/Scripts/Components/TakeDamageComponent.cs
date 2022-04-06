using UnityEngine;
using UnityEngine.Events;

public class TakeDamageComponent : MonoBehaviour
{
    [SerializeField] private int _damage;
    
    public void TakeDamage(GameObject gameObject)
    {
        var gameObj = gameObject.GetComponent<HealthComponent>();
        if (gameObj != null)
        {
            gameObj.ApplyDamage(_damage);
        }
    }
}