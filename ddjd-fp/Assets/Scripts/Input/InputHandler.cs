using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

    public void OnDeviceRegained() {
        Debug.Log("DeviceLost");
    }

    public void OnDeviceLost() {
        Debug.Log("DeviceRegained");
    }

    public void OnControlsChanged() {
        Debug.Log("ControlsChanged");
    }

    public void OnLook(InputValue value) {
        look = value.Get<Vector2>();
	}

    public void OnJump(InputValue value) {
        jump = value.isPressed;
    }
    
    #region Mouse
    public void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }

    private void OnApplicationFocus(bool hasFocus) {
        Cursor.lockState =  CursorLockMode.Locked;
    }

    private void SetCursorState(bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    #endregion
}
