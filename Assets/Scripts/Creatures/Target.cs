using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _directionHorizontal;
    private float _directionVertical;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveHorizontal();
        MoveVertical();
    }

    public void SetDirectionHorizontal(float direction)
    {
        _directionHorizontal = direction;
    }
    
    private void MoveHorizontal()
    {
        _rb.velocity = new Vector2(_directionHorizontal * _speed, _rb.velocity.y);
    }
    
    public void SetDirectionVertical(float direction)
    {
        _directionVertical = direction;
    }

    private void MoveVertical()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, -_directionVertical * _speed);
    }
}
