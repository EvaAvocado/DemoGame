using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private bool _horizontal;
    [SerializeField] private bool _vertical;
    [SerializeField] private float _horizontalOffset;
    [SerializeField] private float _verticalOffset;
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;

    private bool _moveHorizontalRight;
    private bool _moveVerticalTop;

    private float _startPositionHorizontal;
    private float _startPositionVertical;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (_horizontalOffset > 0)
        {
            _moveHorizontalRight = true;
        }
        if (_horizontalOffset < 0)
        {
            _moveHorizontalRight = false;
        }

        if (_verticalOffset > 0)
        {
            _moveVerticalTop = true;
        }
        if (_verticalOffset < 0)
        {
            _moveVerticalTop = false;
        }
        
        _startPositionHorizontal = transform.position.x;
        _startPositionVertical = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (_horizontal)
        {
            CheckEndPositionHorizontal();
            MoveHorizontal();
        }

        if (_vertical)
        {
            CheckEndPositionVerical();
            MoveVertical();  
        }
    }

    private void MoveHorizontal()
    {
        if (_moveHorizontalRight)
        {
            //moving right
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        } 
        else if (!_moveHorizontalRight)
        { 
            //moving left
            _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
        }
    }

    private void CheckEndPositionHorizontal()
    {
        if (_horizontalOffset > 0)
        {
            if (_moveHorizontalRight && transform.position.x > (_startPositionHorizontal + _horizontalOffset))
            {
                _moveHorizontalRight = !_moveHorizontalRight;
            } 
            else if (!_moveHorizontalRight && transform.position.x < (_startPositionHorizontal - _horizontalOffset))
            {
                _moveHorizontalRight = !_moveHorizontalRight; 
            }
        }
        else if (_horizontalOffset < 0)
        {
            if (_moveHorizontalRight && transform.position.x > (_startPositionHorizontal - _horizontalOffset))
            {
                _moveHorizontalRight = !_moveHorizontalRight;
            } 
            else if (!_moveHorizontalRight && transform.position.x < (_startPositionHorizontal + _horizontalOffset))
            {
                _moveHorizontalRight = !_moveHorizontalRight; 
            }
        }
        
    }
    
    private void MoveVertical()
    {
        if (_moveVerticalTop)
        {
            //moving top
            _rb.velocity = new Vector2(_rb.velocity.x, _speed);
        } 
        else if (!_moveVerticalTop)
        {
            //moving bottom
            _rb.velocity = new Vector2(_rb.velocity.x, -_speed);
        }
    }

    private void CheckEndPositionVerical()
    {
        if (_verticalOffset > 0)
        {
            if (_moveVerticalTop && transform.position.y > (_startPositionVertical + _verticalOffset))
            {
                _moveVerticalTop = !_moveVerticalTop;
            } 
            else if (!_moveVerticalTop && transform.position.y < (_startPositionVertical - _verticalOffset))
            {
                _moveVerticalTop = !_moveVerticalTop; 
            } 
        }
        else if (_verticalOffset < 0)
        {
            if (_moveVerticalTop && transform.position.y > (_startPositionVertical - _verticalOffset))
            {
                _moveVerticalTop = !_moveVerticalTop;
            } 
            else if (!_moveVerticalTop && transform.position.y < (_startPositionVertical + _verticalOffset))
            {
                _moveVerticalTop = !_moveVerticalTop; 
            } 
        }
        
    }
}

