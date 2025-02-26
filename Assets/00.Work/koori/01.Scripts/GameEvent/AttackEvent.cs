public class AttackEvent : GameEvent
{
    public bool isAttacking;

    public AttackEvent(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }
}
