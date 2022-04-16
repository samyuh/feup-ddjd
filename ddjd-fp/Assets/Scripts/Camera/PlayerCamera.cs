using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : Camera {
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;

    private GameObject _cinemachineCameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    public PlayerCamera() {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _cinemachineCameraTarget = GameObject.FindGameObjectWithTag("PlayerCameraTarget");
    }

    public override void LateUpdateCamera(float magnitude, float lookAxisX, float lookAxisY) {
        if (magnitude >= _threshold && !LockCameraPosition) {
            _cinemachineTargetYaw += lookAxisX;
            _cinemachineTargetPitch += lookAxisY;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch,BottomClamp,TopClamp);

        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float ifAngle, float ifMin, float ifMax) {
        if (ifAngle < -360f) ifAngle += 360f;
        if (ifAngle > 360f) ifAngle -= 360f;
        return Mathf.Clamp(ifAngle, ifMin, ifMax);
    }
}
