using System;
using UnityEngine;

namespace Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;
        public int damage => _damage;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            if (_session.levelData.stateDifficultyLevel == LevelData.DifficultyLevel.Easy)
            {
                _damage /= 2;
            }
            
            if (_session.levelData.stateDifficultyLevel == LevelData.DifficultyLevel.Hard)
            {
                _damage *= 2;
            }
        }

        public void SetDamage(int delta)
        {
            _damage = delta;
        }
    
        public void ApplyDamage(GameObject gameObject)
        {
            var target = gameObject.GetComponent<HealthComponent>();
            if (target != null)
            {
                target.ApplyDamage(_damage);
            }
        }
    }
    
}