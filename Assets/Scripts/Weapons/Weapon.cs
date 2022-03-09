using System;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private GemItem[] _gemItems;
    
    [SerializeField] private ParticleSystem _rayParticle;
    [SerializeField] private LayerMask _targetLayer;
    
    private SpriteRenderer _spriteRenderer;

    private float _timeBeforeApplyRay = 0;
    public bool attackPhase = false;
    private Gem currentGem;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (currentGem)
        {
            case Gem.White:
                if (_timeBeforeApplyRay >= 0)
                {
                    _timeBeforeApplyRay -= Time.deltaTime;
                }
                if (attackPhase)
                {
                    ApplyRayParticle();
                }
                break;
            default:
                break;
        }
    }

    public void SetPhase(float direction)
    {
        if (direction < 0)
        {
            attackPhase = true;
        }
        else if (direction <= 0)
        {
            attackPhase = false;
        }
    }

    private void ApplyRayParticle()
    {
        _rayParticle.Play();
    }

    public void ApplyRayDamage(GameObject target)
    {
        if (_timeBeforeApplyRay < 0)
        {
            _timeBeforeApplyRay = 0.5f;
            var enemy = target.GetComponent<HealthComponent>();
            if (enemy != null)
            {
                enemy.ApplyDamage(_hero._damage);
            }
        }
    }

    private void ShowGemSprite(Gem gem)
    {
        foreach (var gemItem in _gemItems)
        {
            if (gemItem.gem == gem)
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = gemItem.sprite;
            }
        }
    }

    public void SetCurrentGem(string enumName)
    {
        Enum.TryParse<Gem>(enumName, out currentGem);
        ShowGemSprite(currentGem);
    }

    public enum Gem
    {
        Nothing,
        White,
        Grey,
        Red,
        Yellow,
        Pink,
        Blue,
        Green,
        Rainbow
    }
}

[Serializable]
public class GemItem
{
    public Weapon.Gem gem;
    public Sprite sprite;
    public Buff buff;
    public Debuff debuff;
}

[Serializable]
public class Buff
{
    public string name;
    public UnityEvent action;
}

[Serializable]
public class Debuff
{
    public string name;
    public Collider2D range;
    public UnityEvent action;
}