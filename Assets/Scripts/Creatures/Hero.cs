using UnityEngine;
using UnityEngine.UI;

public class Hero : Creature
{
    [Header("Settings")]
    [SerializeField] private float _currentHealth;

    [Header("Platform settings")] [SerializeField]
    private LayerMask _platformLayer;

    [SerializeField] private float _platformCheckRadius = 0.25f;
    [SerializeField] private Vector3 _platformCheckPositionDelta = new Vector3(0, -0.3f, 0);

    [Header("Interaction settings")] [SerializeField]
    private float _interactionRadius;

    [SerializeField] private LayerMask _interactionLayer;

    [Header("Other settings")] [SerializeField]
    private Transform _additionalPosition;

    [SerializeField] private bool _bloxMoveX = false;

    private bool _isPlatform;
    private float _directionVertical;
    private Collider2D[] _interactionResult = new Collider2D[5];

    private GameSession _session;
    private HealthBarUIComponent _healthBar;

    private static readonly int IsClimb = Animator.StringToHash("is-climb");

    protected override void Start()
    {
        base.Start();
        _session = FindObjectOfType<GameSession>();
        _healthBar = FindObjectOfType<HealthBarUIComponent>();
        var health = GetComponent<HealthComponent>();
        
        transform.position = _session.playerData.playerPositionAfterLoadingScene;
    }

    protected override void Update()
    {
        base.Update();
        _isPlatform = IsPlatform();
        if (!_bloxMoveX)
        {
            Animator.SetBool(IsClimb, false);
        }
    }

    protected override void FixedUpdate()
    {
        if (!_bloxMoveX)
        {
            base.FixedUpdate();
        }
    }

    protected override void LateUpdate()
    {
        if (!_bloxMoveX)
        {
            base.LateUpdate();
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        _session.playerData.maxHealth = maxHealth;

        _currentHealth = (float) maxHealth / _session.playerData.maxHealth;
        SetCurrentHealthInBar();
    }

    public void OnHealthChange(int currentHealth)
    {
        _session.playerData.currentHealth = currentHealth;

        _currentHealth = (float) currentHealth / _session.playerData.maxHealth;
        SetCurrentHealthInBar();
    }

    private void SetCurrentHealthInBar()
    {
        _healthBar.FillAmount(_currentHealth);
    }

    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult,
            _interactionLayer);
        for (int i = 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void Climb()
    {
        transform.position = new Vector3(_additionalPosition.position.x, _additionalPosition.position.y,
            transform.position.z);
    }

    public void StartAnimClimb()
    {
        Animator.SetBool(IsClimb, true);
        _bloxMoveX = true;
        Rb.velocity = Vector2.zero;
        Animator.Play("climb");
        Sounds.Play("SoundClimb");
    }

    public void ThrowStone()
    {
        if (IsGrounded)
        {
            _bloxMoveX = true;
            Rb.velocity = new Vector2(0, Rb.velocity.y);
            Animator.Play("throwStone");   
        }
    }
    
    public void SetDirectionVertical(float direction)
    {
        _directionVertical = direction;
    }

    private bool IsPlatform()
    {
        var hit = Physics2D.CircleCast(transform.position + _platformCheckPositionDelta, _platformCheckRadius,
            Vector2.down, 0, _platformLayer);
        return hit.collider != null;
    }

    public bool IsPlatformAndDown()
    {
        if (_isPlatform && _directionVertical == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void AddInventory(string id, int value)
    {
        _session.playerData.inventory.Add(id, value);
    }

}