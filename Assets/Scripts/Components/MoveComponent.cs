using UnityEngine;
using UnityEngine.Serialization;

public class MoveComponent : MonoBehaviour
{
    [FormerlySerializedAs("_horizontal")] [SerializeField] private bool _isHorizontal;
    [FormerlySerializedAs("_vertical")] [SerializeField] private bool _isVertical;
    [SerializeField] private float _horizontalOffset;
    [SerializeField] private float _verticalOffset;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedToRandom;
    [SerializeField] private bool _loop;

    private Rigidbody2D _rb;

    private bool _moveHorizontalRight;
    private bool _moveVerticalTop;

    private float _startPositionHorizontal;
    private float _startPositionVertical;

    private bool _stopMove;
    
    public void NegativeOffset()
    {
        _verticalOffset = -_verticalOffset;
        _horizontalOffset = -_horizontalOffset;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _moveHorizontalRight = SelectionMoveTo(_horizontalOffset);
        _moveVerticalTop = SelectionMoveTo(_verticalOffset);

        _startPositionHorizontal = transform.position.x;
        _startPositionVertical = transform.position.y;

        _stopMove = false;

        if (_horizontalOffset == 0)
        {
            _isHorizontal = false;
        }

        if (_verticalOffset == 0)
        {
            _isVertical = false;
        }

        if (_speedToRandom != 0)
        {
            _speed = _speed < _speedToRandom ? Random.Range(_speed, _speedToRandom) : Random.Range(_speedToRandom, _speed);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (!_stopMove)
        {
            if (_isHorizontal)
            {
                CheckEndPositionHorizontal();
                MoveHorizontal();
            }

            if (_isVertical)
            {
                CheckEndPositionVertical();
                MoveVertical();
            }
        }

        if (_stopMove)
        {
            _rb.velocity = new Vector2(0, 0);
        }
    }

    private bool SelectionMoveTo(float offset)
    {
        if (offset > 0)
        {
            return true;
        }
        else
        {
            return false;
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

    private void CheckEndPositionHorizontal()
    {
        if (_horizontalOffset > 0)
        {
            if (_moveHorizontalRight && transform.position.x > (_startPositionHorizontal + _horizontalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveHorizontalRight = !_moveHorizontalRight;
            }
            else if (!_moveHorizontalRight && transform.position.x < (_startPositionHorizontal - _horizontalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveHorizontalRight = !_moveHorizontalRight;
            }
        }
        else if (_horizontalOffset < 0)
        {
            if (_moveHorizontalRight && transform.position.x > (_startPositionHorizontal - _horizontalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveHorizontalRight = !_moveHorizontalRight;
            }
            else if (!_moveHorizontalRight && transform.position.x < (_startPositionHorizontal + _horizontalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveHorizontalRight = !_moveHorizontalRight;
            }
        }
    }

    private void CheckEndPositionVertical()
    {
        if (_verticalOffset > 0)
        {
            if (_moveVerticalTop && transform.position.y > (_startPositionVertical + _verticalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveVerticalTop = !_moveVerticalTop;
            }
            else if (!_moveVerticalTop && transform.position.y < (_startPositionVertical - _verticalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveVerticalTop = !_moveVerticalTop;
            }
        }
        else if (_verticalOffset < 0)
        {
            if (_moveVerticalTop && transform.position.y > (_startPositionVertical - _verticalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveVerticalTop = !_moveVerticalTop;
            }
            else if (!_moveVerticalTop && transform.position.y < (_startPositionVertical + _verticalOffset))
            {
                if (!_loop)
                {
                    _stopMove = true;
                }

                _moveVerticalTop = !_moveVerticalTop;
            }
        }
    }
    [ContextMenu("NextHorizontalPosition")]
    public void NextHorizontalPosition()
    {
        _isHorizontal = true;
        _startPositionHorizontal = transform.position.x;
        _stopMove = false;
    }
    
    [ContextMenu("NextVerticalPosition")]
    public void NextVerticalPosition()
    {
        _isVertical = true;
        _startPositionVertical = transform.position.y;
        _stopMove = false;
    }

    public void SetHorizontal(bool status)
    {
        _isHorizontal = status;
    }
    
    public void SetVertical(bool status)
    {
        _isVertical = status;
    }
}