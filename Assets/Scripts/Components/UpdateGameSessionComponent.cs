using System;
using UnityEngine;
using UnityEngine.Events;

public class UpdateGameSessionComponent : MonoBehaviour
{
    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private bool _stoneDoorStatus;
    [SerializeField] private bool _stateWhiteGem;
    [SerializeField] private bool _stateEndLevel; 
    [SerializeField] private bool _stateChest;
    
    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }
    
    public void UpdatePlayerNewPosition()  
    {
        _session.playerData.playerPositionAfterLoadingScene = _newPosition;
    }

    public void UpdateStateWhiteGem()
    {
        _session.level1Data.stateWhiteGem = _stateWhiteGem;
    }
    
    public void UpdateStateEndLevel()
    {
        _session.level1Data.stateEndLevel = _stateEndLevel;
    }

    public void UpdateStoneDoorStatus()
    {
        _session.level1Data.stateDoorStone = _stoneDoorStatus;
    }
    
    public void UpdateStateChest()
    {
        _session.level1Data.stateChest = _stateChest;
    }
}
