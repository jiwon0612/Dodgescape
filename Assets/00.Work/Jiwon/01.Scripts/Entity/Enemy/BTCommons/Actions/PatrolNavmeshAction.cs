using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatrolNavmesh", story: "[self] patrol with [mover] in [distance]", category: "Action",
    id: "df3e5c63eac51c35dc9062a823d1b906")]
public partial class PatrolNavmeshAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<EntityNavMeshMover> Mover;
    [SerializeReference] public BlackboardVariable<float> Distance;

    protected override Status OnStart()
    {
        BTEnemy enemy = Self.Value;

        Vector2 randomPosition = Random.insideUnitCircle * Distance.Value;
        Vector3 point = new Vector3(enemy.transform.position.x + randomPosition.x,
            enemy.transform.position.y, enemy.transform.position.z + randomPosition.y);

        if (NavMesh.SamplePosition(point, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            Mover.Value.SetMovement(hit.position);
        }
        else
            return Status.Failure;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Mover.Value.GetIsSuccessToPoint())
        {
            return Status.Success;
        }
        else
            return Status.Running;
    }
}