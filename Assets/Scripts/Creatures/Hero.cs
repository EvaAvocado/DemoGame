using UnityEngine;
using UnityEngine.UI;

public class Hero : Creature
{
    [Header("UI")] [SerializeField] private Image _healthBar;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Image[] _uhuvdus;

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

    private static readonly int IsClimb = Animator.StringToHash("is-climb");

    protected override void Start()
    {
        base.Start();
        _session = FindObjectOfType<GameSession>();
        var health = GetComponent<HealthComponent>();
        
        transform.position = _session.playerData.playerPositionAfterLoadingScene;
        
        CheckUhuvdus();
        UpdateUhuvduState();
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
        _healthBar.fillAmount = _currentHealth;
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
    
    private void CheckUhuvdus()
    {
        switch (_session.playerData.uhuwdu)
        {
            case 3:
                _uhuvdus[0].enabled = true;
                _uhuvdus[1].enabled = true;
                _uhuvdus[2].enabled = true;
                break;
            case 2:
                _uhuvdus[0].enabled = true;
                _uhuvdus[1].enabled = true;
                _uhuvdus[2].enabled = false;
                break;
            case 1:
                _uhuvdus[0].enabled = true;
                _uhuvdus[1].enabled = false;
                _uhuvdus[2].enabled = false;
                break;
            case 0:
                _uhuvdus[0].enabled = false;
                _uhuvdus[1].enabled = false;
                _uhuvdus[2].enabled = false;
                break;
        }
    }

    public void SubUhuvdu()
    {
        _session.playerData.uhuwdu = _session.playerData.uhuwdu - 1;

        if (_session.playerData.uhuwdu == -1)
        {
            _session.playerData.uhuwdu = 3;
        }
    }

    public void UpdateUhuvduState()
    {
        if (_uhuvdus[0].enabled == true && _uhuvdus[1].enabled == true && _uhuvdus[2].enabled == true)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Green;
        }
        else if (_uhuvdus[0].enabled == true && _uhuvdus[1].enabled == true && _uhuvdus[2].enabled == false)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Pink;
        }
        else if (_uhuvdus[0].enabled == true && _uhuvdus[1].enabled == false && _uhuvdus[2].enabled == false)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Blue;
        }
        else if (_uhuvdus[0].enabled == false && _uhuvdus[1].enabled == false && _uhuvdus[2].enabled == false)
        {
            _session.level1Data.stateColorLevel = Level1Data.ColorLevel.Red;
        }
    }
}