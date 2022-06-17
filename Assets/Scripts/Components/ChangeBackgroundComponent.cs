using System.Collections;
using UnityEngine;

public class ChangeBackgroundComponent : MonoBehaviour
{
    private Camera _camera;
    private GameSession _session;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _session = FindObjectOfType<GameSession>();
        
        StartCoroutine(TimerTo—ooseBackground());
    }
    
    IEnumerator TimerTo—ooseBackground()
    {
        yield return new WaitForSeconds(0.001f);
        ChooseBackground();
    }

    private void ChooseBackground()
    {
        if (_session.levelData.stateColorLevel == LevelData.ColorLevel.Green || _session.levelData.stateColorLevel == LevelData.ColorLevel.Blue)
        {
            _camera.backgroundColor = new Color(0.49f, 0.64f, 0.78f, 1);
        }
        else if (_session.levelData.stateColorLevel == LevelData.ColorLevel.Pink || _session.levelData.stateColorLevel == LevelData.ColorLevel.Red)
        {
            _camera.backgroundColor = new Color(0.89f, 0.71f, 0.73f, 255);
        }
    }
}
