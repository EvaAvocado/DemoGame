using UnityEngine;

public class GameSession : MonoBehaviour
{
    public PlayerData playerData;
    public Level1Data level1Data;
    
    private void Awake()
    {
        if (IsSessionExit())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private bool IsSessionExit()
    {
        var sessions = FindObjectsOfType<GameSession>();
        foreach (var gameSession in sessions)
        {
            if (gameSession != this)
            {
                return true;
            }
        }
        return false;
    }

}