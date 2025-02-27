using System;
using UnityEngine;

public class CurveBullet : Bullet
{
    [Header("CurveBulletSetting")]
    [SerializeField] private float curveRadius;
    
    private Vector3 startPos;
    private Vector3 point;
    private Transform endTrm;
    
    public override void InitAndFire(Entity dealer, Transform target)
    {
        base.InitAndFire(dealer, target);
        
        Vector2 randomVector = UnityEngine.Random.insideUnitCircle;
        point = dealer.transform.
            TransformPoint(new Vector3(randomVector.x,Mathf.Abs(randomVector.y),-1) * curveRadius);
        
        startPos = dealer.transform.position;
        endTrm = target.transform;
        
        transform.position = startPos;
    }

    private void FixedUpdate()
    {
        if (!IsFire) return;
        
        float t = _currentTime / lifeTime * speed;
        Vector3 p1 = Vector3.Lerp(startPos, point, t);
        Vector3 p2 = Vector3.Lerp(point, endTrm.position, t);
        Vector3 position = Vector3.Lerp(p1, p2, t);
        
        _rbCompo.MovePosition(position);
    }
}
