using System.Numerics;

public enum InteractType
{
    Chest,
    Talk,
}
public interface IInteractable
{ 
    public void Interact();
}
