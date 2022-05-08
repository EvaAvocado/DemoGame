using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerComponent : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private bool _doAction = true;
    [SerializeField] private UnityEvent _action;
    private float _timeBeforeApplyAction;

    private void Awake()
    {
        _timeBeforeApplyAction = _time;
    }

    private void Update()
    {
        if (_doAction)
        {
            Timer(); 
        }
    }

    public void SetNegativeDoAction()
    {
        _doAction = !_doAction;
    }
    
    public void Timer()
    {
        _timeBeforeApplyAction -= Time.deltaTime;
        if (_timeBeforeApplyAction < 0)
        {
            _timeBeforeApplyAction = _time;
            _action?.Invoke();
        }
    }

   
}