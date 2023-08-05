using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    [SerializeField] private Player_IA playerInputActions;

    public EventHandler OnJumpAction;
    public EventHandler OnJumpUpAction;

    public EventHandler OnAttackAction;
    public EventHandler OnDashAction;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new Player_IA();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.started += Jump_Performed;
        playerInputActions.Player.Jump.canceled += Jump_Released;
        playerInputActions.Player.Dash.performed += Dash_Performed;
    }
    private void Dash_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }
    private void Jump_Released(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnJumpUpAction?.Invoke(this, EventArgs.Empty);
    }
    private void Jump_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }
    private void Attack_Perfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVector() 
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        return inputVector;
    }






}
