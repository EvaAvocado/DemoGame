using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateUhuvduComponent : MonoBehaviour
{
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
        StartCoroutine(TimerToCheckUhuvduStateAfterStart());
    }
    
    IEnumerator TimerToCheckUhuvduStateAfterStart()
    {
        yield return new WaitForSeconds(0.01f);
        CheckUhuvduState();
    }

    public void CheckUhuvduState()
    {
        _session = FindObjectOfType<GameSession>();
        if (_session.levelData.stateColorLevel == LevelData.ColorLevel.Green)
        {
            OnUhuvduState(0);
        }
        else if (_session.levelData.stateColorLevel == LevelData.ColorLevel.Pink)
        {
            OnUhuvduState(1);
        }
        else if (_session.levelData.stateColorLevel == LevelData.ColorLevel.Blue)
        {
            OnUhuvduState(2);
        }
        else if (_session.levelData.stateColorLevel == LevelData.ColorLevel.Red)
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