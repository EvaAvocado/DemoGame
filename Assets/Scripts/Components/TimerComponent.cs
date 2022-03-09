using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerComponent : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private UnityEvent _action;
    private float _timeBeforeApplyAction;

    private void Awake()
    {
        _timeBeforeApplyAction = _time;
    }

    private void Update()
    {
        Timer();
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