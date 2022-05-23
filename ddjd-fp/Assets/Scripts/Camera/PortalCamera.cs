using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    public Transform button;
    public PortalButton buttonScript;

    void Start()
    {
        buttonScript = button.GetComponent<PortalButton>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 playerCameraOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerCameraOffsetFromPortal;*/

        if(buttonScript.isPressed){
            float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
            Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
            Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
    }
}
