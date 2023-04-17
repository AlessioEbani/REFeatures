using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CameraCollisionList {
    public CameraCollider[] colliders;
    public GameObject camera;

    public event Action<GameObject> OnCameraTrigger;

    public CameraCollisionList(CameraCollider[] colliders,GameObject camera) {
        this.colliders = colliders;
        this.camera = camera;
        foreach (CameraCollider c in colliders) {
            c.OnTrigger += ChangeCamera;
        }
    }

    private void ChangeCamera() {
        OnCameraTrigger?.Invoke(camera);
    }
}

public class CameraSwitcher : MonoBehaviour
{
    public GameObject firstActivatedCamera;
    private GameObject lastActivatedCamera;

    private List<Transform> cameraParents;
    private List<CameraCollisionList> cameraCollisions;


    private void Awake() {
        cameraParents = new List<Transform>();
        for(int i=0; i<transform.childCount;i++) {
            cameraParents.Add(transform.GetChild(i));
        }
        cameraCollisions = new List<CameraCollisionList>();
        foreach(Transform cameraParent in cameraParents) {
            CameraCollisionList cameraCollision = new CameraCollisionList(
                cameraParent.GetComponentsInChildren<CameraCollider>(),
                cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject);
            cameraCollision.camera.SetActive(cameraCollision.camera.Equals(firstActivatedCamera));
            cameraCollision.OnCameraTrigger += ChangeCamera;
            cameraCollisions.Add(cameraCollision);
        }
        lastActivatedCamera = firstActivatedCamera;
    }

    private void ChangeCamera(GameObject newCamera) {
        lastActivatedCamera.SetActive(false);
        newCamera.SetActive(true);
        lastActivatedCamera = newCamera;
    }
}
