using UnityEngine;

public class Player : Entity
{
    [Header("FSM")]
    [SerializeField] private EntityStatesSO _playerFSM;

    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

    public EntityState CurrentState => _stateMachine.currentState;
    
    public float dashSpeed = 25f;
    public float dashCool;
    public float attackCool;

    private float _nowDashCool;
    private float _nowAttackCool;

    private EntityAnimator _animator;

    private StateMachine _stateMachine;

    protected override void AfterInit()
    {
        base.AfterInit();

        _animator = GetComp<EntityAnimator>();

        _stateMachine = new StateMachine(_playerFSM, this);

        PlayerInput.DashEvent += HandleDashEvent;
        PlayerInput.AttackEvent += HandleAttackEvent;

        _animator.OnAnimationEnd += HandleAnimationEnd;
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

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }

    private void OnDestroy()
    {
        PlayerInput.DashEvent -= HandleDashEvent;
        GetComp<EntityAnimator>().OnAnimationEnd -= HandleAnimationEnd;
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
        
    }
}
