using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlayerInputActions playerControls;
    private InputAction move;
    public InputAction Move => move;
    private InputAction tankMove;
    public InputAction TankMove=> tankMove;
    private InputAction run;
    public InputAction Run=> run;
    private InputAction interact;
    public InputAction Interact => interact;

    #region UNITY_EVENTS
    private void Awake() {
        playerControls = new PlayerInputActions();
    }


    public void OnEnable() {
        EnableInputs();
    }

    private void OnDisable() {
        DisableInputs();
    }

    #endregion

    #region INPUTS

    private void EnableInputs() {
        move = playerControls.Player.Move;
        tankMove = playerControls.Player.TankMove;
        run = playerControls.Player.Run;
        interact = playerControls.Player.Interact;

        move.Enable();
        tankMove.Enable();
        run.Enable();
        interact.Enable();
    }

    private void DisableInputs() {
        move.Disable();
        tankMove.Disable();
        run.Disable();
        interact.Disable();
    }

    #endregion

}
