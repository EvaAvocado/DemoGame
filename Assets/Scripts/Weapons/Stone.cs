using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private float _forceX;
    [SerializeField] private float _forceY;

    private Rigidbody2D _rb;
    private int _direction = 1;

    void Start()
    {
        _direction = transform.lossyScale.x > 0 ? 1 : -1;
        _rb = GetComponent<Rigidbody2D>();
        var force = new Vector2(_direction * _forceX, _forceY);
        _rb.AddForce(force, ForceMode2D.Impulse);
    }
}
