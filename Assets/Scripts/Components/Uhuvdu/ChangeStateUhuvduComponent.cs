using System.Collections.Generic;
using UnityEngine;

public class ChangeStateUhuvduComponent : MonoBehaviour
{
    [SerializeField] private Cooldown _checkUhuvduCooldown;
    
    private Transform _parent;
    private Transform[] _children;
    private GameSession _session;

    private void Awake()
    {
        _parent = transform;
        _children = GetChildren();
    }

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        if (_checkUhuvduCooldown.IsReady)
        {
            CheckUhuvduState();
            _checkUhuvduCooldown.Reset();
        }
    }

    public void CheckUhuvduState()
    {
        _session = FindObjectOfType<GameSession>();
        if (_session.level1Data.stateColorLevel == Level1Data.ColorLevel.Green)
        {
            OnUhuvduState(0);
        }
        else if (_session.level1Data.stateColorLevel == Level1Data.ColorLevel.Pink)
        {
            OnUhuvduState(1);
        }
        else if (_session.level1Data.stateColorLevel == Level1Data.ColorLevel.Blue)
        {
            OnUhuvduState(2);
        }
        else if (_session.level1Data.stateColorLevel == Level1Data.ColorLevel.Red)
        {
            OnUhuvduState(3); 
        }
    }

    private Transform[] GetChildren()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in _parent) children.Add(child);
        return children.ToArray();
    }

    public void OnUhuvduState(int state)
    {
        foreach (var child in _children)
        {
            child.gameObject.SetActive(false);
        }
        _children[state].gameObject.SetActive(true);
    }
}