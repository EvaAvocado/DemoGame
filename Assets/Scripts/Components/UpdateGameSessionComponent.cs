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
    [SerializeField] private bool _stateMenu;
    
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
        _session.levelData.stateWhiteGem = _stateWhiteGem;
    }
    
    public void UpdateStateEndLevel()
    {
        _session.levelData.stateEndLevel = _stateEndLevel;
    }

    public void UpdateStoneDoorStatus()
    {
        _session.levelData.stateDoorStone = _stoneDoorStatus;
    }
    
    public void UpdateStateChest()
    {
        _session.levelData.stateChest = _stateChest;
    }
    
    public void UpdateStateMenu()
    {
        _session.playerData.menuStatus = _stateMenu;
    }
}
