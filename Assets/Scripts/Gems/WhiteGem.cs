using UnityEngine;

public class WhiteGem : MonoBehaviour
{
    private GameSession _session;
    
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        transform.gameObject.SetActive(_session.level1Data.stateWhiteGem);
    }
    
}
