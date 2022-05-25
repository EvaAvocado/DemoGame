using UnityEngine;
using UnityEngine.Events;

public class ActionIfHeroInCertainPositionOnStart : MonoBehaviour
{
    [SerializeField] private double _positionX;
    [SerializeField] private double _positionY;
    [SerializeField] private UnityEvent _action;
    

    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        if (_session.playerData.playerPositionAfterLoadingScene.x == _positionX && _session.playerData.playerPositionAfterLoadingScene.y == _positionY)
        {
            _action?.Invoke();
        }
    }
}