using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Input/PlayerInput")]
public class PlayerInputSO : ScriptableObject, Console.IPlayerActions
{
    public Vector2 MoveDirection { get; private set; }

    public event Action DashEvent;
    public event Action InteratEvent;
    public event Action AttackEvent;
    public event Action DodgeEvent;

    private Console _console;
    
    private void OnEnable()
    {
        if (_console == null)
        {
            _console = new Console();
            _console.Player.SetCallbacks(this);
        }
        _console.Player.Enable();
    }

    private void OnDisable()
    {
        _console.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            InteratEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            DashEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
            DodgeEvent?.Invoke();
    }
}
