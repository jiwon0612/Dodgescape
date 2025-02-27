using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class EvasionSkill : MonoBehaviour
{
    [field : SerializeField] public bool SkillEnabled { get; set; }
    
    protected Entity _entity;
    protected EntityEvasionSkillCompo _skillCompo;
    
    public virtual void InitSkill(Entity entity, EntityEvasionSkillCompo compo)
    {
        _entity = entity;
        _skillCompo = compo;
    }

    public virtual void UseSkill(Entity target)
    {
        
    }
}
