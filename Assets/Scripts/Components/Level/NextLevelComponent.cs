using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelComponent : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void NextScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
