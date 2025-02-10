using System;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private PlayerInputSO playerInputSO;
    private EntityRenderer _renderer;

    protected override void AfterInit()
    {
        base.AfterInit();
        _renderer = GetComp<EntityRenderer>();
    }

    private void Update()
    {
        Vector2 dir = playerInputSO.MoveDirection;
        _renderer.SetRotation(dir.x, dir.y);
    }
}
