public class AttackEvent : GameEvent
{
    public bool isAttacking;
}

public static class PlayerEvent
{
    public static AttackEvent AttackEvent = new AttackEvent();
}
