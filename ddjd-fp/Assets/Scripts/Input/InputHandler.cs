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

    public InputAction PlayerMovement { get { return _playerMovement; } set { _playerMovement = value; } }
    public InputAction PlayerLook { get { return _playerLook; } set { _playerLook = value; } }
    public InputAction PlayerJump { get { return _playerJump; } set { _playerJump = value; } }
    public InputAction PlayerMeleeAttack { get { return _playerMeleeAttack; } set { _playerMeleeAttack = value; } }
    public InputAction PlayerInteract { get { return _playerInteract; } set { _playerInteract = value; } }
    #endregion

    #region UI Input Actions
    #endregion

    #region Input Values
    public Vector2 move {get; set;}
    public Vector2 look {get; set;}
    public bool interact {get; set;}
    public bool sprint {get; set;}
    #endregion
    
    private void Awake() {
        _inputAction = new InputController();
        
        _playerMovement = _inputAction.Player.Move;
        _playerLook = _inputAction.Player.Look;
        _playerJump = _inputAction.Player.Jump;
        _playerMeleeAttack = _inputAction.Player.MeleeAttack;
        _playerInteract = _inputAction.Player.Interact;

        EnablePlayerInput();
    }

    private void EnablePlayerInput() {
        _playerMovement.performed += OnMovement;
        _playerMovement.canceled += OnMovement;
        _playerLook.performed += OnLook;
        _playerLook.canceled += OnLook;
        _playerInteract.performed += OnInteract;

        _inputAction.Player.Enable();
    }

    private void DisablePlayerInput() {
        _inputAction.Player.Disable();
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

    private void OnInteract(InputAction.CallbackContext context){
        interact = true;
    }

    private void OnApplicationFocus(bool hasFocus) {
        Cursor.lockState =  CursorLockMode.Locked;
    }
}
