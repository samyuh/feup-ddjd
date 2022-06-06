using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VectorGraphics;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private AimController _aim;
    [SerializeField] private CrystalWheelController _crystalWheel;
    [SerializeField] private InventoryController _inventory;
    [SerializeField] private PauseMenuController _pauseMenu;
    [SerializeField] private GameObject _gameOverlay;

    [SerializeField] private TMP_Text _health;
    [SerializeField] private Sprite _healthSprite;
    [SerializeField] private Sprite _nothingSprite;

     [SerializeField] private SVGImage _target;
    private int _numHealthPotions;
    
    void Awake()
    {   
        Events.OnToggleAim.AddListener(OnToggleAim);
        Events.OnToggleInventory.AddListener(OnToggleInventory);
        Events.OnToggleCrystalWheel.AddListener(OnToggleCrystalWheel);
        Events.OnTogglePauseMenu.AddListener(OnTogglePauseMenu);

        Events.OnCatchHealthCrystal.AddListener(OnCollectHealth);
        Events.OnUseHealthCrystal.AddListener(OnUseHealth);
    }

    void Update()
    {
        
    }

    public void OnUseHealth() {
        if (_numHealthPotions > 0) {
            _numHealthPotions -= 1;
            _health.text = _numHealthPotions.ToString();
        }

        if (_numHealthPotions == 0) {
            _target.sprite = _nothingSprite;
        }
    }

    public void OnCollectHealth() {
        _numHealthPotions += 1;
        _health.text = _numHealthPotions.ToString();
        _target.sprite = _healthSprite;
    }

    public void OnToggleAim() {
        _aim.OnToggleAim();
    }

    public void OnToggleInventory() {
        _inventory.OnToggleInventory();
    }

    public void OnToggleCrystalWheel() {
        _crystalWheel.OnToggleCrystalWheel();
    }

    public void OnTogglePauseMenu() {
        _pauseMenu.OnTogglePauseMenu();
    }
}
