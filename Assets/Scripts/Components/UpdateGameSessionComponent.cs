using System;
using UnityEngine;
using UnityEngine.Events;

public class UpdateGameSessionComponent : MonoBehaviour
{
    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private bool _stoneDoorStatus;
    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }

    
    public void UpdatePlayerNewPosition()  
    {
        _session.playerData.playerPositionAfterLoadingScene = _newPosition;
    }

    public void UpdateStoneDoorStatus()
    {
        _session.level1Data.stateDoorStone = _stoneDoorStatus;
    }
}
