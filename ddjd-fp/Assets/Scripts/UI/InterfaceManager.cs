using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private AimController _aim;
    [SerializeField] private CrystalWheelController _crystalWheel;
    [SerializeField] private InventoryController _inventory;
    [SerializeField] private PauseMenuController _pauseMenu;
    [SerializeField] private GameObject _gameOverlay;
    
    void Awake()
    {   
        Events.OnToggleAim.AddListener(OnToggleAim);
        Events.OnToggleInventory.AddListener(OnToggleInventory);
        Events.OnToggleCrystalWheel.AddListener(OnToggleCrystalWheel);
        Events.OnTogglePauseMenu.AddListener(OnTogglePauseMenu);
    }

    void Update()
    {
        
    }

    public void OnToggleAim() {
        _aim.OnToggleAim();
    }

    public void OnToggleInventory() {
        _pauseMenu.OnTogglePauseMenu();
    }

    public void OnToggleCrystalWheel() {
        _crystalWheel.OnToggleCrystalWheel();
    }

    public void OnTogglePauseMenu() {
        _pauseMenu.OnTogglePauseMenu();
    }
}
