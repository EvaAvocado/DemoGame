using System;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private GemItem[] _gemItems;
    
    [SerializeField] private Cooldown _whiteGemCooldown;
    [SerializeField] private Cooldown _whiteRayCooldown;
    [SerializeField] private Cooldown _whiteRayCooldownOnMute;
    
    [SerializeField] private ParticleSystem _rayParticle;
    [SerializeField] private LayerMask _targetLayer;
    
    [SerializeField] private bool _attackPhase = false;
    
    public bool attackPhase => _attackPhase;

    private SpriteRenderer _spriteRenderer;
    private GameSession _session;
    private PlaySoundsComponent _sounds;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sounds = GetComponent<PlaySoundsComponent>();
    }

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        switch (_session.playerData.currentGem)
        {
            case Gem.White:
               
                ShowGemSprite(_session.playerData.currentGem);
                if (attackPhase)
                {
                    _sounds.UnMute();
                    ApplyRayParticle();
                }

                if (!attackPhase)
                {
                    if (_whiteRayCooldownOnMute.IsReady)
                    {
                        _sounds.Mute();
                        _whiteRayCooldownOnMute.Reset();
                    }
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
            _attackPhase = true;
        }
        else if (direction <= 0)
        {
            _attackPhase = false;
        }
    }

    private void ApplyRayParticle()
    {
        _rayParticle.Play();
        
        if (_whiteRayCooldown.IsReady)
        {
            _sounds.Play("SoundWhiteRay");
            _whiteRayCooldown.Reset();
        }
    }

    public void ApplyRayDamage(GameObject target)
    {
        if (_whiteGemCooldown.IsReady)
        {
            var enemy = target.GetComponent<HealthComponent>();
            if (enemy != null)
            {
                enemy.ApplyDamage(_hero.damage/2);
            }
            _whiteGemCooldown.Reset();
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
        Enum.TryParse<Gem>(enumName, out _session.playerData.currentGem);
        ShowGemSprite(_session.playerData.currentGem);
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