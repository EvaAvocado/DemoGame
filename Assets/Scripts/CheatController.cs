using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{
    private string _currentInput;
    [SerializeField] private float _inputTimeToLive;
    [SerializeField] private CheatItem[] _cheats;
    private float _inputTime;
    private void Awake()
    {
        //Подписка на прослушивание метода OnTextInput с клавиатуры
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnTextInput(char inputChar)
    {
        _currentInput += inputChar;
        _inputTime = _inputTimeToLive;
        FindCheats();
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void FindCheats()
    {
        foreach (var cheatItem in _cheats)
        {
            if (_currentInput.Contains(cheatItem.name))
            {
                cheatItem.action?.Invoke();
                _currentInput = string.Empty;
            }
        }
    }

    private void Update()
    {
        if (_inputTime <= 0)
        {
            _currentInput = string.Empty;
        }
        else
        {
            _inputTime -= Time.deltaTime;
        }
    }
}
[Serializable]
public class CheatItem
{
    public string name;
    public UnityEvent action;
}
