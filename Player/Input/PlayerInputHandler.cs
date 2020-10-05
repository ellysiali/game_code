using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInput playerInput;

    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool ContinueInput { get; private set; }
    public bool ExitInput { get; private set; }
    public bool InventoryInput { get; private set; }
    public bool MenuInput { get; private set; }


    public Vector2 MovementInput { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpInputStartTime, dashInputStartTime, attackInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckAttackInputHoldTime();
    }

    #region Gameplay Action Map Functions

    #region Move Functions
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }
    #endregion

    #region Jump Functions
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if(context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    #endregion

    #region Dash Functions
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            dashInputStartTime = Time.time;
        }
    }

    public void UseDashInput() => DashInput = false;

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
    #endregion

    #region Attack Functions
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
            attackInputStartTime = Time.time;
        }
    }

    public void UseAttackInput() => AttackInput = false;

    private void CheckAttackInputHoldTime()
    {
        if (Time.time >= attackInputStartTime + inputHoldTime)
        {
            AttackInput = false;
        }
    }
    #endregion

    #region Inventory Functions
    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InventoryInput = true;
        }
    }

    public void UseInventoryInput() => InventoryInput = false;
    #endregion

    #region Menu Functions
    public void OnMenuInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MenuInput = true;
        }
    }
    #endregion
    #endregion


    #region Interactions Action Map Functions

    #region Continue Functions
    public void OnContinueInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ContinueInput = true;
        }
    }
    public void UseContinueInput() => ContinueInput = false;
    #endregion

    #region Exit Functions
    public void OnExitInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ExitInput = true;
        }
    }

    public void UseExitInput() => ExitInput = false;
    #endregion

    #endregion


    #region Other Functions
    public void SetActionMapToInteractions() => playerInput.SwitchCurrentActionMap("Interactions");
    public void SetActionMapToGameplay() => playerInput.SwitchCurrentActionMap("Gameplay");
    #endregion
}
