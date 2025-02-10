using UnityEngine;

[CreateAssetMenu(fileName = "EntityFinderSO", menuName = "SO/Entity/Finder")]
public class EntityFinderSO : ScriptableObject
{
    [SerializeField] private string targetTag;
    public Entity target;

    public void SetEntity(Entity entity) => target = entity;
}
