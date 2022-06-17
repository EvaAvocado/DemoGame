using UnityEngine;
using UnityEngine.Serialization;

public class GameSession : MonoBehaviour
{
    public PlayerData playerData;
    [FormerlySerializedAs("level1Data")] public LevelData levelData;
    
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