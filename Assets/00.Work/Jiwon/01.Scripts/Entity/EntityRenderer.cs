using UnityEngine;

public class EntityRenderer : AnimatorCompo, IEntityComp
{
    private Entity _entity;
    private Rigidbody _rb;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _rb = _entity.GetComponent<Rigidbody>();
    }

    public void SetRotation(Quaternion targetRotation, float rotationSpeed)
    {
        Quaternion newRotation = Quaternion.RotateTowards(
            _rb.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );
        _rb.MoveRotation(newRotation);
    }
}
