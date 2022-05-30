using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UhuvduUIComponent : MonoBehaviour
{ 
    private Transform _parent;
    private Transform[] _children;
    private Image[] _uhuvdus = new Image[3];
    private GameSession _session;

    private void Awake()
    {
        _parent = transform;
        _children = GetChildren();
        for (int i = 0; i < 3; i++)
        {
            _uhuvdus[i] = _children[i].gameObject.GetComponent<Image>();
        }
    }

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        
        CheckUhuvdus();
        UpdateUhuvduState();
    }
    
    private Transform[] GetChildren()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in _parent) children.Add(child);
        return children.ToArray();
    }
    
    
    private void CheckUhuvdus()
    {
        switch (_session.playerData.uhuwdu)
        {
            case 3:
                _uhuvdus[0].enabled = true;
                _uhuvdus[1].enabled = true;
                _uhuvdus[2].enabled = true;
                break;
            case 2:
                _uhuvdus[0].enabled = true;
                _uhuvdus[1].enabled = true;
                _uhuvdus[2].enabled = false;
                break;
            case 1:
                _uhuvdus[0].enabled = true;
                _uhuvdus[1].enabled = false;
                _uhuvdus[2].enabled = false;
                break;
            case 0:
                _uhuvdus[0].enabled = false;
                _uhuvdus[1].enabled = false;
                _uhuvdus[2].enabled = false;
                break;
        }
    }
    
    
    public void SubUhuvdu()
    {
        _session.playerData.uhuwdu = _session.playerData.uhuwdu - 1;

        if (_session.playerData.uhuwdu == -1)
        {
            _session.playerData.uhuwdu = 3;
        }
    }

    public void UpdateUhuvduState()
    {
        if (_uhuvdus[0].enabled == true && _uhuvdus[1].enabled == true && _uhuvdus[2].enabled == true)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Green;
        }
        else if (_uhuvdus[0].enabled == true && _uhuvdus[1].enabled == true && _uhuvdus[2].enabled == false)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Pink;
        }
        else if (_uhuvdus[0].enabled == true && _uhuvdus[1].enabled == false && _uhuvdus[2].enabled == false)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Blue;
        }
        else if (_uhuvdus[0].enabled == false && _uhuvdus[1].enabled == false && _uhuvdus[2].enabled == false)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Red;
        }
    }

    
}






