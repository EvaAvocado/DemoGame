using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _directionHorizontal;
    private float _directionVertical;

    //Задача направления: направо - 1, налево - (-1)
    public void SetDirectionHorizontal(float direction)
    {
        _directionHorizontal = direction;
    }
    
    public void SetDirectionVertical(float direction)
    {
        _directionVertical = direction;
    }

    private void Update()
    {
        Run();
        VerticalMove();
    }

    private void Run()
    {
        if (_directionHorizontal != 0)
        {
            var delta = _directionHorizontal * _speed * Time.deltaTime;
            var newXPosition = transform.position.x + delta;
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
    }

    private void VerticalMove()
    {
        if (_directionVertical != 0)
        {
            var delta = _directionVertical * _speed * Time.deltaTime;
            var newYPosition = transform.position.y + delta;
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
    }
    
    
}
