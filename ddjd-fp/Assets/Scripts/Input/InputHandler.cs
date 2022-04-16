using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {
    private InputController _inputAction;

    #region Player Input Actions
    private InputAction _playerMovement;
    private InputAction _playerLook;
    private InputAction _playerJump;
    private InputAction _playerMeleeAttack;
    private InputAction _playerInteract;
    #endregion

    #region UI Input Actions
    #endregion

    #region Input Values
    public Vector2 move {get; set;}
    public Vector2 look {get; set;}
    public bool jump {get; set;}
    public bool interact {get; set;}
    public bool sprint {get; set;}
    public bool meleeAttack {get; set;}
    #endregion
    
    private void Awake() {
        _inputAction = new InputController();

        EnablePlayerActions();
        // EnableUIActions
    }

    private void EnablePlayerActions() {
        _playerMovement = _inputAction.Player.Move;
        _playerMovement.performed += OnMovement;
        _playerMovement.canceled += OnMovement;
        _playerMovement.Enable();

        _playerLook = _inputAction.Player.Look;
        _playerLook.performed += OnLook;
        _playerLook.canceled += OnLook;
        _playerLook.Enable();

        _playerJump = _inputAction.Player.Jump;
        _playerJump.performed += OnJump;
        _playerJump.Enable();

        _playerMeleeAttack = _inputAction.Player.MeleeAttack;
        _playerMeleeAttack.performed += OnMeleeAttack;
        _playerMeleeAttack.Enable();

        _playerInteract = _inputAction.Player.Interact;
        _playerInteract.performed += OnInteract;
        _playerInteract.Enable();
    }

    private void Disable() {
        
    }

    private void Rebind() {
        // _playerJump.rebind("Gamepad/X");
    }

    private void OnMovement(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context) {
        look = context.ReadValue<Vector2>();
	}

    private void OnJump(InputAction.CallbackContext context) {
        jump = true;
    }
    
    private void OnMeleeAttack(InputAction.CallbackContext context) {
        meleeAttack = true;
    }

    private void OnInteract(InputAction.CallbackContext context){
        interact = true;
    }

    private void OnApplicationFocus(bool hasFocus) {
        Cursor.lockState =  CursorLockMode.Locked;
    }
}
