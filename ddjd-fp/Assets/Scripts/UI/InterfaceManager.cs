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
    [SerializeField] private GameObject _dialogOverlay;

    [SerializeField] private Sprite _nothingSprite;

    #region Health
    [SerializeField] private TMP_Text _health;
    [SerializeField] private Sprite _healthSprite;
    private int _numHealthPotions;
    [SerializeField] private SVGImage _target;
    #endregion
    
    [SerializeField] private TMP_Text _dialogCharacter;
    [SerializeField] private TMP_Text _dialogText;

    private DialogManager _currentDialog;
    private bool _dialogActive = false;
    private int _numDialog = 0;
   
    void Awake() {   
        Events.OnToggleAim.AddListener(OnToggleAim);
        Events.OnToggleInventory.AddListener(OnToggleInventory);
        Events.OnToggleCrystalWheel.AddListener(OnToggleCrystalWheel);
        Events.OnTogglePauseMenu.AddListener(OnTogglePauseMenu);

        Events.OnCatchHealthCrystal.AddListener(OnCollectHealth);
        Events.OnUseHealthCrystal.AddListener(OnUseHealth);

        Events.OnDialog.AddListener(OnDialog);
        Events.OnNextDialog.AddListener(OnNextDialog);
    }

    public void OnDialog(DialogManager currentDialog) {
        _currentDialog = currentDialog;

        _dialogActive = !_dialogActive;
        OnNextDialog();
    }

    public void OnNextDialog() {
        if(_dialogActive) {
            _dialogOverlay.SetActive(_dialogActive);
            
            if (_numDialog == _currentDialog.dialog.Count) {
                _dialogActive = false;
                _numDialog = 0;
                _dialogOverlay.SetActive(_dialogActive);
            } else {
                _dialogCharacter.text = _currentDialog.dialog[_numDialog].character;
                _dialogText.text =_currentDialog.dialog[_numDialog].text;
                _numDialog += 1;
            }
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
