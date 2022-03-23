using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        transform.gameObject.SetActive(_session.level1Data.stateEndLevel);
    }

    public void UpdateStateEndLevel()
    {
        _session = FindObjectOfType<GameSession>();
        transform.gameObject.SetActive(_session.level1Data.stateEndLevel);
    }
}