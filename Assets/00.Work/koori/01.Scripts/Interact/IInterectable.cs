using System.Numerics;

public enum InteractType
{
    Chest,
    Talk,
}
public interface IInteractable
{
    public InteractType InteractType { get; }
    public void Interact();
}
