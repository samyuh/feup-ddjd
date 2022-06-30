using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Código obtido em https://sharpcoderblog.com/blog/unity-3d-character-controller-moving-platform-support
public class SC_MovingPlatform : MonoBehaviour {
    public Transform activePlatform;

    CharacterController controller;
    Vector3 moveDirection;
    Vector3 activeGlobalPlatformPoint;
    Vector3 activeLocalPlatformPoint;
    Quaternion activeGlobalPlatformRotation;
    Quaternion activeLocalPlatformRotation;

    void Start() {
        controller = GetComponent<CharacterController>();
        Events.OnFreeFall.AddListener(SetNullPlatform);
    }

    private void SetNullPlatform() {
        activePlatform = null;
    }

    void Update()  {
        if (activePlatform != null){
            Vector3 newGlobalPlatformPoint = activePlatform.TransformPoint(activeLocalPlatformPoint);
            moveDirection = newGlobalPlatformPoint - activeGlobalPlatformPoint;
            if (moveDirection.magnitude > 0.01f)
            {
                controller.Move(moveDirection);
            }
            if (activePlatform) {
                Quaternion newGlobalPlatformRotation = activePlatform.rotation * activeLocalPlatformRotation;
                Quaternion rotationDiff = newGlobalPlatformRotation * Quaternion.Inverse(activeGlobalPlatformRotation);

                rotationDiff = Quaternion.FromToRotation(rotationDiff * Vector3.up, Vector3.up) * rotationDiff;
                transform.rotation = rotationDiff * transform.rotation;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                UpdateMovingPlatform();
            }
        }
        else
        {
            if (moveDirection.magnitude > 0.01f)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime);
                controller.Move(moveDirection);
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.moveDirection.y < -0.9 && hit.normal.y > 0.41) {
            if (activePlatform != hit.collider.transform) {
                activePlatform = hit.collider.transform;
                UpdateMovingPlatform();
            }
        } else {
            activePlatform = null;
        }
    }

    void UpdateMovingPlatform() {
        activeGlobalPlatformPoint = transform.position;
        activeLocalPlatformPoint = activePlatform.InverseTransformPoint(transform.position);

        activeGlobalPlatformRotation = transform.rotation;
        activeLocalPlatformRotation = Quaternion.Inverse(activePlatform.rotation) * transform.rotation;
    }
}