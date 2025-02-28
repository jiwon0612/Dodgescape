using UnityEngine;

public class Player : Entity
{
    [Header("FSM")]
    [SerializeField] private EntityStatesSO _playerFSM;

    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

    public EntityState CurrentState => _stateMachine.currentState;

    public GameEventChannelSO PlayerEventChannel;

    public float dodgeSpeed = 15f;
    public float dashSpeed = 25f;
    public float dodgeCool;
    public float dashCool;
    public float attackCool;

    private float _nowDodgeCool;
    private float _nowDashCool;
    private float _nowAttackCool;

    private EntityAnimator _animator;
    private EntityMover _mover;
    private EntityHealth _health;
    private EntityEvasionSkillCompo _skillCompo;

    private StateMachine _stateMachine;

    protected override void AfterInit()
    {
        base.AfterInit();

        _animator = GetComp<EntityAnimator>();
        _health = GetComp<EntityHealth>();
        _mover = GetComp<EntityMover>();
        _skillCompo = GetComp<EntityEvasionSkillCompo>();

        _stateMachine = new StateMachine(_playerFSM, this);

        PlayerInput.DashEvent += HandleDashEvent;
        PlayerInput.AttackEvent += HandleAttackEvent;
        PlayerInput.DodgeEvent += HandleDodgeEvent;
        PlayerInput.InteratEvent += HandleInteractEvent;

        _animator.OnAnimationEnd += HandleAnimationEnd;
        _health.OnHitEvent += ApplyDamage;
        _health.OnEvasionEvent.AddListener(HandleEvasionEvent); //회피기믹 여따 구도하십셔
    }

    private void HandleEvasionEvent(Entity dealer)
    {
        _skillCompo.activeSkill.UseSkill(dealer);
    }

    private void HandleDodgeEvent()
    {
        if(AttemptDodge())
            ChangeState("Dodge");
    }

    private bool AttemptDodge()
    {
        if (Time.time > _nowDodgeCool)
        {
            _nowDodgeCool = Time.time + dodgeCool;
            return true;
        }
        return false;
    }

    private void HandleDashEvent()
    {
        if (AttemptDash())
            ChangeState("Dash");
    }

    private bool AttemptDash()
    {
        if (Time.time > _nowDashCool)
        {
            _nowDashCool = Time.time + dashCool;
            return true;
        }
        return false;
    }

    private void HandleAttackEvent()
    {
        if (AttemptAttack())
            ChangeState("Attack");
    }

    private bool AttemptAttack()
    {
        if (Time.time > _nowAttackCool)
        {
            _nowAttackCool = Time.time + attackCool;
            return true;
        }
        return false;
    }

    private void HandleInteractEvent()
    {
        ChangeState("Interact");
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }

    private void OnDestroy()
    {
        PlayerInput.DashEvent -= HandleDashEvent;
        PlayerInput.AttackEvent -= HandleAttackEvent;
        PlayerInput.DodgeEvent -= HandleDodgeEvent;
        
        _animator.OnAnimationEnd -= HandleAnimationEnd;
        
        _health.OnHitEvent -= ApplyDamage;
        _health.OnEvasionEvent.RemoveListener(HandleEvasionEvent);
    }

    private void Start()
    {
        _stateMachine.Initialize("Idle");
    }

    public EntityState GetState(StateSO state)
        => _stateMachine.GetState(state.stateName);

    public void ChangeState(string newState)
        => _stateMachine.ChangeState(newState);

    private void Update()
    {
        _stateMachine.currentState.Update();
    }

    public override void ApplyDamage(float damage, Vector2 knockBack, float stunDuration)
    {
        _mover.KnockBack(knockBack, stunDuration);
    }
}
