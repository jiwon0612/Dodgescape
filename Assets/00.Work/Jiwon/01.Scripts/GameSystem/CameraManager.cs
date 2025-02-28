using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO cameraChannel;

    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();        
        
        cameraChannel.AddListener<CamShakeEvent>(ShakeCamera);
    }

    private void OnDestroy()
    {
        cameraChannel.RemoveListener<CamShakeEvent>(ShakeCamera);
    }

    public void ShakeCamera(CamShakeEvent evt)
    {
        _impulseSource.ImpulseDefinition.ImpulseShape = evt.impulseShapes;
        _impulseSource.ImpulseDefinition.ImpulseDuration = evt.shakeTime;
        _impulseSource.DefaultVelocity = evt.shakeDirection * evt.shakePower;
        
        _impulseSource.GenerateImpulse();
    }
}
