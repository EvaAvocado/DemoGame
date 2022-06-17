using UnityEngine;

namespace Components
{
    public class SetDifficultyLevelComponent : MonoBehaviour
    {
        [SerializeField] private LevelData.DifficultyLevel _level;
        private GameSession _session;

        public void SetCurrentDifficultyLevel()
        {
            _session = FindObjectOfType<GameSession>();
            
            if (_session != null)
            {
                _session.levelData.stateDifficultyLevel = _level;
            }
        }
    }
}