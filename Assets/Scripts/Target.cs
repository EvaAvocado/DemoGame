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
        // if (_directionHorizontal != 0)
        // {
        //     var delta = _directionHorizontal * _speed * Time.deltaTime;
        //     var newXPos = transform.position.x + delta;
        //     transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
        // }
        _rb.velocity = new Vector2(_directionHorizontal * _speed, _rb.velocity.y);
    }
    
    public void SetDirectionVertical(float direction)
    {
        _directionVertical = direction;
    }

    private void MoveVertical()
    {
        // if (_directionVertical != 0)
        // {
        //     var delta = -_directionVertical * _speed * Time.deltaTime;
        //     var newYPos = transform.position.y + delta;
        //     transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
        // }
        _rb.velocity = new Vector2(_rb.velocity.x, -_directionVertical * _speed);
    }
}
