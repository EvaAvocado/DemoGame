using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    [SerializeField] private UnityEvent _actionIfOpen;
    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        StartCoroutine("TimerToCheckStateChest");
    }

    IEnumerator TimerToCheckStateChest()
    {
        yield return new WaitForSeconds(0.001f);
        if (_session.level1Data.stateChest)
        {
            _actionIfOpen?.Invoke();
        }
    }

    public void Action()
    {
        if (_session.level1Data.stateKey)
        {
            _actionIfOpen?.Invoke();
        }
    }
}
