using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInputs playerInputs;

    [SerializeField]private Transform interactionPoint;
    [SerializeField]private Vector3 interactionSize;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders=new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable;

    private void Awake() {
        playerInputs = GetComponent<PlayerInputs>();
    }

    private void Update() {
        numFound = Physics.OverlapBoxNonAlloc(interactionPoint.position, interactionSize/2, colliders,Quaternion.identity, interactableMask);
        if(playerInputs.Interact.WasPressedThisFrame() && numFound > 0 ) {
            var interactable = colliders[0].GetComponentInParent<IInteractable>();
            interactable?.Interact(this);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(interactionPoint.position, interactionSize);
    }
}
