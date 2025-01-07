using UnityEngine;

public interface IEntityComp
{
    public void Initialize(Entity entity);
}

public interface IAfterInit : IEntityComp
{
    public void AfterInit();
}
