using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Dictionary<Type, IEntityComp> _components;
    
    private void Awake()
    {
        _components = new Dictionary<Type, IEntityComp>();
        GetComponentsInChildren<IEntityComp>(true).ToList().ForEach((x) => _components.Add(x.GetType(), x));

        InitComp();
        AfterInit();
    }

    protected virtual void InitComp()
    {
        _components.Values.ToList().ForEach((x) => x.Initialize(this));
    }
    
    protected virtual void AfterInit()
    {
        _components.Values.ToList().ForEach((x) =>
        {
            if (x is IAfterInit afterInit)
            {
                afterInit.AfterInit();
            }
        });
    }

    public T GetComp<T>(bool isdDerived = false) where T : IEntityComp
    {
        if (_components.TryGetValue(typeof(T), out IEntityComp comp))
        {
            return (T)comp;
        }

        if (isdDerived == false) return default;
        
        Type findType = _components.Keys.FirstOrDefault((t) => t.IsSubclassOf(typeof(T)));
        if (findType != null)
            return (T)_components[findType];
        
        return default;
    }

    public abstract void Stun(Vector3 originPoint, float duration);
}
