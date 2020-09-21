using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionsInputHandler : MonoBehaviour
{
    public bool ContinueInput { get; private set; }
    public bool ExitInput { get; private set; }
    public Vector2 MovementInput { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;

    private float continueInputStartTime, exitInputStartTime;
    private void Update()
    {
        CheckContinueInputHoldTime();
        CheckExitInputHoldTime();
    }

    public void OnContinueInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ContinueInput = true;
        }
    }

    private void CheckContinueInputHoldTime()
    {
        if (Time.time >= continueInputStartTime + inputHoldTime)
        {
            ContinueInput = false;
        }
    }

    public void OnExitInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ExitInput = true;
        }
    }

    private void CheckExitInputHoldTime()
    {
        if (Time.time >= exitInputStartTime + inputHoldTime)
        {
            ExitInput = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

}
