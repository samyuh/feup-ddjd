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
    private int _numHealthPotions;

    [SerializeField] private SVGImage _target;

    [SerializeField] private DialogManager _currentDialog;
    [SerializeField] private GameObject _dialogOverlay;
    [SerializeField] private TMP_Text _dialog;
    private bool _dialogActive = false;
    private int _numDialog = 0;
   
    void Awake()
    {   
        Events.OnToggleAim.AddListener(OnToggleAim);
        Events.OnToggleInventory.AddListener(OnToggleInventory);
        Events.OnToggleCrystalWheel.AddListener(OnToggleCrystalWheel);
        Events.OnTogglePauseMenu.AddListener(OnTogglePauseMenu);

        Events.OnCatchHealthCrystal.AddListener(OnCollectHealth);
        Events.OnUseHealthCrystal.AddListener(OnUseHealth);

        Events.OnDialog.AddListener(OnDialog);
        Events.OnNextDialog.AddListener(OnNextDialog);
    }

    void Update()
    {
        
    }

    public void OnDialog() {
        _dialogActive = !_dialogActive;

        OnNextDialog();
    }

    public void OnNextDialog() {
        if(_dialogActive) {
            _dialogOverlay.SetActive(_dialogActive);

            if (_currentDialog.dialog[_numDialog] == "CRLF") {
                _dialogActive = false;
                _dialogOverlay.SetActive(_dialogActive);
            } else {
                _dialog.text = _currentDialog.dialog[_numDialog];
            }

            _numDialog += 1;
        }
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
