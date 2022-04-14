using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    #region Game Data
    [SerializeField] private GameData _data;
    #endregion

    #region Camera
    private GameObject _mainCamera;
    public GameObject MainCamera  { get { return _mainCamera; } set { _mainCamera = value;} }
    #endregion

    #region Player
    private GameObject _player;
    #endregion

    #region Input
    private InputHandler _input;
    public InputHandler Input { get { return _input; } set { _input = value;} }
    #endregion

    #region UI
    private Slider _healthBar;
    #endregion

    private void Awake() {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _player = GameObject.FindGameObjectWithTag("Player");
        _healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();

        _input = GetComponent<InputHandler>();
    }

    public void HealthUpdate(int currentHealth, int maxHealth) {
        _healthBar.value = (float) currentHealth/ (float) maxHealth;
        _data.CurrentHealth = currentHealth;
        _data.MaxHealth = maxHealth;
    }
}
