using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityEvasionSkillCompo : MonoBehaviour, IEntityComp
{
    public EvasionSkill activeSkill;
    
    private Dictionary<Type, EvasionSkill> _skills;
    
    private Entity _entity;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
        
        _skills = new Dictionary<Type, EvasionSkill>();
        
        GetComponentsInChildren<EvasionSkill>(true).ToList().ForEach(skill => 
            _skills.Add(skill.GetType(), skill));
        _skills.Values.ToList().ForEach(skill => skill.InitSkill(_entity, this));
    }

    public T GetSkill<T>() where T : EvasionSkill
    {
        Type type = typeof(T);
        return _skills.GetValueOrDefault(type) as T;
    }
}
