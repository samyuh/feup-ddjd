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
    private GameObject _player;
    private CharacterController _playerController;
    private Animator _playerAnimator;
    private Player _playerScript;

    [SerializeField] private SVGImage _mainCrystal;
    [SerializeField] private Sprite _nothingSprite;
    [SerializeField] private Sprite _obsidiaSprite;
    [SerializeField] private Sprite _airSprite;
    [SerializeField] private Sprite _fireSprite;

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
        Events.FinishDialog.AddListener(FinishDialog);

        Events.OnSetActiveCrystal.AddListener(MainCrystal);

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<CharacterController>();
        _playerAnimator = _player.GetComponent<Animator>();
        _playerScript = _player.GetComponent<Player>();
    }

    public void MainCrystal(CrystalData data) {
        _mainCrystal.sprite = data.icon;
    }
    public void OnDialog(DialogManager currentDialog) {
        _playerController.enabled = false;

        Events.DisableMovement.Invoke();
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
                Events.FinishDialog.Invoke();
                
            } else {
                _dialogCharacter.text = _currentDialog.dialog[_numDialog].character;
                _dialogText.text =_currentDialog.dialog[_numDialog].text;
                _numDialog += 1;
            }
        }
    }

    public void FinishDialog() {
        Events.EnableMovement.Invoke();
        _playerController.enabled = true;
        _playerScript.enabled = true;
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
