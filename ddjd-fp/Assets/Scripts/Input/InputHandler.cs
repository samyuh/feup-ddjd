using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {
    private InputController _inputAction;

    #region UI Input Actions
    #endregion

    #region Player Input Actions
    private InputAction _playerMovement;
    private InputAction _playerCrystalWheel;
    private InputAction _playerRun;
    private InputAction _playerDash;
    private InputAction _playerAim;
    private InputAction _playerLook;
    private InputAction _playerJump;
    private InputAction _playerMeleeAttack;
    private InputAction _playerInteract;
    private InputAction _playerUseItem;
    private InputAction _toggleInventory;
    private InputAction _togglePauseMenu;
    private InputAction _nextDialog;

    public InputAction PlayerMovement { get { return _playerMovement; } set { _playerMovement = value; } }
    public InputAction PlayerRun { get { return _playerRun; } set { _playerRun = value; } }
    public InputAction PlayerDash { get { return _playerDash; } set { _playerDash = value; } }
    public InputAction PlayerAim { get { return _playerAim; } set { _playerAim = value; } }
    public InputAction PlayerLook { get { return _playerLook; } set { _playerLook = value; } }
    public InputAction PlayerJump { get { return _playerJump; } set { _playerJump = value; } }
    public InputAction PlayerMeleeAttack { get { return _playerMeleeAttack; } set { _playerMeleeAttack = value; } }
    public InputAction PlayerInteract { get { return _playerInteract; } set { _playerInteract = value; } }
    public InputAction PlayerUseItem { get { return _playerUseItem; } set { _playerUseItem = value; } }
    #endregion

    #region UI Input Actions
    #endregion

    #region Input Values
    public Vector2 Movement {get; set;}
    public Vector2 Look {get; set;}
    public bool Interact {get; set;}
    public bool UseItem {get; set;}
    #endregion
    
    private void Awake() {
        _inputAction = new InputController();
        
        Events.DisableMovement.AddListener(DisableMovement);
        Events.EnableMovement.AddListener(EnableMovement);
        Events.EnableAim.AddListener(EnableAim);

        _playerMovement = _inputAction.Player.Move;
        _playerCrystalWheel = _inputAction.Player.CrystalWheel;
        _playerRun = _inputAction.Player.Run;
        _playerDash = _inputAction.Player.Dash;
        _playerLook = _inputAction.Player.Look;
        _playerJump = _inputAction.Player.Jump;
        _playerMeleeAttack = _inputAction.Player.MeleeAttack;
        _playerInteract = _inputAction.Player.Interact;
        _playerUseItem = _inputAction.Player.UseItem;
        _toggleInventory = _inputAction.Player.Inventory;
        _togglePauseMenu = _inputAction.Player.PauseMenu;
        _nextDialog = _inputAction.Player.NextDialog;

        _playerCrystalWheel.performed += OnToggleCrystalWheel;
        _playerCrystalWheel.canceled += OnToggleCrystalWheel;
        _playerInteract.performed += OnInteract;
        _playerUseItem.performed += OnUseItem;
        _playerMovement.performed += OnMovement;
        _playerMovement.canceled += OnMovement;

        _playerLook.performed += OnLook;
        _playerLook.canceled += OnLook;
        _toggleInventory.performed += OnToggleInventory;
        _togglePauseMenu.performed += OnTogglePauseMenu;
        _nextDialog.performed += OnNextDialog;

        _inputAction.Player.Enable();
    }

    private void EnableAim() {
        _playerAim = _inputAction.Player.Aim;
    }

    private void EnableMovement() {
        _playerCrystalWheel.performed += OnToggleCrystalWheel;
        _playerCrystalWheel.canceled += OnToggleCrystalWheel;
        _playerInteract.performed += OnInteract;
        _playerUseItem.performed += OnUseItem;
        _playerMovement.performed += OnMovement;
        _playerMovement.canceled += OnMovement;
    }

    private void DisableMovement() {
       _playerCrystalWheel.performed -= OnToggleCrystalWheel;
        _playerCrystalWheel.canceled -= OnToggleCrystalWheel;
        _playerInteract.performed -= OnInteract;
        _playerUseItem.performed -= OnUseItem;
        _playerMovement.performed -= OnMovement;
        _playerMovement.canceled -= OnMovement;
    }

    private void Rebind() {
        // _playerJump.rebind("Gamepad/X");
    }

    private void OnNextDialog(InputAction.CallbackContext context) {
        Events.OnNextDialog.Invoke();
    }

    private void OnTogglePauseMenu(InputAction.CallbackContext context) {
        Events.OnTogglePauseMenu.Invoke();
    }

    private void OnToggleInventory(InputAction.CallbackContext context) {
        Events.OnToggleInventory.Invoke();
    }

    private void OnToggleCrystalWheel(InputAction.CallbackContext context) {
        Events.OnToggleCrystalWheel.Invoke();
    }

    private void OnMovement(InputAction.CallbackContext context) {
        Movement = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context) {
        Look = context.ReadValue<Vector2>();
	}

    private void OnInteract(InputAction.CallbackContext context){
        Interact = true;
    }

    private void OnUseItem(InputAction.CallbackContext context)
    {
        UseItem = true;
    }

    private void OnApplicationFocus(bool hasFocus) {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
