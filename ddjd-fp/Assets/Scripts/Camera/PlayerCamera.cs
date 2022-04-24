using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera{
    private GameObject _mainCamera;
    public GameObject MainCamera {get {return _mainCamera;} set {_mainCamera = value;}}
    
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;

    private GameObject _cinemachineCameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    // Copied other script

    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;

    public GameObject followTransform;

    public PlayerCamera() {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // _mainCamera = GameObject.FindGameObjectWithTag("PlayerCameraTarget");
        // _cinemachineCameraTarget = GameObject.FindGameObjectWithTag("PlayerCameraTarget");

        Debug.Log("Current Camera : " + _mainCamera);
    }

    public void LateUpdateCamera(float magnitude, float lookAxisX, float lookAxisY) {
    //     if (magnitude >= _threshold && !LockCameraPosition) {
    //         _cinemachineTargetYaw += lookAxisX;
    //         _cinemachineTargetPitch += lookAxisY;
    //     }

    //     _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
    //     _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch,BottomClamp,TopClamp);

    //     _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    //

    // #region Player Based Rotation
        
    //     //Move the player based on the X input on the controller
    //     //transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

    //     #endregion

    //     #region Follow Transform Rotation

    //     //Rotate the Follow Target transform based on the input
    //     followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

    //     #endregion

    //     #region Vertical Rotation
    //     followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

    //     var angles = followTransform.transform.localEulerAngles;
    //     angles.z = 0;

    //     var angle = followTransform.transform.localEulerAngles.x;

    //     //Clamp the Up/Down rotation
    //     if (angle > 180 && angle < 340)
    //     {
    //         angles.x = 340;
    //     }
    //     else if(angle < 180 && angle > 40)
    //     {
    //         angles.x = 40;
    //     }


    //     followTransform.transform.localEulerAngles = angles;
    //     #endregion

        
    //     nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

    //     if (_move.x == 0 && _move.y == 0) 
    //     {   
    //         nextPosition = transform.position;

    //         if (aimValue == 1)
    //         {
    //             //Set the player rotation based on the look transform
    //             transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
    //             //reset the y rotation of the look transform
    //             followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    //         }

    //         return; 
    //     }
    //     float moveSpeed = speed / 100f;
    //     Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
    //     nextPosition = transform.position + position;        
        

    //     //Set the player rotation based on the look transform
    //     transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
    //     //reset the y rotation of the look transform
    //     followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0); 
    
    }

    private static float ClampAngle(float ifAngle, float ifMin, float ifMax) {
        if (ifAngle < -360f) ifAngle += 360f;
        if (ifAngle > 360f) ifAngle -= 360f;
        return Mathf.Clamp(ifAngle, ifMin, ifMax);
    }
}
