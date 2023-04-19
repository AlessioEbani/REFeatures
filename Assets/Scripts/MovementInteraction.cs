using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInteraction : MonoBehaviour, IInteractable {

    public GameObject firstTarget;
    public GameObject secondTarget;

    public bool Interact(PlayerInteract interactor) {
        Debug.Log("Interact");
        return true;
    }
}
