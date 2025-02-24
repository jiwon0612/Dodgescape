using UnityEngine;

public class Player : Entity
{
    [Header("FSM")]
    [SerializeField] private EntityStatesSO _playerFSM;

    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

    public EntityState CurrentState => _stateMachine.currentState;
    
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float gravityMultiplier = 1.5f;

    private EntityMover _mover;
    private EntityAnimator _animator;

    private StateMachine _stateMachine;

    protected override void AfterInit()
    {
        base.AfterInit();

        _mover = GetComp<EntityMover>();
        _animator = GetComp<EntityAnimator>();

        _stateMachine = new StateMachine(_playerFSM, this);

        PlayerInput.DashEvent += HandleDashEvent;

        _animator.OnAnimationEnd += HandleAnimationEnd;
    }

    private void HandleDashEvent()
    {
        ChangeState("Dash");
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
}
