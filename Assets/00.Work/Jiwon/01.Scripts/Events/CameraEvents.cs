using Unity.Cinemachine;
using UnityEngine;

public static class CameraEvents
{
    public static CamShakeEvent CameraShake = new CamShakeEvent();
}

public class CamShakeEvent : GameEvent
{
    public float shakePower;
    public float shakeTime;
    public CinemachineImpulseDefinition.ImpulseShapes impulseShapes;
    public Vector3 shakeDirection;
}
