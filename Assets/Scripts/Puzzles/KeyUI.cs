using UnityEngine;

public class KeyUI : MonoBehaviour
{
    private GameSession _session;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        gameObject.SetActive(_session.playerData.inventory.Count("Key") >= 1);
    }
}
