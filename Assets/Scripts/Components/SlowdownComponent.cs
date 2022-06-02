using System.Collections;
using UnityEngine;

public class SlowdownComponent : MonoBehaviour
{
    [SerializeField] private float _slowdownPercentage;
    [SerializeField] private float _cooldownSlowdown;

    public void GOSlowdown(GameObject go)
    {
        go.GetComponent<Creature>().currentSpeed = go.GetComponent<Creature>().speed * _slowdownPercentage;
        StartCoroutine(TimerToStopSlowdown(go.GetComponent<Creature>()));
    }
    
    
    IEnumerator TimerToStopSlowdown(Creature creature)
    {
        yield return new WaitForSeconds(_cooldownSlowdown);
        creature.currentSpeed = creature.speed;
    }
}
