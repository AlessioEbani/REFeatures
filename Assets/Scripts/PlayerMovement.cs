using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private CharacterController controller;
    private Camera mainCamera;

    public bool tankControls;

    public float rotationSpeed=40f; 
    public float walkSpeed =2;
    public float runSpeed = 4;

    private Vector2 lastDir;
    private Vector3 dir=new Vector3();


    #region UNITY_EVENTS

    private void Awake() {
        playerInputs = GetComponent<PlayerInputs>();
        controller =GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update() {
        NormalControlsUpdate();
        TankControlsUpdate();
        GravityUpdate();
    }

    #endregion

    private void NormalControlsUpdate() {
        var localDir = playerInputs.Move.ReadValue<Vector2>();
        if (localDir != lastDir) {
            lastDir = localDir;
            dir = mainCamera.transform.TransformDirection(lastDir);
            dir.y = 0;
        }
        if (localDir != Vector2.zero) {
            transform.rotation = Quaternion.LookRotation(dir);
        }
        
        dir.Normalize();
        var movement = dir * runSpeed * Time.deltaTime;  
        controller.Move(movement);
    }

    private void TankControlsUpdate() {
        var localDir = playerInputs.TankMove.ReadValue<Vector2>();
        float speed = playerInputs.Run.ReadValue<float>()==0 ? walkSpeed : runSpeed;
        localDir.Normalize();
        controller.Move(transform.forward * localDir.y * speed * Time.deltaTime);
        transform.Rotate(transform.up,rotationSpeed * localDir.x * Time.deltaTime);
    }

    private void GravityUpdate() {
        if (!controller.isGrounded) {
            controller.Move(-transform.up);
        }
    }
}
