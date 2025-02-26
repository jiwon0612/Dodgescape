using UnityEngine;

public abstract class EntityState
{
    protected Entity _entity;

    protected AnimParamSO _animParam;
    protected bool _isTriggerCall;

    protected EntityRenderer _renderer;

    public EntityState(Entity entity, AnimParamSO animParam)
    {
        _entity = entity;
        _animParam = animParam;
        _renderer = _entity.GetComp<EntityRenderer>();
    }

    public virtual void Enter()
    {
        _renderer.SetParam(_animParam, true);
        _isTriggerCall = false;
    }

    public virtual void Update()
    {
        if (_isTriggerCall)
        {
            _isTriggerCall = false;
            Exit();
        }
    }
    public virtual void FixedUpdate()
    {
    }

    public virtual void Exit()
    {
        _renderer.SetParam(_animParam, false);
    }

    public virtual void AnimationEndTrigger()
    {
        _isTriggerCall = true;
    }

}
