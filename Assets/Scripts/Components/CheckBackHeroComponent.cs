using UnityEngine;
using UnityEngine.Events;

public class CheckBackHeroComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent _action;
    
    private Hero _player;

    private bool _isBack;

    private void Awake()
    {
        _player = FindObjectOfType<Hero>();
    }

    public void CheckBackHero()
    {
        if (transform.position.x > _player.transform.position.x && _player.transform.localScale.x < 0 ||
            transform.position.x < _player.transform.position.x && _player.transform.localScale.x > 0)
        {
            _action?.Invoke();
        }
    }
}
