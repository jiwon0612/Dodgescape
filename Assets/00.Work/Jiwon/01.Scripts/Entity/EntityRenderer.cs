using UnityEngine;

public class EntityRenderer : MonoBehaviour, IEntityComp
{
    private Entity _entity;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;     
    }

    public void SetRotation(float x, float z)
    {
        float angle = Mathf.Atan2(x,z) * Mathf.Rad2Deg;
        _entity.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
